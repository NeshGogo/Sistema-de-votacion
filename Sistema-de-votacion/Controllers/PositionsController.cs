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

namespace Sistema_de_votacion.Controllers
{
    [Authorize]
    public class PositionsController : Controller
    {
        private readonly IPositionService _positionService;

        public PositionsController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        // GET: Positions
        public async Task<IActionResult> Index()
        {
            return View(await _positionService.GetPositions().Where(p => p.IsActive == true).ToListAsync());
        }

        // GET: Positions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = await Task.FromResult(_positionService.GetPositionById(id.Value));
                
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // GET: Positions/Create
        public IActionResult Create()
        {
            return View("Form");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Position position)
        {
            if (ModelState.IsValid)
            {
                position.IsActive = true;
                await  Task.FromResult(_positionService.InsertPosition(position));
               
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

            var position = await Task.FromResult(_positionService.GetPositionById(id.Value));
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
                    await Task.FromResult(_positionService.UdatePosition(position));
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionExists(position.Id))
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
            if (id == null)
            {
                return NotFound();
            }
            var position =  await Task.FromResult( _positionService.GetPositionById(id.Value) );
            
                
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
            var position = _positionService.GetPositionById(id);
            await Task.FromResult( _positionService.DeletePosition(position));
            
            return RedirectToAction(nameof(Index));
        }

        private bool PositionExists(int id)
        {
            return _positionService.GetPositions().Any(e => e.Id == id);
        }
    }
}
