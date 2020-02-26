using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema_de_votacion.Data;
using Sistema_de_votacion.Models;
using Sistema_de_votacion.Services.Citizens;

namespace Sistema_de_votacion.Controllers
{
    public class CitizensController : Controller
    {
        private readonly ICitizenService _citizenServices;

        public CitizensController(ICitizenService citizenServices)
        {
            _citizenServices = citizenServices;
        }

        // GET: Citizens
        public async Task<IActionResult> Index()
        {
            var citizens = await _citizenServices.GetCitizenByCondition(c => c.IsActive == true );
            return View( citizens.ToList().OrderBy(c => new { c.Name, c.LastName }));
        }
        [HttpPost]
        public async Task<IActionResult> Index( string activeParam)
        {
            IQueryable<Citizen> citizens;

            if (activeParam == "on") {
                citizens = await _citizenServices.GetCitizenByCondition(c => c.IsActive == true);
            }
            else
            {
                citizens = await _citizenServices.GetCitizenByCondition(c => c.IsActive == false);
            }            

            return View(citizens.ToList());
        }

        // GET: Citizens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizen = await _citizenServices.GetCitizenById(id);
                
            if (citizen == null)
            {
                return NotFound();
            }

            return View(citizen);
        }

        // GET: Citizens/Create
        public IActionResult Create()
        {
            return View("Form");
        }

        // POST: Citizens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Dni,Name,LastName,Email,IsActive")] Citizen citizen)
        {
            if (ModelState.IsValid)
            {
                citizen.IsActive = true;
                 var newCitizen = await _citizenServices.InsertCitizen(citizen);
                if (newCitizen != null)
                {
                    return RedirectToAction(nameof(Index));
                }                
                
            }
            return View("Form",citizen);
        }

        // GET: Citizens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizen = await _citizenServices.GetCitizenById(id);
            if (citizen == null)
            {
                return NotFound();
            }
            return View("Form", citizen);
        }

        // POST: Citizens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Dni,Name,LastName,Email,IsActive")] Citizen citizen)
        {
            if (id != citizen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _citizenServices.UdateCitizen(citizen);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitizenExists(citizen.Id))
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
            return View("Form", citizen);
        }

        // GET: Citizens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizen = await _citizenServices.GetCitizenById(id);
                
            if (citizen == null)
            {
                return NotFound();
            }

            return View(citizen);
        }

        // POST: Citizens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var citizen = await _citizenServices.GetCitizenById(id);
            if (citizen != null)
            {
                await _citizenServices.DeleteCitizen(citizen);
            }          
          
            return RedirectToAction(nameof(Index));
        }

        private bool CitizenExists(int id)
        {
            var citizen = _citizenServices.GetCitizenById(id);
            if (citizen != null)
                return true;
            return false;
        }
    }
}
