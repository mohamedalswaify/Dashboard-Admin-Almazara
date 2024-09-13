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
    public class AdAcceptController : Controller
    {
        private readonly BloggingContext _context;

        public AdAcceptController(BloggingContext context)
        {
            _context = context;
        }

        // GET: AdAccept
        public async Task<IActionResult> Index()
        {




            DateTime currentDate = DateTime.Now.Date; // تاريخ اليوم

            var bloggingContext = _context.Ads
           .Include(a => a.AdType)
           .Include(a => a.Area)
           .Include(a => a.Category)
           .Include(a => a.Customer)
           .AsEnumerable()  // Execute the query on the client side
           .Where(a => a.ExpiryDate >= currentDate && a.AdStatusId == 2 && a.PublishDate != null)  // Filter the results on the client side
           .OrderByDescending(a => a.ExpiryDate);  // Order the results on the client side

            return View(bloggingContext.ToList());





        }

        // GET: AdAccept/Details/5
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

        // GET: AdAccept/Create
        public IActionResult Create()
        {
            ViewData["AdTypeId"] = new SelectList(_context.AdTypes, "Id", "Id");
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id");
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Id");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            return View();
        }

        // POST: AdAccept/Create
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

        // GET: AdAccept/Edit/5
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

        // POST: AdAccept/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Token,Title,Description,CategoryId,AdTypeId,Price,AreaId,MobileNumber,WhatsAppNumber,NameVideo,numberShow,numberOfWhatsApp,numberOfCall,Space,NumberOfRooms,NumberOfBathrooms,CustomerId,StatusId,AddedDate,PublishDate")] AdModel adModel)
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
            ViewData["AdTypeId"] = new SelectList(_context.AdTypes, "Id", "Name", adModel.AdTypeId);
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Name", adModel.AreaId);
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Name", adModel.CategoryId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "username", adModel.CustomerId);
            ViewData["StatusId"] = new SelectList(_context.adStatuses, "Id", "Status", adModel.AdStatusId);
            return View(adModel);
        }

        // GET: AdAccept/Delete/5
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

        // POST: AdAccept/Delete/5
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
