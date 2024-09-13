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
    public class AllAdTestController : Controller
    {
        private readonly BloggingContext _context;

        public AllAdTestController(BloggingContext context)
        {
            _context = context;
        }

        // GET: AllAdTest
        public async Task<IActionResult> Index()
        {
            var bloggingContext = _context.Ads.Include(a => a.AdStatus).Include(a => a.AdType).Include(a => a.Area).Include(a => a.Category).Include(a => a.Customer);
            return View(await bloggingContext.ToListAsync());
        }

        // GET: AllAdTest/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adModel = await _context.Ads
                .Include(a => a.AdStatus)
                .Include(a => a.AdType)
                .Include(a => a.Area)
                .Include(a => a.Category)
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adModel == null)
            {
                return NotFound();
            }

            return View(adModel);
        }

        // GET: AllAdTest/Create
        public IActionResult Create()
        {
            ViewData["AdStatusId"] = new SelectList(_context.adStatuses, "Id", "Id");
            ViewData["AdTypeId"] = new SelectList(_context.AdTypes, "Id", "Id");
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id");
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Id");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            return View();
        }

        // POST: AllAdTest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Token,Title,Description,CategoryId,AdTypeId,Price,AreaId,MobileNumber,WhatsAppNumber,NameVideo,numberShow,numberOfWhatsApp,numberOfCall,IsHide,Space,NumberOfRooms,NumberOfBathrooms,CustomerId,AdStatusId,AddedDate,PublishDate")] AdModel adModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdStatusId"] = new SelectList(_context.adStatuses, "Id", "Id", adModel.AdStatusId);
            ViewData["AdTypeId"] = new SelectList(_context.AdTypes, "Id", "Id", adModel.AdTypeId);
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id", adModel.AreaId);
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Id", adModel.CategoryId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", adModel.CustomerId);
            return View(adModel);
        }

        // GET: AllAdTest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adModel = await _context.Ads.FindAsync(id);
            if (adModel == null)
            {
                return NotFound();
            }
            ViewData["AdStatusId"] = new SelectList(_context.adStatuses, "Id", "Id", adModel.AdStatusId);
            ViewData["AdTypeId"] = new SelectList(_context.AdTypes, "Id", "Id", adModel.AdTypeId);
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id", adModel.AreaId);
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Id", adModel.CategoryId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", adModel.CustomerId);
            return View(adModel);
        }

        // POST: AllAdTest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Token,Title,Description,CategoryId,AdTypeId,Price,AreaId,MobileNumber,WhatsAppNumber,NameVideo,numberShow,numberOfWhatsApp,numberOfCall,IsHide,Space,NumberOfRooms,NumberOfBathrooms,CustomerId,AdStatusId,AddedDate,PublishDate")] AdModel adModel)
        {
            if (id != adModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdModelExists(adModel.Id))
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
            ViewData["AdStatusId"] = new SelectList(_context.adStatuses, "Id", "Id", adModel.AdStatusId);
            ViewData["AdTypeId"] = new SelectList(_context.AdTypes, "Id", "Id", adModel.AdTypeId);
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id", adModel.AreaId);
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Id", adModel.CategoryId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", adModel.CustomerId);
            return View(adModel);
        }

        // GET: AllAdTest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adModel = await _context.Ads
                .Include(a => a.AdStatus)
                .Include(a => a.AdType)
                .Include(a => a.Area)
                .Include(a => a.Category)
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adModel == null)
            {
                return NotFound();
            }

            return View(adModel);
        }

        // POST: AllAdTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adModel = await _context.Ads.FindAsync(id);
            if (adModel != null)
            {
                _context.Ads.Remove(adModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdModelExists(int id)
        {
            return _context.Ads.Any(e => e.Id == id);
        }
    }
}
