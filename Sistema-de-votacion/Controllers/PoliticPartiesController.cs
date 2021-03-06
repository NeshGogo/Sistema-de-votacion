﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_de_votacion.Data.Elections;
using Sistema_de_votacion.Models;
using Sistema_de_votacion.Services.Elections;
using Sistema_de_votacion.Services.PoliticParties;
using Sistema_de_votacion.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema_de_votacion.Controllers
{
    [Authorize]
    public class PoliticPartiesController : Controller
    {
        private readonly IPoliticPartyService _politicPartyService;
        private readonly IElectionRepository _electionRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IElectionService _electionService;

        public PoliticPartiesController(IPoliticPartyService politicPartyService, IElectionRepository electionRepository,  
            IHostingEnvironment hostingEnvironment, IElectionService electionService )
        {
            this._politicPartyService = politicPartyService;
            this._electionRepository = electionRepository;
            this._hostingEnvironment = hostingEnvironment;
            this._electionService = electionService;
        }

        // GET: PoliticParties
        public async Task<IActionResult> Index()
        {
            return View(await _politicPartyService.GetPoliticParties().ToListAsync());
        }

        // GET: PoliticParties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var politicParty = await _politicPartyService.GetPoliticParties()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (politicParty == null)
            {
                return NotFound();
            }

            return View(politicParty);
        }

        // GET: PoliticParties/Create
        public async Task<IActionResult> Create()
        {
            if (await _electionService.VerifyElectionOpenAsync())
                return RedirectToAction("Index");
            return View();
        }

        // POST: PoliticParties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PoliticPartyCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                if (uniqueFileName == null)
                    uniqueFileName = "PartidoNull.png";
                PoliticParty politicParty = new PoliticParty
                {
                    Name = model.Name,
                    Description = model.Description,
                    PartyLogoPath= uniqueFileName,
                    IsActive=model.IsActive

                };

                _politicPartyService.InsertPoliticParty(politicParty);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PoliticParties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await _electionService.VerifyElectionOpenAsync())
                return RedirectToAction("Index");
            if (id == null)
            {
                return NotFound();
            }

            var politicParty = await _politicPartyService.GetPoliticParties().FirstOrDefaultAsync(pp=>pp.Id==id);

            var politicPartyCreateViewModel = new PoliticPartyCreateViewModel()
            {
                Id=politicParty.Id,
                Name = politicParty.Name,
                Description= politicParty.Description,
                Photo=politicParty.PartyLogoPath,
                IsActive=politicParty.IsActive

            };


            if (politicParty == null)
            {
                return NotFound();
            }
            return View(politicPartyCreateViewModel);
        }

        // POST: PoliticParties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,/* [Bind("Id,Name,Description,PartyLogoPath,IsActive")]*/ PoliticPartyCreateViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                if (uniqueFileName == null)
                {
                    uniqueFileName = model.Photo;
                }

                var politicParty = new PoliticParty()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    PartyLogoPath = uniqueFileName,
                    IsActive = model.IsActive

                };
                try
                {
                    _politicPartyService.UdatePoliticParty(politicParty);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoliticPartyExists(politicParty.Id))
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
            return View(model);
        }

        // GET: PoliticParties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await _electionService.VerifyElectionOpenAsync())
                return RedirectToAction("Index");
            if (id == null)
            {
                return NotFound();
            }

            var politicParty = await _politicPartyService.GetPoliticParties()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (politicParty == null)
            {
                return NotFound();
            }

            return View(politicParty);
        }

        // POST: PoliticParties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _electionService.VerifyElectionOpenAsync())
                return RedirectToAction("Index");
            var politicParty = await _politicPartyService.GetPoliticParties().FirstOrDefaultAsync(pp=>pp.Id==id);
         
            _politicPartyService.DeletePoliticParty(politicParty);
            return RedirectToAction(nameof(Index));
        }

        private bool PoliticPartyExists(int id)
        {
            return _politicPartyService.GetPoliticParties().Any(e => e.Id == id);
        }
        private string ProcessUploadedFile(PoliticPartyCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.PartyLogoPath != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PartyLogoPath.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.PartyLogoPath.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }
    }
}

