using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using produccion.Models;

namespace produccion.Controllers
{
    public class TbCategoriaProductoesController : Controller
    {
        private readonly ProduccionContext _context;

        public TbCategoriaProductoesController(ProduccionContext context)
        {
            _context = context;
        }

        // GET: TbCategoriaProductoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbCategoriaProductos.ToListAsync());
        }

        // GET: TbCategoriaProductoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCategoriaProducto = await _context.TbCategoriaProductos
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (tbCategoriaProducto == null)
            {
                return NotFound();
            }

            return View(tbCategoriaProducto);
        }

        // GET: TbCategoriaProductoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbCategoriaProductoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCategoria,NombreCategoria,Descripcion,Estado,FechaCreacion,FechaActualizacion")] TbCategoriaProducto tbCategoriaProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbCategoriaProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbCategoriaProducto);
        }

        // GET: TbCategoriaProductoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCategoriaProducto = await _context.TbCategoriaProductos.FindAsync(id);
            if (tbCategoriaProducto == null)
            {
                return NotFound();
            }
            return View(tbCategoriaProducto);
        }

        // POST: TbCategoriaProductoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCategoria,NombreCategoria,Descripcion,Estado,FechaCreacion,FechaActualizacion")] TbCategoriaProducto tbCategoriaProducto)
        {
            if (id != tbCategoriaProducto.IdCategoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbCategoriaProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbCategoriaProductoExists(tbCategoriaProducto.IdCategoria))
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
            return View(tbCategoriaProducto);
        }

        // GET: TbCategoriaProductoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCategoriaProducto = await _context.TbCategoriaProductos
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (tbCategoriaProducto == null)
            {
                return NotFound();
            }

            return View(tbCategoriaProducto);
        }

        // POST: TbCategoriaProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbCategoriaProducto = await _context.TbCategoriaProductos.FindAsync(id);
            if (tbCategoriaProducto != null)
            {
                _context.TbCategoriaProductos.Remove(tbCategoriaProducto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbCategoriaProductoExists(int id)
        {
            return _context.TbCategoriaProductos.Any(e => e.IdCategoria == id);
        }
        [HttpPost]
        public IActionResult ToggleEstado(int id)
        {
            var categoria = _context.TbCategoriaProductos.FirstOrDefault(c => c.IdCategoria == id);

            if (categoria == null)
                return NotFound();

            categoria.Estado = categoria.Estado == 1 ? (byte)0 : (byte)1;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
