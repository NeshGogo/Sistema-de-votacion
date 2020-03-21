using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema_de_votacion.Data;
using Sistema_de_votacion.Data.PoliticParties;
using Sistema_de_votacion.Models;
using Sistema_de_votacion.Services.PoliticParties;

namespace Sistema_de_votacion.Controllers
{
    public class PoliticPartiesController : Controller
    {
        private readonly IPoliticPartyService _politicPartyService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PoliticPartiesController(IPoliticPartyService politicPartyService, IHostingEnvironment hostingEnvironment)
        {
            this._politicPartyService = politicPartyService;
            this._hostingEnvironment = hostingEnvironment;
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: PoliticParties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Id,Name,Description,PartyLogoPath,IsActive")]*/ PoliticParty politicParty)
        {
            if (ModelState.IsValid)
            {
                _politicPartyService.InsertPoliticParty(politicParty);
                return RedirectToAction(nameof(Index));
            }
            return View(politicParty);
        }

        // GET: PoliticParties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var politicParty = await _politicPartyService.GetPoliticParties().FirstOrDefaultAsync(pp=>pp.Id==id);
            if (politicParty == null)
            {
                return NotFound();
            }
            return View(politicParty);
        }

        // POST: PoliticParties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,/* [Bind("Id,Name,Description,PartyLogoPath,IsActive")]*/ PoliticParty politicParty)
        {
            if (id != politicParty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            return View(politicParty);
        }

        // GET: PoliticParties/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: PoliticParties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var politicParty = await _politicPartyService.GetPoliticParties().FirstOrDefaultAsync(pp=>pp.Id==id);
            _politicPartyService.DeletePoliticParty(politicParty);
            return RedirectToAction(nameof(Index));
        }

        private bool PoliticPartyExists(int id)
        {
            return _politicPartyService.GetPoliticParties().Any(e => e.Id == id);
        }
    }
}
