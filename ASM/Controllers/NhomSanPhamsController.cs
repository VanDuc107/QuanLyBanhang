using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASM_NET104Contenxt.Models;
using ASM_NET104.Models;

namespace ASM_NET104Contenxt.Controllers
{
    public class NhomSanPhamsController : Controller
    {
        private readonly ASM_NET104Context _context;

        public NhomSanPhamsController(ASM_NET104Context context)
        {
            _context = context;
        }

        // GET: NhomSanPhams
        public async Task<IActionResult> Index()
        {
            return View(await _context.NhomSanPham.ToListAsync());
        }

        // GET: NhomSanPhams/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhomSanPham = await _context.NhomSanPham
                .FirstOrDefaultAsync(m => m.MaNhom == id);
            if (nhomSanPham == null)
            {
                return NotFound();
            }

            return View(nhomSanPham);
        }

        // GET: NhomSanPhams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NhomSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNhom,TenNhom")] NhomSanPham nhomSanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhomSanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhomSanPham);
        }

        // GET: NhomSanPhams/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhomSanPham = await _context.NhomSanPham.FindAsync(id);
            if (nhomSanPham == null)
            {
                return NotFound();
            }
            return View(nhomSanPham);
        }

        // POST: NhomSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNhom,TenNhom")] NhomSanPham nhomSanPham)
        {
            if (id != nhomSanPham.MaNhom)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhomSanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhomSanPhamExists(nhomSanPham.MaNhom))
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
            return View(nhomSanPham);
        }

        // GET: NhomSanPhams/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhomSanPham = await _context.NhomSanPham
                .FirstOrDefaultAsync(m => m.MaNhom == id);
            if (nhomSanPham == null)
            {
                return NotFound();
            }

            return View(nhomSanPham);
        }

        // POST: NhomSanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhomSanPham = await _context.NhomSanPham.FindAsync(id);
            _context.NhomSanPham.Remove(nhomSanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhomSanPhamExists(int id)
        {
            return _context.NhomSanPham.Any(e => e.MaNhom == id);
        }
    }
}