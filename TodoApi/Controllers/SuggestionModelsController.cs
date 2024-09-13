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
    public class SuggestionModelsController : Controller
    {
        private readonly BloggingContext _context;

        public SuggestionModelsController(BloggingContext context)
        {
            _context = context;
        }

        // GET: SuggestionModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Suggestions.ToListAsync());
        }

        // GET: SuggestionModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suggestionModel = await _context.Suggestions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suggestionModel == null)
            {
                return NotFound();
            }

            return View(suggestionModel);
        }

        // GET: SuggestionModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SuggestionModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,email,Text")] SuggestionModel suggestionModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suggestionModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(suggestionModel);
        }

        // GET: SuggestionModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suggestionModel = await _context.Suggestions.FindAsync(id);
            if (suggestionModel == null)
            {
                return NotFound();
            }
            return View(suggestionModel);
        }

        // POST: SuggestionModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,email,Text")] SuggestionModel suggestionModel)
        {
            if (id != suggestionModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suggestionModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuggestionModelExists(suggestionModel.Id))
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
            return View(suggestionModel);
        }

        // GET: SuggestionModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suggestionModel = await _context.Suggestions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suggestionModel == null)
            {
                return NotFound();
            }

            return View(suggestionModel);
        }

        // POST: SuggestionModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suggestionModel = await _context.Suggestions.FindAsync(id);
            if (suggestionModel != null)
            {
                _context.Suggestions.Remove(suggestionModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuggestionModelExists(int id)
        {
            return _context.Suggestions.Any(e => e.Id == id);
        }
        public IActionResult GetDetails(int id)
        {
            // منطق استرجاع البيانات بناءً على id
            var data = _context.Suggestions.Find(id);

            if (data != null)
            {
                return Ok(new { data = data.Text });
            }
            else
            {
                return NotFound(new { message = "التفاصيل غير موجودة" });
            }
        }
        public IActionResult IsReadState(int id)
        {
            var isRead = _context.Suggestions.Find(id);


            if (isRead != null)
            {
                isRead.isRead = true;
                _context.Suggestions.Update(isRead);
                _context.SaveChanges();
                return Ok(new { success = true });
            }
            else
            {
                return BadRequest(new { success = false });
            }
        }
    }
}
