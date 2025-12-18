using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechNova.Models;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace TechNova.Controllers
{
    public class VentasController : Controller
    {
        private readonly TechNovaContext _context;

        public VentasController(TechNovaContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.VentaDetalles)
                    .ThenInclude(d => d.Producto)
                .ToListAsync();

            return View(ventas);
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.VentaDetalles)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venta == null) return NotFound();

            return View(venta);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            var vm = new NuevaVentaViewModel
            {
                Productos = _context.Productos
                    .Select(p => new ProductoSeleccionado
                    {
                        ProductoId = p.Id,
                        Nombre = p.Nombre,
                        Precio = p.Precio,
                        Stock = p.Stock,
                        Cantidad = 1,
                        Seleccionado = false
                    }).ToList()
            };

            ViewBag.ClienteId = new SelectList(_context.Clientes, "Id", "NombreCompleto");
            return View(vm);
        }

        // POST: Ventas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NuevaVentaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ClienteId = new SelectList(_context.Clientes, "Id", "NombreCompleto");
                return View(model);
            }

            var productosSeleccionados = model.Productos.Where(p => p.Seleccionado).ToList();
            if (!productosSeleccionados.Any())
            {
                ModelState.AddModelError("", "Debe seleccionar al menos un producto.");
                ViewBag.ClienteId = new SelectList(_context.Clientes, "Id", "NombreCompleto");
                return View(model);
            }

            // Validar stock disponible
            foreach (var p in productosSeleccionados)
            {
                var productoReal = await _context.Productos.FindAsync(p.ProductoId);
                if (productoReal == null)
                {
                    ModelState.AddModelError("", $"El producto {p.Nombre} no existe.");
                    ViewBag.ClienteId = new SelectList(_context.Clientes, "Id", "NombreCompleto");
                    return View(model);
                }
                if (p.Cantidad > productoReal.Stock)
                {
                    ModelState.AddModelError("", $"No hay suficiente stock de {p.Nombre}. Stock disponible: {productoReal.Stock}");
                    ViewBag.ClienteId = new SelectList(_context.Clientes, "Id", "NombreCompleto");
                    return View(model);
                }
            }

            // Calcular total de la venta
            decimal totalVenta = productosSeleccionados.Sum(p => p.Cantidad * p.Precio);

            // Crear la venta
            var venta = new Venta
            {
                ClienteId = model.ClienteId,
                Fecha = DateTime.Now,
                Total = totalVenta
            };

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            // Crear los detalles y reducir stock
            foreach (var p in productosSeleccionados)
            {
                // Crear detalle
                var detalle = new VentaDetalle
                {
                    VentaId = venta.Id,
                    ProductoId = p.ProductoId,
                    Cantidad = p.Cantidad,
                    Precio = p.Precio,
                    Subtotal = p.Cantidad * p.Precio
                };
                _context.VentaDetalles.Add(detalle);

                // Reducir stock
                var productoReal = await _context.Productos.FindAsync(p.ProductoId);
                productoReal.Stock -= p.Cantidad;
                if (productoReal.Stock < 0) productoReal.Stock = 0;
                _context.Update(productoReal);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var venta = await _context.Ventas
                .Include(v => v.VentaDetalles)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venta == null) return NotFound();

            ViewBag.ClienteId = new SelectList(_context.Clientes, "Id", "NombreCompleto", venta.ClienteId);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,Fecha,Total")] Venta venta)
        {
            if (id != venta.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ventas.Any(e => e.Id == venta.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ClienteId = new SelectList(_context.Clientes, "Id", "NombreCompleto", venta.ClienteId);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.VentaDetalles)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venta == null) return NotFound();

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.VentaDetalles)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venta != null)
            {
                _context.VentaDetalles.RemoveRange(venta.VentaDetalles);
                _context.Ventas.Remove(venta);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
