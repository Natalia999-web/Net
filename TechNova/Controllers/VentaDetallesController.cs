using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechNova.Models;
using System.Linq;
using System.Threading.Tasks;

public class VentaDetallesController : Controller
{
    private readonly TechNovaContext _context;

    public VentaDetallesController(TechNovaContext context)
    {
        _context = context;
    }

    // GET: VentaDetalles
    public async Task<IActionResult> Index()
    {
        var detalles = _context.VentaDetalles
            .Include(d => d.Producto)
            .Include(d => d.Venta);
        return View(await detalles.ToListAsync());
    }

    // GET: VentaDetalles/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var detalle = await _context.VentaDetalles
            .Include(d => d.Producto)
            .Include(d => d.Venta)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (detalle == null) return NotFound();

        return View(detalle);
    }

    // GET: VentaDetalles/Create
    public IActionResult Create()
    {
        ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "NombreCompleto");
        ViewData["Productos"] = _context.Productos.ToList();
        return View();
    }

    // POST: VentaDetalles/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("VentaId,ProductoId,Cantidad,PrecioUnitario,Subtotal")] VentaDetalle detalle)
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

    // GET: VentaDetalles/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var detalle = await _context.VentaDetalles.FindAsync(id);
        if (detalle == null) return NotFound();

        ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", detalle.ProductoId);
        ViewData["VentaId"] = new SelectList(_context.Ventas, "Id", "Id", detalle.VentaId);
        return View(detalle);
    }

    // POST: VentaDetalles/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,VentaId,ProductoId,Cantidad,PrecioUnitario,Subtotal")] VentaDetalle detalle)
    {
        if (id != detalle.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(detalle);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentaDetalleExists(detalle.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }

        ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", detalle.ProductoId);
        ViewData["VentaId"] = new SelectList(_context.Ventas, "Id", "Id", detalle.VentaId);
        return View(detalle);
    }

    // GET: VentaDetalles/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var detalle = await _context.VentaDetalles
            .Include(d => d.Producto)
            .Include(d => d.Venta)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (detalle == null) return NotFound();

        return View(detalle);
    }

    // POST: VentaDetalles/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var detalle = await _context.VentaDetalles.FindAsync(id);
        if (detalle != null)
        {
            _context.VentaDetalles.Remove(detalle);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool VentaDetalleExists(int id)
    {
        return _context.VentaDetalles.Any(e => e.Id == id);
    }
}
