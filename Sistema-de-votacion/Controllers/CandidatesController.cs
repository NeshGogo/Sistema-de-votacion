using System;
using System.Collections.Generic;
using System.IO;
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
using Sistema_de_votacion.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace Sistema_de_votacion.Controllers
{
    [Authorize]
    public class CandidatesController : Controller
    {
        private readonly ICandidateService _candidateService;
        private readonly IPoliticPartyService _politicPartyService;
        private readonly IPositionService _positionService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CandidatesController(ICandidateService candidateService,IPoliticPartyService politicPartyService, IPositionService positionService, IHostingEnvironment hostingEnvironment)
        {
            //_context = context;
            this._candidateService = candidateService;
            this._politicPartyService = politicPartyService;
            this._positionService = positionService;
            this._hostingEnvironment = hostingEnvironment;
        }

        // GET: Candidates
        public async Task<IActionResult> Index()
        {
            var electionDBContext = _candidateService.GetCandidates().Where(c=>c.IsActive==true).OrderBy(c=>c.Name).Include(c => c.PoliticParty).Include(c => c.Position);
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
        public async Task<IActionResult> Create()
        {

            ViewData["PoliticPartyId"] = new SelectList(_politicPartyService.GetPoliticParties().Where(p=> p.IsActive == true), "Id", "Name");
            ViewData["PositionId"] = new SelectList((await _positionService.GetPositionsAsync()).Where(p => p.IsActive == true), "Id", "Name");
            return View();
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LastName,PoliticPartyId,PositionId,ProfilePhothoPath,IsActive")] CandidateCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Candidate candidate = new Candidate
                {
                    Name = model.Name,
                    LastName = model.LastName,
                    PoliticPartyId = model.PoliticPartyId,
                    PositionId = model.PositionId,
                    ProfilePhothoPath = uniqueFileName,
                    IsActive = model.IsActive

                };
                _candidateService.InsertCandidate(candidate);
                //await _candidateService.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PoliticPartyId"] = new SelectList(_politicPartyService.GetPoliticParties().Where(p => p.IsActive == true), "Id", "Name", model.PoliticPartyId);
            ViewData["PositionId"] = new SelectList((await _positionService.GetPositionsAsync()).Where(p => p.IsActive == true), "Id", "Name", model.PositionId);
            return View(model);
        }

        // GET: Candidates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await _candidateService.GetCandidates().FirstAsync(c=>c.Id==id);

            var candidateViewModel = new CandidateCreateViewModel()
            {
                Id=candidate.Id,
                Name=candidate.Name,
                LastName=candidate.LastName,
                IsActive=candidate.IsActive,
                photoName=candidate.ProfilePhothoPath

            };

            if (candidate == null)
            {
                return NotFound();
            }
            ViewData["PoliticPartyId"] = new SelectList(_politicPartyService.GetPoliticParties().Where(p => p.IsActive == true), "Id", "Name", candidate.PoliticPartyId);
            ViewData["PositionId"] = new SelectList((await _positionService.GetPositionsAsync()).Where(p => p.IsActive == true), "Id", "Name", candidate.PositionId);
            return View(candidateViewModel);
        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CandidateCreateViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                if (uniqueFileName==null)
                {
                    uniqueFileName = model.photoName;
                }
                Candidate candidate = new Candidate
                {
                    //Id= model.Id,
                    Name = model.Name,
                    LastName = model.LastName,
                    PoliticPartyId = model.PoliticPartyId,
                    PositionId = model.PositionId,
                    ProfilePhothoPath = uniqueFileName,
                    IsActive = model.IsActive

                };
                try
                {
                    _candidateService.UpdateCandidate(candidate);
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
            ViewData["PoliticPartyId"] = new SelectList(_politicPartyService.GetPoliticParties().Where(p => p.IsActive == true), "Id", "Name", model.PoliticPartyId);
            ViewData["PositionId"] = new SelectList((await _positionService.GetPositionsAsync()).Where(p => p.IsActive == true), "Id", "Name", model.PositionId);
            return View(model);
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
            candidate.IsActive = false;
            _candidateService.UpdateCandidate(candidate);
            //_candidateService.DeleteCandidate(candidate);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidateExists(int id)
        {
            return _candidateService.GetCandidates().Any(e => e.Id == id);
        }
        private string ProcessUploadedFile(CandidateCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.ProfilePhothoPath != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePhothoPath.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfilePhothoPath.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }
    }
}
