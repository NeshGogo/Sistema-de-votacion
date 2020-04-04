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

                if (await _electionService.VerifyCitizenVoteAsync(votationLoginViewModel.DNI))
                {
                    ViewBag.Message = "Usted ya ejercion su derecho al voto.";
                    return View(votationLoginViewModel);
                }

                Citizen citizen = await _citizenService.GetCitizenByConditionAsync(c => c.Dni == votationLoginViewModel.DNI).Result.FirstOrDefaultAsync();
                HttpContext.Session.SetInt32(Configuration.Ciudadano, citizen.Id);
                return RedirectToAction("Votation");                 
            }

            return View(votationLoginViewModel);
        }

        public async Task<IActionResult> Votation(ElectionVotationViewModel electionVotationViewModel)
        {
            if (!HttpContext.Session.GetInt32(Configuration.Ciudadano).HasValue)
                return RedirectToAction(nameof(Index));
            Election election = await _electionService.GetElectionByConditionAsync(e => e.IsActive == true).Result.Include(e => e.ElectionPosition).ThenInclude(ep => ep.Position).FirstOrDefaultAsync();
            if (electionVotationViewModel.Id == 0)
            {                
                electionVotationViewModel.Id = election.Id;
                electionVotationViewModel.Name = election.Name;                
            }
            electionVotationViewModel.Postions = election.ElectionPosition.Select(ep => ep.Position).ToList();
            return View(electionVotationViewModel);
            
                       
        }

        [HttpGet]
        public async Task<IActionResult> Candidate(ElectionVotationViewModel electionVotationViewModel)
        {
            if (!HttpContext.Session.GetInt32(Configuration.Ciudadano).HasValue)
                return RedirectToAction(nameof(Index));

            if (HttpContext.Session.GetInt32(electionVotationViewModel.CurrentPositionName).HasValue)
                return RedirectToAction(nameof(Votation), electionVotationViewModel);

            Election election = await _electionService.GetElectionByConditionAsync(e => e.IsActive == true).Result.Include(e => e.ElectionCadidate)
                .ThenInclude(ec => ec.Candidate).ThenInclude(c=> c.PoliticParty).FirstOrDefaultAsync();

            electionVotationViewModel.Candidates = election.ElectionCadidate.Select(ec => ec.Candidate)
                .Where(c => c.PositionId == electionVotationViewModel.CurrentPositionId).ToList();

            return View(electionVotationViewModel);
            
        }

        [HttpPost]
        public async Task<IActionResult> Candidate(ElectionVotationViewModel model, int candidateId)
        {
            int postionVoted = 0;
            int? citizenId = HttpContext.Session.GetInt32(Configuration.Ciudadano);
            if (!citizenId.HasValue)            
                return RedirectToAction(nameof(Index));          

            HttpContext.Session.SetInt32(model.CurrentPositionName, candidateId);

            model.Postions = (await _electionService.GetElectionByConditionAsync(e => e.IsActive == true).Result
                .Include(e => e.ElectionPosition).ThenInclude(ep => ep.Position).FirstOrDefaultAsync())
                .ElectionPosition.Select(ep => ep.Position).ToList();

            model.Postions.ForEach(p =>
            {
               if (HttpContext.Session.GetInt32(p.Name).HasValue)
                   postionVoted++;
            });

            if (postionVoted != model.Postions.Count())            
                return RedirectToAction(nameof(Votation), model);
            
            List<Result> results = new List<Result>();
            string content = "Usted ha votado por los siguientes candidatos:";
            model.Postions.ForEach(p =>
            {
                Result result = new Result()
                {
                    ElectionId = model.Id,
                    CandidateId = HttpContext.Session.GetInt32(p.Name).Value,
                    CitizenId = citizenId.Value
                };                
                Candidate candidate = _candidateService.GetCandidateById(HttpContext.Session.GetInt32(p.Name));
                content += $"\n {p.Name}: {candidate.Name} {candidate.LastName}.";
                results.Add(result);                
            });         

            await _electionService.InsertElectionCitizenVote(new ElectionCitizen() { ElectionId = model.Id, CitizenId = citizenId.Value });
            await _electionService.InsertElectionResulAsync(results);

            var message = new Sistema_de_votacion.Mail.Message(new string[] { _citizenService.GetCitizenByIdAsync(citizenId.Value).Result.Email }, 
                "RESULTADO DE VOTACION", content );
            await _emailSender.SendEmailAsync(message);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Elections", model);       
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }            
            var electionResult =await _electionService.GetElectionResultsByIdAsync(id.Value);
            string electionName = (await _electionService.GetElectionByIdAsync(id)).Name;

            Election election = await _electionService.GetElectionByConditionAsync(e => e.Id == id.Value).Result.Include(e => e.ElectionCadidate)
                .ThenInclude(ec => ec.Candidate).ThenInclude(c => c.PoliticParty).FirstOrDefaultAsync();

            var candidatess = election.ElectionCadidate.Select(ec => ec.Candidate).ToList();

            var Votes = electionResult.Select(e => e.Candidate).GroupBy( c => c.Position.Name).ToList();
            ResultDetailViewMode resultDetailViewMode = new ResultDetailViewMode {  Votes = Votes, Name = electionName, Candidates = candidatess };

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

                var candidatesSelectedGroupByPosition = candidatesSelected.GroupBy(c => c.Position.Name).Where(c => c.Count( cc => cc.Name !="NULL") > 1 && c.Any(cn => cn.Name == "NULL"));

                if (postitionSelected.Count() != candidatesSelectedGroupByPosition.Count())
                {
                    ViewBag.Message = "Debe seleccionar almenos 2 candidatos y el candidato NULL  por cada posicion electoral para poder iniciar un proceso de eleccion.";
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
        public async Task<IActionResult> ElectionsList()
        {
            var elections = (await _electionService.GetElectionsAsync()).OrderByDescending(e =>  e.IsActive == true).ThenByDescending( e=> e.Date);
            
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
