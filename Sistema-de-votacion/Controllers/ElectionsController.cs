using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Sistema_de_votacion.Data.ElectionCitizens;
using Sistema_de_votacion.Data.Results;
using Sistema_de_votacion.Helpers;
using Sistema_de_votacion.Models;
using Sistema_de_votacion.Services.Candidates;
using Sistema_de_votacion.Services.Candidates.Positions;
using Sistema_de_votacion.Services.Citizens;
using Sistema_de_votacion.Services.Elections;
using Sistema_de_votacion.Services.PoliticParties;
using Sistema_de_votacion.ViewModels;

namespace Sistema_de_votacion.Controllers
{
    public class ElectionsController : Controller
    {
        private readonly IElectionService _electionService;
        private readonly IPositionService _positionService;
        private readonly ICandidateService _candidateService;
        private readonly IPoliticPartyService _politicPartyService;
        private readonly ICitizenService _citizenService;
        private readonly IMapper _mapper;
        private readonly IResultRepository _resultRepository;
        private readonly IElectionCitizenRepository _electionCitizenRepository;
        private readonly Sistema_de_votacion.Mail.IEmailSender _emailSender;
        private  int _positionQty;

        public ElectionsController( IElectionService electionService, IPositionService positionService, ICandidateService candidateService, 
                                    IPoliticPartyService politicPartyService, ICitizenService citizenService, IMapper mapper,
                                    IResultRepository resultRepository, IElectionCitizenRepository electionCitizenRepository,
                                    Sistema_de_votacion.Mail.IEmailSender emailSender)
        {
            _electionService = electionService;
            _positionService = positionService;
            _candidateService = candidateService;
            _politicPartyService = politicPartyService;
            _citizenService = citizenService;
            _mapper = mapper;
            this._resultRepository = resultRepository;
            this._electionCitizenRepository = electionCitizenRepository;
            this._emailSender = emailSender;
        }

        // GET: Elections
        public IActionResult Index()
        {
            if (User.Identity.Name != null)
                return RedirectToAction("Index", "AdminitrationHome");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(VotationLoginViewModel votationLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                
               
                if (await _citizenService.VerifyExistAsync(votationLoginViewModel.DNI) == false)
                {
                    ViewBag.Message = "EL ciudadano no existe o esta inactivo.";
                    return View(votationLoginViewModel);
                }
                
                if (await _electionService.VerifyElectionOpenAsync() == false)
                {
                    ViewBag.Message = "No hay ningun proceso electoral en estos momentos.";
                    return View(votationLoginViewModel);
                }
                var citizen = await _citizenService.GetCitizenByConditionAsync(c => c.Dni == votationLoginViewModel.DNI).Result.FirstOrDefaultAsync();
                if ( await _electionService.VerifyCitizenVoteAsync(citizen.Id))
                {
                    ViewBag.Message = "Usted ya ejercion su derecho al voto.";
                    return View(votationLoginViewModel);
                }

                return RedirectToAction("Votation", citizen); 
                
            }

            return View(votationLoginViewModel);
        }
        public async Task<IActionResult> Votation()
        {
            var election = await _electionService.GetElectionByConditionAsync(e => e.IsActive == true).Result.Include(e => e.ElectionPosition).ThenInclude(ep => ep.Position).FirstOrDefaultAsync();
           
            
            return View(election);
        }

