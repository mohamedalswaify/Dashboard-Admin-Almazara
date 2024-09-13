using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminControl.Models;

namespace AdminControl.Controllers
{
    public class AttachmentModelsController : Controller
    {
        private readonly BloggingContext _context;

        public AttachmentModelsController(BloggingContext context)
        {
            _context = context;
        }

        // GET: AttachmentModels
        public async Task<IActionResult> Index()
        {
            var bloggingContext = _context.Attachments.Include(a => a.Ad);
            return View(await bloggingContext.ToListAsync());
        }

        // GET: AttachmentModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachmentModel = await _context.Attachments
                .Include(a => a.Ad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attachmentModel == null)
            {
                return NotFound();
            }

            return View(attachmentModel);
        }

        // GET: AttachmentModels/Create
        public IActionResult Create()
        {
            ViewData["AdId"] = new SelectList(_context.Ads, "Id", "Id");
            return View();
        }

        // POST: AttachmentModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,nameimage,AdId")] AttachmentModel attachmentModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attachmentModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdId"] = new SelectList(_context.Ads, "Id", "Id", attachmentModel.AdId);
            return View(attachmentModel);
        }

        // GET: AttachmentModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachmentModel = await _context.Attachments.FindAsync(id);
            if (attachmentModel == null)
            {
                return NotFound();
            }
            ViewData["AdId"] = new SelectList(_context.Ads, "Id", "Id", attachmentModel.AdId);
            return View(attachmentModel);
        }

        // POST: AttachmentModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,nameimage,AdId")] AttachmentModel attachmentModel)
        {
            if (id != attachmentModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attachmentModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttachmentModelExists(attachmentModel.Id))
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
            ViewData["AdId"] = new SelectList(_context.Ads, "Id", "Id", attachmentModel.AdId);
            return View(attachmentModel);
        }

        // GET: AttachmentModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachmentModel = await _context.Attachments
                .Include(a => a.Ad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attachmentModel == null)
            {
                return NotFound();
            }

            return View(attachmentModel);
        }

        // POST: AttachmentModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attachmentModel = await _context.Attachments.FindAsync(id);
            if (attachmentModel != null)
            {
                _context.Attachments.Remove(attachmentModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttachmentModelExists(int id)
        {
            return _context.Attachments.Any(e => e.Id == id);
        }
    }
}
