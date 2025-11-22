using AngieGiraldo.Data;
using AngieGiraldo.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AngieGiraldo.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET Producto

        public ActionResult Index()
        {
            IEnumerable<Productos> ListaProductos = _context.Productos;
            return View(ListaProductos);
        }

        //create GET

        public IActionResult Create()
        {
            return View();
        }

        //Create POST
        [HttpPost]
        public IActionResult Create(Productos producto)
        {
            if (ModelState.IsValid)
            {
                _context.Productos.Add(producto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        //http edit GET

        public IActionResult Edit(string? Nombre)
        {
            if (Nombre == null || Nombre.Length == 0)
            {
                return NotFound();
            }
            var producto = _context.Productos.Find(Nombre);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        //http edit POST
        [HttpPost]

        public IActionResult Edit(Productos producto)
        {
            if (ModelState.IsValid)
            {
                _context.Productos.Update(producto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);

        }

        public IActionResult Delete(string? Nombre)
        {
            if (Nombre == null || Nombre.Length == 0)
            {
                return NotFound();
            }
            var producto = _context.Productos.Find(Nombre);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }


        [HttpPost]

        public IActionResult Delete(Productos producto)
        {
            if (ModelState.IsValid)
            {
                _context.Productos.Remove(producto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}