        [HttpGet]
        public async Task<IActionResult> Candidate(Citizen citizen, ElectionVotationViewModel model)
        {
            _positionQty = (await _positionService.GetPositionsAsync()).Count();
            var politicParties = _politicPartyService.GetPoliticParties().ToList();
            var election = await _electionService.GetElectionsAsync().Result.Where(e=> e.IsActive == true).Include(e => e.ElectionCadidate).ThenInclude(ec => ec.Candidate).ThenInclude(c=> c.Position).FirstOrDefaultAsync();


                foreach (var cadidateElection in election.ElectionCadidate)
                {
                    PoliticParty politicParty = _politicPartyService.GetPoliticPartyById(cadidateElection.Candidate.PoliticPartyId);
                    cadidateElection.Candidate.PoliticParty = politicParty;
                }
                   



            ViewBag.Election = (await _electionService.GetElectionsAsync()).Where(e => e.IsActive).Include(p => p.ElectionPosition).Include(c => c.ElectionCitizen).ToList();
            if(model.ElectionId == 0 )
            {
                model = _mapper.Map<Election, ElectionVotationViewModel>(election);
                model.PositionIndex = model.PositionIndex;
                model.CitizenId = citizen.Id;
            }           

            
            
            var positions = election.ElectionCadidate.Select(ce => ce.Candidate).Select(c => c.Position).Where(p => p.IsActive == true).Distinct();
            model.Position = positions.ElementAt(model.PositionIndex);
            model.Candidates = election.ElectionCadidate.Select(ec => ec.Candidate).Where(c=> c.Position == model.Position).ToList();
            
     

            return View(model);
            
        }
        [HttpPost]
        public async Task<IActionResult> Candidate(ElectionVotationViewModel model, int candidateId)
        {
            Result result = new Result()
            {
                ElectionId = model.ElectionId,
                CandidateId=candidateId,
                CitizenId=model.CitizenId
            };
            ElectionCitizen elctionCitizen = new ElectionCitizen()
            {
                ElectionId=model.ElectionId,
                CitizenId=model.CitizenId
            };

            _electionCitizenRepository.Insert(elctionCitizen);
            _resultRepository.Insert(result);
            var candidate = _candidateService.GetCandidateById(candidateId);

            

            if (candidate.Position.Name.Contains("Presidente"))
            {
                HttpContext.Session.SetString(Configuration.Presidente, candidate.Name + " " + candidate.LastName);
       
            }
            else if(candidate.Position.Name.Contains("Alcalde"))
            {
                HttpContext.Session.SetString(Configuration.Alcalde, candidate.Name + " " + candidate.LastName);
            }
            else if (candidate.Position.Name.Contains("Regidor"))
            {
                HttpContext.Session.SetString(Configuration.Regidor, candidate.Name + " " + candidate.LastName);
            }
            else if (candidate.Position.Name.Contains("Senador"))
            {
                HttpContext.Session.SetString(Configuration.Senador, candidate.Name + " " + candidate.LastName);
            }
            var politicParties = _politicPartyService.GetPoliticParties().ToList();
            var election = (await _electionService.GetElectionsAsync()).Where(e => e.IsActive == true).Include(e => e.ElectionCadidate).ThenInclude(ec => ec.Candidate).ThenInclude(c => c.Position).FirstOrDefault();

            
            foreach (var cadidateElection in election.ElectionCadidate)
            {
                PoliticParty politicParty = _politicPartyService.GetPoliticPartyById(cadidateElection.Candidate.PoliticPartyId);
                cadidateElection.Candidate.PoliticParty = politicParty;
            }
            
            ViewBag.Election = (await _electionService.GetElectionsAsync()).Where(e => e.IsActive).Include(p => p.ElectionPosition).Include(c => c.ElectionCitizen).ToList();

            
     
            model.PositionIndex = model.PositionIndex + 1;
            
            var positions = election.ElectionCadidate.Select(ce => ce.Candidate).Select(c => c.Position).Where(p => p.IsActive == true).Distinct();

            if (model.PositionIndex == positions.Count())
            {

                var message = new Sistema_de_votacion.Mail.Message(new string[] { "sistemadesarrolloeleccion@gmail.com" } , "RESULTADO DE VOTACION", 
                    "Usted ha votado por los siguientes candidatos: \nPRESIDENTE:"+ HttpContext.Session.GetString(Configuration.Presidente)+
                                                                    "\nALCALDE:"+ HttpContext.Session.GetString(Configuration.Alcalde) +
                                                                    "\nREGIDOR:" + HttpContext.Session.GetString(Configuration.Regidor) +
                                                                    "\nSENADOR:" + HttpContext.Session.GetString(Configuration.Senador));

                await _emailSender.SendEmailAsync(message);

                

                return RedirectToAction("Index", "Elections", model);

            }
            return RedirectToAction("Votation", "Elections", model);

            //return View();
        }
        [Authorize]
        // GET: Elections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }            
            var electionResult =await _electionService.GetElectionResultsByIdAsync(id.Value);
            string electionName = (await _electionService.GetElectionByIdAsync(id)).Name;
            var postions = electionResult.Select(e => e.Candidate).Select(c => c.Position.Name).Distinct().ToList();
            var candidates = electionResult.Select(e => e.Candidate).ToList();
            ResultDetailViewMode resultDetailViewMode = new ResultDetailViewMode { Postions = postions, Candidates = candidates, Name = electionName };

            if (electionResult == null)
            {
                ViewBag.Message = "No se encontro ningun resultado de esa eleccion.";
                return RedirectToAction(nameof(ElectionsList));
            }

