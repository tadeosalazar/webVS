using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba_v1.Models.dbModels;

namespace Prueba_v1.Controllers
{
    [Authorize]
    public class CarritoController : Controller
    {
        private readonly Pia_ProgWebContext _context;

        public CarritoController(Pia_ProgWebContext context)
        {
            _context = context;
        }

        // GET: Carrito
       
        public async Task<IActionResult> Index()
        {
            var carritos = await _context.Carritos
                .Include(c => c.Comida)
                .Include(c => c.Usuario)
                .ToListAsync();

            return View(carritos);
        }
     
        public async Task<IActionResult> Inicio()
        {
            var pia_ProgWebContext = _context.Carritos.Include(c => c.IdComidaNavigation).Include(c => c.IdUsusario1).Include(c => c.IdUsusarioNavigation);
            return View(await pia_ProgWebContext.ToListAsync());
        }

        // GET: Carrito/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carritos == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos
                .Include(c => c.IdComidaNavigation)
                .Include(c => c.IdUsusario1)
                .Include(c => c.IdUsusarioNavigation)
                .FirstOrDefaultAsync(m => m.IdUsusario == id);
            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        //EDIT Y DELETE

        //PRUEBA
        // GET: Carrito/Create
        public IActionResult Create()
        {
            ViewData["IdComida"] = new SelectList(_context.Comida, "IdComida", "Nombre");
            ViewData["IdUsusario"] = new SelectList(_context.Users, "Id", "UserName");

            return View();
        }

        // POST: Carrito/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsusario, IdComida, Cantidad")] Carrito carrito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrito);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["IdComida"] = new SelectList(_context.Comida, "IdComida", "Nombre", carrito.IdComida);
            ViewData["IdUsusario"] = new SelectList(_context.Users, "Id", "UserName", carrito.IdUsusario);

            return View(carrito);
        }


        // GET: Carrito/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos.FindAsync(id);

            if (carrito == null)
            {
                return NotFound();
            }

            ViewData["IdComida"] = new SelectList(_context.Comida, "IdComida", "Nombre", carrito.IdComida);
            ViewData["IdUsusario"] = new SelectList(_context.Users, "Id", "UserName", carrito.IdUsusario);

            return View(carrito);
        }
        // POST: Carrito/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCarrito,IdUsusario,IdComida,Cantidad")] Carrito carrito)
        {
            if (id != carrito.IdCarrito)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carrito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoExists(carrito.IdCarrito))
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

            ViewData["IdComida"] = new SelectList(_context.Comida, "IdComida", "Nombre", carrito.IdComida);
            ViewData["IdUsusario"] = new SelectList(_context.Users, "Id", "UserName", carrito.IdUsusario);

            return View(carrito);
        }

        // GET: Carrito/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos
                .Include(c => c.Comida)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.IdCarrito == id);

            if (carrito == null)
            {
                return NotFound();
            }

            return View(carrito);
        }

        // POST: Carrito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carrito = await _context.Carritos.FindAsync(id);
            _context.Carritos.Remove(carrito);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
