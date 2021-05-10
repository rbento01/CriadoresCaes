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
    public class VeterinariosController : Controller
    {
        private readonly CriadoresCaesDB _context;

        public VeterinariosController(CriadoresCaesDB context)
        {
            _context = context;
        }

        // GET: Veterinarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Veterinarios.ToListAsync());
        }

        // GET: Veterinarios/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinarios = await _context.Veterinarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veterinarios == null)
            {
                return NotFound();
            }

            return View(veterinarios);
        }

        // GET: Veterinarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Veterinarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Honorarios")] Veterinarios veterinario)
        {
            if (ModelState.IsValid)
            {
                //adicionar o valor do HonorárioAux aos dados a guardar na Base de Dados
                // converter o valor HonorarioAux para um formato que seja compreendido pela função 'Convert'

                veterinario.HonorarioAux = veterinario.HonorarioAux.Replace('.', '.');
                // converter e adicionar aos dados que serão guardados na BD
                veterinario.Honorarios = Convert.ToDecimal(veterinario.HonorarioAux);

                // será que o ID que o utilizador forneceu pode ser usado?
                // procurar na BD se o ID proposto já existe
                // SELECT *
                // FROM Veterinarios
                // WHERE Id = a um valor fornecido pelo utilizador
                // v => v.Id == veterinario.Id
                var _auxVeterinario = await _context.Veterinarios.FirstOrDefaultAsync(veterinario => veterinario.Id == veterinario.Id);
                // avaliar se posso inserir os dados
                if (_auxVeterinario == null) {
                    try {
                        _context.Add(veterinario);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    } catch (Exception ex) {
                        // adicionar os dados do erro numa tabela na BD
                        // - nome da pessoa q está a usar a app
                        // - data + hora do erro
                        // - nome do controller
                        // - nome do método
                        // - guardar o conteúdo do ex. Message
                        // - e do ex.StackTrace

                        // escrever os mesmos dados num ficheiro no disco rigido do servidor
                    }
                    
                }
            } else {
                // o ID já existe na BD
                // devolver o controlo à View
                ModelState.AddModelError("", "O código do veterinário proposto já existe. Adicione um outro, por favor.");
            }
            return View(veterinario);
        }

        // GET: Veterinarios/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinarios = await _context.Veterinarios.FindAsync(id);
            if (veterinarios == null)
            {
                return NotFound();
            }
            return View(veterinarios);
        }

        // POST: Veterinarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nome,Honorarios")] Veterinarios veterinarios)
        {
            if (id != veterinarios.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veterinarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinariosExists(veterinarios.Id))
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
            return View(veterinarios);
        }

        // GET: Veterinarios/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinarios = await _context.Veterinarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veterinarios == null)
            {
                return NotFound();
            }

            return View(veterinarios);
        }

        // POST: Veterinarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var veterinarios = await _context.Veterinarios.FindAsync(id);
            _context.Veterinarios.Remove(veterinarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinariosExists(string id)
        {
            return _context.Veterinarios.Any(e => e.Id == id);
        }
    }
}
