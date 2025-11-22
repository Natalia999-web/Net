using AngieGiraldo.Data;
using AngieGiraldo.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngieGiraldo.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cliente
        public IActionResult Index()
        {
            IEnumerable<Cliente> listaClientes = _context.Clientes;
            return View(listaClientes);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var cliente = _context.Clientes.Find(id);
            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        // POST: Edit
        [HttpPost]
        public IActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Clientes.Update(cliente);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var cliente = _context.Clientes.Find(id);
            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        // POST: Delete
        [HttpPost]
        public IActionResult Delete(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }
    }
}
