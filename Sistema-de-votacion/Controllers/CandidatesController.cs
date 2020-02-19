using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema_de_votacion.Data;
using Sistema_de_votacion.Models;
using Sistema_de_votacion.Services.Candidates;
using Sistema_de_votacion.Services.Candidates.Positions;
using Sistema_de_votacion.Services.PoliticParties;

namespace Sistema_de_votacion.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly ICandidateService _candidateService;
        private readonly IPoliticPartyService _politicPartyService;
        private readonly IPositionService _positionService;

        public CandidatesController(ICandidateService candidateService,IPoliticPartyService politicPartyService, IPositionService positionService)
        {
            //_context = context;
            this._candidateService = candidateService;
            this._politicPartyService = politicPartyService;
            this._positionService = positionService;
        }

        // GET: Candidates
        public async Task<IActionResult> Index()
        {
            var electionDBContext = _candidateService.GetCandidates().Include(c => c.PoliticParty).Include(c => c.Position);
            return View(await electionDBContext.ToListAsync());
        }

        // GET: Candidates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _candidateService.GetCandidates()
                .Include(c => c.PoliticParty)
                .Include(c => c.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        //GET: Candidates/Create
        public IActionResult Create()
        {
            ViewData["PoliticPartyId"] = new SelectList(_politicPartyService.GetPoliticParties(), "Id", "Description");
            ViewData["PositionId"] = new SelectList(_positionService.GetPositions(), "Id", "Description");
            return View();
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LastName,PoliticPartyId,PositionId,ProfilePhothoPath,IsActive")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                _candidateService.InsertCandidate(candidate);
                //await _candidateService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PoliticPartyId"] = new SelectList(_politicPartyService.GetPoliticParties(), "Id", "Description", candidate.PoliticPartyId);
            ViewData["PositionId"] = new SelectList(_positionService.GetPositions(), "Id", "Description", candidate.PositionId);
            return View(candidate);
        }

        // GET: Candidates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _candidateService.GetCandidates().FirstAsync(c=>c.Id==id);
            if (candidate == null)
            {
                return NotFound();
            }
            ViewData["PoliticPartyId"] = new SelectList(_politicPartyService.GetPoliticParties(), "Id", "Description", candidate.PoliticPartyId);
            ViewData["PositionId"] = new SelectList(_positionService.GetPositions(), "Id", "Description", candidate.PositionId);
            return View(candidate);
        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LastName,PoliticPartyId,PositionId,ProfilePhothoPath,IsActive")] Candidate candidate)
        {
            if (id != candidate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _candidateService.UdateCandidate(candidate);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateExists(candidate.Id))
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
            ViewData["PoliticPartyId"] = new SelectList(_politicPartyService.GetPoliticParties(), "Id", "Description", candidate.PoliticPartyId);
            ViewData["PositionId"] = new SelectList(_positionService.GetPositions(), "Id", "Description", candidate.PositionId);
            return View(candidate);
        }

        // GET: Candidates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _candidateService.GetCandidates()
                .Include(c => c.PoliticParty)
                .Include(c => c.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidate = await _candidateService.GetCandidates().FirstAsync(c=>c.Id==id);
            _candidateService.DeleteCandidate(candidate);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidateExists(int id)
        {
            return _candidateService.GetCandidates().Any(e => e.Id == id);
        }
    }
}
