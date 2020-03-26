using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        private  int _positionQty;

        public ElectionsController(IElectionService electionService, IPositionService positionService, ICandidateService candidateService, 
                                    IPoliticPartyService politicPartyService, ICitizenService citizenService, IMapper mapper)
        {
            _electionService = electionService;
            _positionService = positionService;
            _candidateService = candidateService;
            _politicPartyService = politicPartyService;
            _citizenService = citizenService;
            _mapper = mapper;
        }

        // GET: Elections
        public IActionResult Index()
        {
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
                if (await _electionService.VerifyElectionOpen() == false)
                {
                    ViewBag.Message = "No hay ningun proceso electoral en estos momentos.";
                    return View(votationLoginViewModel);
                }
                if ( await _electionService.VerifyCitizenVote(citizen.Id))
                {
                    ViewBag.Message = "Usted ya ejercion su derecho al voto.";
                    return View(votationLoginViewModel);
                }

                return RedirectToAction("Votation",citizen);
                
            }


            return View(votationLoginViewModel);
        }
        public async Task<IActionResult> Votation(Citizen citizen)
        {
            _positionQty =   _positionService.GetPositions().Count();
            var election = (await _electionService.GetElections()).Where(e=>e.IsActive).Include(p=>p.ElectionPosition).Include(c=>c.ElectionCitizen);
            
            for (int i =0; i< _positionQty;i++)
            {
                return View(election);
            }
            return View("");
        }

        [Authorize]
        // GET: Elections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _electionService.GetElectionById(id);

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
            var candidates = _candidateService.GetCandidates().Where(c => c.IsActive == true).Select(c => new { c.Id, Name = $"{c.Name} {c.Name}" });
            var citizens = (await _citizenService.GetCitizenByConditionAsync(c => c.IsActive == true)).Select(c => new { c.Id, Name = $"{c.Name}  {c.LastName}" });
            ViewBag.Citizens = new MultiSelectList(citizens, "Id", "Name");
            ViewBag.Positions = new MultiSelectList( _positionService.GetPositions().Where(p => p.IsActive == true), "Id", "Name");
            ViewBag.Candidates = new MultiSelectList(candidates, "Id", "Name");
            ViewBag.PoliticParties = new MultiSelectList(_politicPartyService.GetPoliticParties().Where(pp => pp.IsActive == true), "Id", "Name");
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ElectionCreateViewModel electionViewModel)
        {
            if (ModelState.IsValid)
            {                
                Election election = _mapper.Map<ElectionCreateViewModel, Election>(electionViewModel);
                Election result = await _electionService.InsertElection(election, electionViewModel.ElectionCadidate, electionViewModel.ElectionCitizen, 
                    electionViewModel.ElectionPosition,electionViewModel.ElectionPoliticParty);
                if (result!=null)
                {
                    return RedirectToAction(nameof(Index));
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

            var election = await _electionService.GetElectionById(id);
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
                    await _electionService.UpdateElection(election);
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
        // GET: Elections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var election = await _electionService.GetElectionById(id);
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
            var election = await _electionService.GetElectionById(id);
            await _electionService.DeleteElection(election);
     
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ElectionExists(int id)
        {
            var result = await _electionService.GetElections();
            return result.Any(e => e.Id == id);
        }
    }
}
