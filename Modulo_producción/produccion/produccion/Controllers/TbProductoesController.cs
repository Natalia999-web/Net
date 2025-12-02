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
    public class TbProductoesController : Controller
    {
        private readonly ProduccionContext _context;

        public TbProductoesController(ProduccionContext context)
        {
            _context = context;
        }

        // GET: TbProductoes
        public async Task<IActionResult> Index()
        {
            var produccionContext = _context.TbProductos.Include(t => t.IdCategoriaNavigation);

            ViewBag.Categorias = await _context.TbCategoriaProductos.ToListAsync();
            return View(await produccionContext.ToListAsync());
        }

        // GET: TbProductoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProducto = await _context.TbProductos
                .Include(t => t.IdCategoriaNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);

            if (tbProducto == null)
            {
                return NotFound();
            }

            return View(tbProducto);
        }

        // GET: TbProductoes/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] =
                new SelectList(_context.TbCategoriaProductos, "IdCategoria", "NombreCategoria");

            return View();
        }

        // POST: TbProductoes/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TbProducto tbProducto)
        {
            if (ModelState.IsValid)
            {
                tbProducto.FechaCreacion = DateTime.Now;
                tbProducto.FechaActualizacion = DateTime.Now;

                // NO sobrescribas Estado aquí, ya viene del formulario.

                _context.Add(tbProducto);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["IdCategoria"] =
                new SelectList(_context.TbCategoriaProductos, "IdCategoria", "NombreCategoria", tbProducto.IdCategoria);

            return View(tbProducto);
        }

        // GET: TbProductoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProducto = await _context.TbProductos.FindAsync(id);
            if (tbProducto == null)
            {
                return NotFound();
            }

            ViewData["IdCategoria"] =
                new SelectList(_context.TbCategoriaProductos, "IdCategoria", "NombreCategoria", tbProducto.IdCategoria);

            return View(tbProducto);
        }

        // POST: TbProductoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,NombreProducto,Descripcion,Precio,Stock,Estado,IdCategoria")] TbProducto tbProducto)
        {
            if (id != tbProducto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tbProducto.FechaActualizacion = DateTime.Now;

                    _context.Update(tbProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbProductoExists(tbProducto.IdProducto))
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

            ViewData["IdCategoria"] =
                new SelectList(_context.TbCategoriaProductos, "IdCategoria", "NombreCategoria", tbProducto.IdCategoria);

            return View(tbProducto);
        }

        // GET: TbProductoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProducto = await _context.TbProductos
                .Include(t => t.IdCategoriaNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);

            if (tbProducto == null)
            {
                return NotFound();
            }

            return View(tbProducto);
        }

        // POST: TbProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbProducto = await _context.TbProductos.FindAsync(id);

            if (tbProducto != null)
            {
                _context.TbProductos.Remove(tbProducto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbProductoExists(int id)
        {
            return _context.TbProductos.Any(e => e.IdProducto == id);
        }
    }
}
