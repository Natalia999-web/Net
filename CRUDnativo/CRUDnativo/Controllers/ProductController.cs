using CRUDnativo.Data;
using CRUDnativo.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRUDnativo.Controllers
{
    public class ProductController : Controller
    {
        private readonly AplicationDbContext _context;

        public ProductController(AplicationDbContext context)
        {
            _context = context;
        }

        //GET Producto

        public ActionResult Index()
        {
            IEnumerable<Productos> ListaProductos = _context.Producto;
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
                _context.Producto.Add(producto);
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
            var producto = _context.Producto.Find(Nombre);
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
                _context.Producto.Update(producto);
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
            var producto = _context.Producto.Find(Nombre);
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
                _context.Producto.Remove(producto);
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