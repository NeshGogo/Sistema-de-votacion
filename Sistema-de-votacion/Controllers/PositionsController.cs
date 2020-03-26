using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sistema_de_votacion.Data;
using Sistema_de_votacion.Models;
using Sistema_de_votacion.Services.Candidates.Positions;
using Sistema_de_votacion.Services.Elections;

namespace Sistema_de_votacion.Controllers
{
    [Authorize]
    public class PositionsController : Controller
    {
        private readonly IPositionService _positionService;
        private readonly IElectionService _electionService;

        public PositionsController(IPositionService positionService, IElectionService electionService)
        {
            _positionService = positionService;
            _electionService = electionService;
        }

        // GET: Positions
        public async Task<IActionResult> Index()
        {
            return View(await _positionService.GetPositionsByConditionAsync(p => p.IsActive == true));
        }
        [HttpPost]
        public async Task<IActionResult> Index(string activeParam)
        {
            IEnumerable<Position> positions;

            if (activeParam == "on")            
                positions = await _positionService.GetPositionsByConditionAsync(c => c.IsActive == true);            
            else            
                positions = await _positionService.GetPositionsByConditionAsync(c => c.IsActive == false);            

            return View(positions.ToList());
        }

        // GET: Positions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (await _electionService.VerifyElectionOpenAsync())
            {
                ViewBag.Message="No es posible Modificar ninguna posicion politica porque actualmente existe una eleccion abierta.";
                return RedirectToAction("Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            var position = await _positionService.GetPositionByIdAsync(id.Value);
                
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // GET: Positions/Create
        public async Task<IActionResult> Create()
        {
            if (await _electionService.VerifyElectionOpenAsync())
            {
                ViewBag.Message = "No es posible crear o añadir ninguna posicion politica porque actualmente existe una eleccion abierta.";
                return RedirectToAction("Index");
            }
            return View("Form");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Position position)
        {
            if (ModelState.IsValid)
            {
                position.IsActive = true;
                await  _positionService.InsertPositionAsync(position);
               
                return RedirectToAction(nameof(Index));
            }
            return View("Form",position);
        }

        // GET: Positions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = await _positionService.GetPositionByIdAsync(id.Value);
            if (position == null)
            {
                return NotFound();
            }
            return View("Form",position);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,IsActive")] Position position)
        {
            if (id != position.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {                    
                    await _positionService.UdatePositionAsync(position);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PositionExists(position.Id))
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
            return View("Form",position);
        }

        // GET: Positions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await _electionService.VerifyElectionOpenAsync())
            {
                ViewBag.Message = "No es posible Eliminar ninguna posicion politica porque actualmente existe una eleccion abierta.";
                return RedirectToAction("Index");
            }
            if (id == null)
            {
                return NotFound();
            }
            var position =  await _positionService.GetPositionByIdAsync(id.Value);
            
                
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _electionService.VerifyElectionOpenAsync())
            {
                ViewBag.Message = "No es posible eliminar ninguna posicion politica porque actualmente existe una eleccion abierta.";
                return RedirectToAction("Index");
            }
            var position = await _positionService.GetPositionByIdAsync(id);
            await  _positionService.DeletePositionAsync(position);
            
            return RedirectToAction(nameof(Index));
        }

        private async  Task<bool> PositionExists(int id)
        {
            return (await _positionService.GetPositionsAsync()).Any(e => e.Id == id);
        }
    }
}
