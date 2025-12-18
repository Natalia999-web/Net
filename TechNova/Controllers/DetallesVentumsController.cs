using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechNova.Models;

namespace TechNova.Controllers
{
    public class DetallesVentumsController : Controller
    {
        private readonly TechNovaContext _context;

        public DetallesVentumsController(TechNovaContext context)
        {
            _context = context;
        }

        // GET: DetallesVentums
        public async Task<IActionResult> Index()
        {
            var detalles = _context.VentaDetalles
                .Include(d => d.Producto)
                .Include(d => d.Venta);
            return View(await detalles.ToListAsync());
        }

        // GET: DetallesVentums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var detalle = await _context.VentaDetalles
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (detalle == null)
                return NotFound();

            return View(detalle);
        }

        // GET: DetallesVentums/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre");
            ViewData["VentaId"] = new SelectList(_context.Ventas, "Id", "Id");
            return View();
        }

        // POST: DetallesVentums/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VentaId,ProductoId,Cantidad,Precio,Subtotal")] VentaDetalle detalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", detalle.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "Id", "Id", detalle.VentaId);
            return View(detalle);
        }

        // GET: DetallesVentums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var detalle = await _context.VentaDetalles.FindAsync(id);
            if (detalle == null)
                return NotFound();

            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", detalle.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "Id", "Id", detalle.VentaId);
            return View(detalle);
        }

        // POST: DetallesVentums/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VentaId,ProductoId,Cantidad,Precio,Subtotal")] VentaDetalle detalle)
        {
            if (id != detalle.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleExists(detalle.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", detalle.ProductoId);
            ViewData["VentaId"] = new SelectList(_context.Ventas, "Id", "Id", detalle.VentaId);
            return View(detalle);
        }

        // GET: DetallesVentums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var detalle = await _context.VentaDetalles
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (detalle == null)
                return NotFound();

            return View(detalle);
        }

        // POST: DetallesVentums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalle = await _context.VentaDetalles.FindAsync(id);
            if (detalle != null)
                _context.VentaDetalles.Remove(detalle);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleExists(int id)
        {
            return _context.VentaDetalles.Any(e => e.Id == id);
        }
    }
}
