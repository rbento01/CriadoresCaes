using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CriadoresCaes_tA_B.Data;
using CriadoresCaes_tA_B.Models;

namespace CriadoresCaes_tA_B.Controllers
{
    public class CriadoresController : Controller
    {
        private readonly CriadoresCaesDB _context;

        public CriadoresController(CriadoresCaesDB context)
        {
            _context = context;
        }

        // GET: Criadores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Criadores.ToListAsync());
        }

        // GET: Criadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criadores = await _context.Criadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criadores == null)
            {
                return NotFound();
            }

            return View(criadores);
        }

        // GET: Criadores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Criadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,NomeComercial,Morada,CodPostal,Telemovel,Email")] Criadores criadores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(criadores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(criadores);
        }

        // GET: Criadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criadores = await _context.Criadores.FindAsync(id);
            if (criadores == null)
            {
                return NotFound();
            }
            return View(criadores);
        }

        // POST: Criadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,NomeComercial,Morada,CodPostal,Telemovel,Email")] Criadores criadores)
        {
            if (id != criadores.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(criadores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CriadoresExists(criadores.Id))
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
            return View(criadores);
        }

        // GET: Criadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criadores = await _context.Criadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (criadores == null)
            {
                return NotFound();
            }

            return View(criadores);
        }

        // POST: Criadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var criadores = await _context.Criadores.FindAsync(id);
            _context.Criadores.Remove(criadores);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CriadoresExists(int id)
        {
            return _context.Criadores.Any(e => e.Id == id);
        }
    }
}