            return View(resultDetailViewMode);
        }

        [Authorize]
        // GET: Elections/Create
        public async Task<IActionResult> Create()
        {
            if (await _electionService.VerifyElectionOpenAsync())
                return RedirectToAction("ElectionsList");
            
            var candidates = await _candidateService.GetCandidates().Where(c => c.IsActive == true).Include(c=> c.Position).Include(c=> c.PoliticParty).OrderBy(c=>c.Position.Name).ToListAsync();
            var candidateElection = _mapper.Map<List<Candidate>, List<CandidateElectionViewModel>>(candidates);
            ElectionCreateViewModel electionCreateViewModel = new ElectionCreateViewModel { ElectionCadidate = candidateElection.GroupBy( c => c.Position.Name).ToList() };
            return View(electionCreateViewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ElectionCreateViewModel electionViewModel)
        {
            
            var candidatesSelectedId = (string[])TempData[Configuration.CandidatesElectionCreateSelected];
            if (ModelState.IsValid)
            {
                var candidatesSelected = await _candidateService.GetCandidates().Include(c => c.Position).Where(c => candidatesSelectedId.Contains(c.Id.ToString())).ToListAsync();
                var postitionSelected = candidatesSelected.Select(c => c.Position).Distinct().ToList();

                if (postitionSelected.Count() < 4)
                {
                    ViewBag.Message = "Debe seleccionar candidatos para 4 posiciones electorales diferentes para poder iniciar un proceso electoral.";
                    var allcandidates = await _candidateService.GetCandidates().Where(c => c.IsActive == true).Include(c => c.Position).Include(c => c.PoliticParty).OrderBy(c => c.Position.Name).ToListAsync();
                    electionViewModel.ElectionCadidate = (_mapper.Map<List<Candidate>, List<CandidateElectionViewModel>>(allcandidates) ).GroupBy(c => c.Position.Name).ToList();
                    return View(electionViewModel);
                }

                var candidatesSelectedGroupByPosition = candidatesSelected.GroupBy(c => c.Position.Name).Where(c => c.Count() > 1);

                if (postitionSelected.Count() != candidatesSelectedGroupByPosition.Count())
                {
                    ViewBag.Message = "Debe seleccionar almenos 2 candidatos por posicion electoral para poder iniciar un proceso electoral.";
                    var allcandidates = await _candidateService.GetCandidates().Where(c => c.IsActive == true).Include(c => c.Position).Include(c => c.PoliticParty).OrderBy(c => c.Position.Name).ToListAsync();
                    electionViewModel.ElectionCadidate =( _mapper.Map<List<Candidate>, List<CandidateElectionViewModel>>(allcandidates) ).GroupBy(c => c.Position.Name).ToList();
                    return View(electionViewModel);
                }
                
                Election election = _mapper.Map<ElectionCreateViewModel, Election>(electionViewModel);
                Election result = await _electionService.InsertElectionAsync(election, candidatesSelected, postitionSelected);

                if (result!=null)
                {
                    
                    
                    return RedirectToAction(nameof(ElectionsList));
                }
                ModelState.AddModelError("", "Ocurrion un erro al insertar la eleccion.");
            }
            return View(electionViewModel);
        }
        [Authorize]
        // GET: Elections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _electionService.GetElectionByIdAsync(id);
            if (election == null)
            {
                return NotFound();
            }
            return View(election);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Date,IsActive")] Election election)
        {
            if (id != election.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _electionService.UpdateElectionAsync(election);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ElectionExists(election.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(election);
        }
        [Authorize]
        public async Task<IActionResult> ElectionsList()
        {
            var elections = (await _electionService.GetElectionsAsync()).OrderBy(e =>  e.IsActive == false).ThenBy( e=> e.Date);
            
            return View(elections);
        }
        [Authorize]
        // GET: Elections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _electionService.GetElectionByIdAsync(id);
            if (election == null)
            {
                return NotFound();
            }

            return View(election);
        }
        [Authorize]
        // POST: Elections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var election = await _electionService.GetElectionByIdAsync(id);
            await _electionService.DeleteElectionAsync(election);
     
            return RedirectToAction(nameof(ElectionsList));
        }
        [HttpPost]
        public void CandidatesSelected(string[] candidates)
        {
            TempData[Configuration.CandidatesElectionCreateSelected] = candidates;
        }
        private async Task<bool> ElectionExists(int id)
        {
            var result = await _electionService.GetElectionsAsync();
            return result.Any(e => e.Id == id);
        }
    }
}
