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
    public class AdArciveController : Controller
    {
        private readonly BloggingContext _context;

        public AdArciveController(BloggingContext context)
        {
            _context = context;
        }

        // GET: AdArcive
        public async Task<IActionResult> Index()
        {
            DateTime dateTime = DateTime.Now;

            var bloggingContext = _context.Ads
                .Include(a => a.AdType)
                .Include(a => a.Area)
                .Include(a => a.Category)
                .Include(a => a.Customer)
                .AsEnumerable()
                .Where(a => a.AdStatusId == 2 && a.PublishDate != null && a.ExpiryDate < dateTime) // يتم فلترة الحالة لتكون  2
                .OrderByDescending(a => a.Id) // ترتيب النتائج تنازلياً حسب الـ ID
                .ToList(); // Convert to list

            return View(bloggingContext); // Pass the list directly to the view
        }


        // GET: AdArcive/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adModel = await _context.Ads
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

        // GET: AdArcive/Create
        public IActionResult Create()
        {
            ViewData["AdTypeId"] = new SelectList(_context.AdTypes, "Id", "Id");
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id");
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Id");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            return View();
        }

        // POST: AdArcive/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Token,Title,Description,CategoryId,AdTypeId,Price,AreaId,MobileNumber,WhatsAppNumber,NameVideo,numberShow,numberOfWhatsApp,numberOfCall,Space,NumberOfRooms,NumberOfBathrooms,CustomerId,StatusId,AddedDate,PublishDate")] AdModel adModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdTypeId"] = new SelectList(_context.AdTypes, "Id", "Name", adModel.AdTypeId);
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Name", adModel.AreaId);
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Name", adModel.CategoryId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "username", adModel.CustomerId);
            ViewData["StatusId"] = new SelectList(_context.adStatuses, "Id", "Status", adModel.AdStatusId);
            return View(adModel);
        }

        // GET: AdArcive/Edit/5
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
            ViewData["AdTypeId"] = new SelectList(_context.AdTypes, "Id", "Name", adModel.AdTypeId);
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Name", adModel.AreaId);
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Name", adModel.CategoryId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "username", adModel.CustomerId);
            ViewData["StatusId"] = new SelectList(_context.adStatuses, "Id", "Status", adModel.AdStatusId);
            return View(adModel);
        }

        // POST: AdArcive/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Token,Title,Description,CategoryId,AdTypeId,Price,AreaId,MobileNumber,WhatsAppNumber,NameVideo,numberShow,numberOfWhatsApp,numberOfCall,Space,NumberOfRooms,NumberOfBathrooms,CustomerId,AdStatusId,AddedDate,PublishDate")] AdModel adModel)
        {
            if (id != adModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (adModel.AdStatusId == 2 && adModel.AdTypeId == 1)
                    {
                        var latestAd = await _context.Ads
                            .Where(a => a.AdTypeId == 1 && a.AdStatusId == 2 && a.IsHide == true && a.ExpiryDate>=DateTime.Now)
                            .OrderByDescending(a => a.PublishDate)
                            .FirstOrDefaultAsync();

                        if (latestAd != null)
                        {
                            var expiryDate = latestAd.ExpiryDate.Value.AddDays(1);
                            adModel.PublishDate = expiryDate;
                        }
                        else
                        {
                            adModel.PublishDate = DateTime.Now;

                        }
                    }
                    else if (adModel.AdStatusId == 2 && adModel.AdTypeId == 2)
                    {
                        var latestAd = await _context.Ads
                            .Where(a => a.AdTypeId == 2 && a.CategoryId == adModel.CategoryId && a.AreaId == adModel.AreaId && a.AdStatusId == 2 && a.IsHide == true && a.ExpiryDate >= DateTime.Now)
                            .OrderByDescending(a => a.PublishDate)
                            .FirstOrDefaultAsync();

                        if (latestAd != null)
                        {
                            var expiryDate = latestAd.ExpiryDate.Value.AddDays(1);
                            adModel.PublishDate = expiryDate;
                        }
                        else
                        {
                            adModel.PublishDate = DateTime.Now;

                        }
                    }
                    else if (adModel.AdStatusId == 2 && adModel.AdTypeId == 3)
                    {
                        var latestAd = await _context.Ads
                            .Where(a => a.AdTypeId == 3 && a.CategoryId == adModel.CategoryId && a.AreaId == adModel.AreaId && a.AdStatusId == 2 && a.IsHide == true && a.ExpiryDate >= DateTime.Now)
                            .OrderByDescending(a => a.PublishDate)
                            .FirstOrDefaultAsync();

                        if (latestAd != null)
                        {
                            var expiryDate = latestAd.ExpiryDate.Value.AddDays(1);
                            adModel.PublishDate = expiryDate;
                        }
                        else
                        {
                            adModel.PublishDate = DateTime.Now;

                        }
                    }
                    else if (adModel.AdStatusId == 2 && (adModel.AdTypeId == 4 || adModel.AdTypeId == 5))
                    {
                        adModel.PublishDate = DateTime.Now;
                    }

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
            ViewData["AdTypeId"] = new SelectList(_context.AdTypes, "Id", "Name", adModel.AdTypeId);
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Name", adModel.AreaId);
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Name", adModel.CategoryId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "username", adModel.CustomerId);
            ViewData["StatusId"] = new SelectList(_context.adStatuses, "Id", "Status", adModel.AdStatusId);
            return View(adModel);
        }

        // GET: AdArcive/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adModel = await _context.Ads
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

        // POST: AdArcive/Delete/5
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
