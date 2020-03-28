using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        private  int _positionQty;

        public ElectionsController(IElectionService electionService, IPositionService positionService, ICandidateService candidateService, 
                                    IPoliticPartyService politicPartyService, ICitizenService citizenService, IMapper mapper, IResultRepository resultRepository, IElectionCitizenRepository electionCitizenRepository)
        {
            _electionService = electionService;
            _positionService = positionService;
            _candidateService = candidateService;
            _politicPartyService = politicPartyService;
            _citizenService = citizenService;
            _mapper = mapper;
            this._resultRepository = resultRepository;
            this._electionCitizenRepository = electionCitizenRepository;
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
                
                Citizen citizen = (await  _citizenService.GetCitizenByConditionAsync(c => c.Dni == votationLoginViewModel.DNI && c.IsActive == true)).FirstOrDefault();
                if (citizen == null)
                {
                    ViewBag.Message = "EL ciudadano no existe o esta inactivo.";
                    return View(votationLoginViewModel);
                }
                ;
                if (await _electionService.VerifyElectionOpenAsync() == false)
                {
                    ViewBag.Message = "No hay ningun proceso electoral en estos momentos.";
                    return View(votationLoginViewModel);
                }
                if ( await _electionService.VerifyCitizenVoteAsync(citizen.Id))
                {
                    ViewBag.Message = "Usted ya ejercion su derecho al voto.";
                    return View(votationLoginViewModel);
                }

                return RedirectToAction("Votation", citizen/*new { citizen = citizen }*/); 
                
            }


            return View(votationLoginViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Votation(Citizen citizen, ElectionVotationViewModel model)
        {
            _positionQty = (await _positionService.GetPositionsAsync()).Count();
            var politicParties = _politicPartyService.GetPoliticParties().ToList();
            var election = (await _electionService.GetElectionsAsync()).Where(e=> e.IsActive == true).Include(e => e.ElectionCadidate).ThenInclude(ec => ec.Candidate).ThenInclude(c=> c.Position).FirstOrDefault();


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
        public async Task<IActionResult> Votation(ElectionVotationViewModel model, int candidateId)
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

            if (candidate.Position.Name == "Presidente")
            {
                HttpContext.Session.SetString(Configuration.Presidente, candidate.Name + " " + candidate.LastName);
            }
            else if(candidate.Position.Name == "Alcalde")
            {
                HttpContext.Session.SetString(Configuration.Alcalde, candidate.Name + " " + candidate.LastName);
            }
            else if (candidate.Position.Name == "Regidor")
            {
                HttpContext.Session.SetString(Configuration.Regidor, candidate.Name + " " + candidate.LastName);
            }
            else if (candidate.Position.Name == "Senador")
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

            var election = await _electionService.GetElectionByIdAsync(id);

            if (election == null)
            {
                return NotFound();
            }

            return View(election);
        }

        [Authorize]
        // GET: Elections/Create
        public async Task<IActionResult> Create()
        {
            if (await _electionService.VerifyElectionOpenAsync())
                return RedirectToAction("ElectionsList");
            
            var candidates = await _candidateService.GetCandidates().Where(c => c.IsActive == true).Include(c=> c.Position).Include(c=> c.PoliticParty).OrderBy(c=>c.Position.Name).ToListAsync();
            var candidateElection = _mapper.Map<List<Candidate>, List<CandidateElectionViewModel>>(candidates);
            ElectionCreateViewModel electionCreateViewModel = new ElectionCreateViewModel { ElectionCadidate = candidateElection };
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
                var postitionSelected = candidatesSelected.Select(c => c.Position.Name).Distinct();

                if (postitionSelected.Count() < 4)
                {
                    ViewBag.Message = "Debe seleccionar candidatos para 4 posiciones electorales diferentes para poder iniciar un proceso electoral.";
                    var allcandidates = await _candidateService.GetCandidates().Where(c => c.IsActive == true).Include(c => c.Position).Include(c => c.PoliticParty).OrderBy(c => c.Position.Name).ToListAsync();
                    electionViewModel.ElectionCadidate = _mapper.Map<List<Candidate>, List<CandidateElectionViewModel>>(allcandidates);
                    return View(electionViewModel);
                }

                var candidatesSelectedGroupByPosition = candidatesSelected.GroupBy(c => c.Position.Name).Where(c => c.Count() > 1);

                if (postitionSelected.Count() != candidatesSelectedGroupByPosition.Count())
                {
                    ViewBag.Message = "Debe seleccionar almenos 2 candidatos por posicion electoral para poder iniciar un proceso electoral.";
                    var allcandidates = await _candidateService.GetCandidates().Where(c => c.IsActive == true).Include(c => c.Position).Include(c => c.PoliticParty).OrderBy(c => c.Position.Name).ToListAsync();
                    electionViewModel.ElectionCadidate = _mapper.Map<List<Candidate>, List<CandidateElectionViewModel>>(allcandidates);
                    return View(electionViewModel);
                }

                Election election = _mapper.Map<ElectionCreateViewModel, Election>(electionViewModel);
                Election result = await _electionService.InsertElectionAsync(election, candidatesSelected);

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
