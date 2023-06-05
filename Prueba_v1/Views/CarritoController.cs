using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba_v1.Models.dbModels;

namespace Prueba_v1.Views
{

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

        // GET: Carrito/Create
        public IActionResult Create()
        {
            ViewData["IdComida"] = new SelectList(_context.Comida, "IdComida", "IdComida");
            ViewData["IdUsusario"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["IdUsusario"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido");
            return View();
        }

        // POST: Carrito/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsusario,IdComida,Cantidad")] Carrito carrito)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carrito);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdComida"] = new SelectList(_context.Comida, "IdComida", "IdComida", carrito.IdComida);
            ViewData["IdUsusario"] = new SelectList(_context.Users, "Id", "Id", carrito.IdUsusario);
            ViewData["IdUsusario"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", carrito.IdUsusario);
            return View(carrito);
        }

        // GET: Carrito/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carritos == null)
            {
                return NotFound();
            }

            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }
            ViewData["IdComida"] = new SelectList(_context.Comida, "IdComida", "IdComida", carrito.IdComida);
            ViewData["IdUsusario"] = new SelectList(_context.Users, "Id", "Id", carrito.IdUsusario);
            ViewData["IdUsusario"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", carrito.IdUsusario);
            return View(carrito);
        }

        // POST: Carrito/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsusario,IdComida,Cantidad")] Carrito carrito)
        {
            if (id != carrito.IdUsusario)
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
                    if (!CarritoExists(carrito.IdUsusario))
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
            ViewData["IdComida"] = new SelectList(_context.Comida, "IdComida", "IdComida", carrito.IdComida);
            ViewData["IdUsusario"] = new SelectList(_context.Users, "Id", "Id", carrito.IdUsusario);
            ViewData["IdUsusario"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", carrito.IdUsusario);
            return View(carrito);
        }

        // GET: Carrito/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Carrito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carritos == null)
            {
                return Problem("Entity set 'Pia_ProgWebContext.Carritos'  is null.");
            }
            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito != null)
            {
                _context.Carritos.Remove(carrito);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoExists(int id)
        {
          return (_context.Carritos?.Any(e => e.IdUsusario == id)).GetValueOrDefault();
        }
    }
}
