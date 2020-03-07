using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema_de_votacion.Data;
using Sistema_de_votacion.Models;

namespace Sistema_de_votacion.Controllers
{
    public class PoliticPartiesController : Controller
    {
        private readonly ElectionDBContext _context;

        public PoliticPartiesController(ElectionDBContext context)
        {
            _context = context;
        }

        // GET: PoliticParties
        public async Task<IActionResult> Index()
        {
            return View(await _context.PoliticParty.ToListAsync());
        }

        // GET: PoliticParties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var politicParty = await _context.PoliticParty
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
        public async Task<IActionResult> Create([Bind("Id,Name,Description,PartyLogoPath,IsActive")] PoliticParty politicParty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(politicParty);
                await _context.SaveChangesAsync();
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

            var politicParty = await _context.PoliticParty.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,PartyLogoPath,IsActive")] PoliticParty politicParty)
        {
            if (id != politicParty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(politicParty);
                    await _context.SaveChangesAsync();
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

            var politicParty = await _context.PoliticParty
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
            var politicParty = await _context.PoliticParty.FindAsync(id);
            _context.PoliticParty.Remove(politicParty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoliticPartyExists(int id)
        {
            return _context.PoliticParty.Any(e => e.Id == id);
        }
    }
}
