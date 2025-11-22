using AngieGiraldo.Data;
using AngieGiraldo.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AngieGiraldo.Controllers
{
    public class VentaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VentaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Venta
        public IActionResult Index()
        {
            var listaVentas = _context.Ventas
                .Select(v => new
                {
                    v.IdVenta,
                    v.FechaVenta,
                    v.Cantidad,
                    v.Total,
                    ClienteNombre = v.Cliente.Nombre,
                    ProductoNombre = v.Producto.Nombre
                })
                .ToList();

            return View(listaVentas);
        }

        // GET: Create
        public IActionResult Create()
        {
            ViewBag.Clientes = new SelectList(_context.Clientes, "IdCliente", "Nombre");
            ViewBag.Productos = new SelectList(_context.Productos, "IdProducto", "Nombre");
            return View();
        }

        // POST: Create
        [HttpPost]
        public IActionResult Create(Venta venta)
        {
            if (ModelState.IsValid)
            {
                _context.Ventas.Add(venta);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Clientes = new SelectList(_context.Clientes, "IdCliente", "Nombre", venta.IdCliente);
            ViewBag.Productos = new SelectList(_context.Productos, "IdProducto", "Nombre", venta.IdProducto);
            return View(venta);
        }

        // GET: Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var venta = _context.Ventas.Find(id);
            if (venta == null)
                return NotFound();

            ViewBag.Clientes = new SelectList(_context.Clientes, "IdCliente", "Nombre", venta.IdCliente);
            ViewBag.Productos = new SelectList(_context.Productos, "IdProducto", "Nombre", venta.IdProducto);

            return View(venta);
        }

        // POST: Edit
        [HttpPost]
        public IActionResult Edit(Venta venta)
        {
            if (ModelState.IsValid)
            {
                _context.Ventas.Update(venta);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Clientes = new SelectList(_context.Clientes, "IdCliente", "Nombre", venta.IdCliente);
            ViewBag.Productos = new SelectList(_context.Productos, "IdProducto", "Nombre", venta.IdProducto);

            return View(venta);
        }

        // GET: Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var venta = _context.Ventas.Find(id);
            if (venta == null)
                return NotFound();

            return View(venta);
        }

        // POST: Delete
        [HttpPost]
        public IActionResult Delete(Venta venta)
        {
            if (ModelState.IsValid)
            {
                _context.Ventas.Remove(venta);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(venta);
        }
    }
}
