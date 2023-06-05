using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba_v1.Models.dbModels;
using Prueba_v1.Models.DTO;

namespace Prueba_v1.Controllers
{

    public class ComidumsController : Controller
    {
        private readonly Pia_ProgWebContext _context;

        public ComidumsController(Pia_ProgWebContext context)
        {
            _context = context;
        }

        // GET: Comidums
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var pia_ProgWebContext = _context.Comida.Include(c => c.IdCategoriaNavigation);
            return View(await pia_ProgWebContext.ToListAsync());
        }
        //Prueba

        //Prueba
        //Prueba
        [Authorize(Roles = "Cliente, Admin")]
        public async Task<IActionResult> Inicio()
        {
            var comidas = await _context.Comida.Where(c => c.IdCategoria == 1).Include(c => c.IdCategoriaNavigation).ToListAsync();
            return View(comidas);
        }
        
        [Authorize(Roles = "Cliente, Admin")]
        public async Task<IActionResult> Entradas()
        {
            var comidas = await _context.Comida.Where(c => c.IdCategoria == 2).Include(c => c.IdCategoriaNavigation).ToListAsync();
            return View(comidas);
        }

        public async Task<IActionResult> Bebidas()
        {
            var comidas = await _context.Comida.Where(c => c.IdCategoria == 3).Include(c => c.IdCategoriaNavigation).ToListAsync();
            return View(comidas);
        }
        //Prueba

        // GET: Comidums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Comida == null)
            {
                return NotFound();
            }

            var comidum = await _context.Comida
                .Include(c => c.IdCategoriaNavigation)
                .FirstOrDefaultAsync(m => m.IdComida == id);
            if (comidum == null)
            {
                return NotFound();
            }

            return View(comidum);
        }

        // GET: Comidums/Create
        public IActionResult Create()
        {
            ComidumCreateDTO ccd = new ComidumCreateDTO
            {
                Categorias = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria")
            };

            return View(ccd);
        }
        

        // POST: Comidums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComidumCreateDTO comidum)
        {
            if (ModelState.IsValid)
            {
                Comidum comida = new Comidum 
                { 
                    IdCategoria= comidum.IdCategoria,
                    Nombre = comidum.Nombre,
                    Descripcion = comidum.Descripcion,
                    Imagen = comidum.Imagen,
                    Precio = comidum.Precio ,
                    IdComida = comidum.IdComida 
                };
                
                _context.Add(comida);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Comidums", "Inicio");
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", comidum.IdCategoria);
            return View(comidum);
        }

        // GET: Comidums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Comida == null)
            {
                return NotFound();
            }

            var comidum = await _context.Comida.FindAsync(id);
            if (comidum == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", comidum.IdCategoria);
            return View(comidum);
        }

        // POST: Comidums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdComida,Nombre,Descripcion,Precio,IdCategoria")] Comidum comidum)
        {
            if (id != comidum.IdComida)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comidum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComidumExists(comidum.IdComida))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "IdCategoria", comidum.IdCategoria);
            return View(comidum);
        }

        // GET: Comidums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Comida == null)
            {
                return NotFound();
            }

            var comidum = await _context.Comida
                .Include(c => c.IdCategoriaNavigation)
                .FirstOrDefaultAsync(m => m.IdComida == id);
            if (comidum == null)
            {
                return NotFound();
            }

            return View(comidum);
        }

        // POST: Comidums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Comida == null)
            {
                return Problem("Entity set 'Pia_ProgWebContext.Comida'  is null.");
            }
            var comidum = await _context.Comida.FindAsync(id);
            if (comidum != null)
            {
                _context.Comida.Remove(comidum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComidumExists(int id)
        {
            return (_context.Comida?.Any(e => e.IdComida == id)).GetValueOrDefault();
        }
    }
}
