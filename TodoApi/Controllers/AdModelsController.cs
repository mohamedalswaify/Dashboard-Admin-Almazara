using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminControl.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Mail;
using System.Net.Http.Headers;

namespace AdminControl.Controllers
{
    public class AdModelsController : Controller
    {
        private readonly BloggingContext _context;
        private readonly IWebHostEnvironment _environment;

        public AdModelsController(BloggingContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }
        public async Task<IActionResult> Index()
        {
            var sqlQuery = @"SELECT * FROM Ads WHERE PublishDate <= DATEADD(DAY, -30, CAST(GETDATE() AS DATE)) AND AdStatusId = 2";
            var achve= _context.Ads.FromSqlRaw(sqlQuery).ToList();
            foreach (var item in achve)
            {
                item.AdStatusId = 4;
                _context.Ads.Update(item);
                _context.SaveChanges();
            }


            ViewBag.whait = _context.Ads.Where(b => b.AdStatusId == 1 && !b.IsHide && !b.Customer.isDelete);
            ViewBag.Okay = _context.Ads.Where(a => a.AdStatusId == 2 &&!a.IsHide &&!a.Customer.isDelete); 
            ViewBag.Reject = _context.Ads.Where(b => b.AdStatusId == 3 && !b.IsHide && !b.Customer.isDelete);
            var Achive = _context.Ads.Where(b => b.AdStatusId == 4 && !b.IsHide && !b.Customer.isDelete);

            return View(Achive);
        }

        // GET: AdModels/Details/5
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

        // GET: AdModels/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AdTypeId"] = new SelectList(_context.AdTypes, "Id", "Id");
            ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "Id");
            ViewData["CategoryId"] = new SelectList(_context.categories, "Id", "Id");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            return View();
        }

        // POST: AdModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Token,Title,Description,CategoryId,AdTypeId,Price,AreaId,MobileNumber,WhatsAppNumber,NameVideo,numberShow,numberOfWhatsApp,numberOfCall,Space,NumberOfRooms,NumberOfBathrooms,CustomerId,AdStatusId,AddedDate,PublishDate")] AdModel adModel)
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

        public IActionResult EditModel(AdModel adModel)
        {
            var GetModel=_context.Ads.FirstOrDefault(b=>b.Id== adModel.Id);

            if(GetModel != null)
            {
                GetModel.Title = adModel.Title;
                GetModel.Description = adModel.Description;
                GetModel.CategoryId = adModel.CategoryId;
                GetModel.AdTypeId = adModel.AdTypeId;
                GetModel.Price = adModel.Price;
                GetModel.AreaId = adModel.AreaId;
                GetModel.MobileNumber = adModel.MobileNumber;
                GetModel.WhatsAppNumber = adModel.WhatsAppNumber;
                GetModel.numberShow = adModel.numberShow;
                GetModel.numberOfWhatsApp = adModel.numberOfWhatsApp;
                GetModel.numberOfCall = adModel.numberOfCall;
                GetModel.Space = adModel.Space;
                GetModel.NumberOfRooms = adModel.NumberOfRooms;
                GetModel.CustomerId = adModel.CustomerId;
                GetModel.NumberOfBathrooms = adModel.NumberOfBathrooms;
                GetModel.AdStatusId = adModel.AdStatusId;
                GetModel.PublishDate = adModel.PublishDate;
                _context.Update(GetModel);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: AdModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var admodeler = _context.Ads.Find(id);
            return View(admodeler);
        }

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
                            .Where(a => a.AdTypeId == 1 && a.AdStatusId == 2 && a.IsHide == true)
                            .OrderByDescending(a => a.PublishDate)
                            .FirstOrDefaultAsync();

                        if (latestAd != null)
                        {
                            var expiryDate = latestAd.ExpiryDate.Value.AddDays(1);
                            adModel.PublishDate = expiryDate;
                        }
                    }
                    else if (adModel.AdStatusId == 2 && adModel.AdTypeId == 2)
                    {
                        var latestAd = await _context.Ads
                            .Where(a => a.AdTypeId == 2 && a.CategoryId == adModel.CategoryId && a.AreaId == adModel.AreaId && a.AdStatusId == 2 && a.IsHide == true)
                            .OrderByDescending(a => a.PublishDate)
                            .FirstOrDefaultAsync();

                        if (latestAd != null)
                        {
                            var expiryDate = latestAd.ExpiryDate.Value.AddDays(1);
                            adModel.PublishDate = expiryDate;
                        }
                    }
                    else if (adModel.AdStatusId == 2 && adModel.AdTypeId == 3)
                    {
                        var latestAd = await _context.Ads
                            .Where(a => a.AdTypeId == 3 && a.CategoryId == adModel.CategoryId && a.AreaId == adModel.AreaId && a.AdStatusId == 2 && a.IsHide == true)
                            .OrderByDescending(a => a.PublishDate)
                            .FirstOrDefaultAsync();

                        if (latestAd != null)
                        {
                            var expiryDate = latestAd.ExpiryDate.Value.AddDays(1);
                            adModel.PublishDate = expiryDate;
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

        // GET: AdModels/Delete/5
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

        // POST: AdModels/Delete/5
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


        /// Function API
       
        [HttpPost]
        public async Task<IActionResult> UploadImage(int attachmentId)
        {
            var pathImage = _context.Attachments.Find(attachmentId);
            var Adsc = _context.Ads.Find(pathImage.AdId);

            try
            {
                SpacialDelete(attachmentId);
                var file = Request.Form.Files[0];
                var fileName = "file_" + Guid.NewGuid().ToString()+ Path.GetExtension(file.FileName);
                pathImage.nameimage = fileName;
                _context.Attachments.Update(pathImage);
                _context.SaveChanges();
                var imageDirectory = Path.Combine(_environment.WebRootPath, "upload", "user_" + Adsc.CustomerId, Adsc.Token, "images");

                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                var imagePath = Path.Combine(imageDirectory, fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                string imageUrl = $"https://myserverhost-001-site1.itempurl.com/upload/user_{Adsc.CustomerId}/{Adsc.Token}/images/{fileName}";
                return Json(new { imageUrl = imageUrl });
            }
            catch (Exception)
            {

                return NotFound();
            }


        }
        [HttpPost]
        public async Task<IActionResult> AddedImage(int attachmentId)
        {
            var Adsc = _context.Ads.Find(attachmentId);

            try
            {
                var file = Request.Form.Files[0];
                var fileName = "file_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                AttachmentModel att = new AttachmentModel
                {
                    nameimage = fileName,
                    AdId = attachmentId
                };
                _context.Attachments.Add(att);
                _context.SaveChanges();
                var imageDirectory = Path.Combine(_environment.WebRootPath, "upload", "user_" + Adsc.CustomerId, Adsc.Token, "images");

                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                var imagePath = Path.Combine(imageDirectory, fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                int maxId = _context.Attachments.Max(e => e.Id);
                string imageUrl = $"https://myserverhost-001-site1.itempurl.com/upload/user_{Adsc.CustomerId}/{Adsc.Token}/images/{fileName}";
                return Json(new { id = maxId.ToString() });
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var Adsc = await _context.Attachments.FindAsync(id);
            var Ads = await _context.Ads.FindAsync(Adsc.AdId);
            try
            {
                var fileName = Adsc.nameimage;
                var imageDirectory = Path.Combine(_environment.WebRootPath, "upload", "user_" + Ads.CustomerId, Ads.Token, "images");
                var imagePath = Path.Combine(imageDirectory, fileName);

                if (System.IO.File.Exists(imagePath))
                {
                    try
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new { success = false, message = "Error deleting file" });
                    }
                }
                _context.Attachments.Remove(Adsc);
                await _context.SaveChangesAsync();

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false });
            }

        }
        public async Task<IActionResult> UploadVedio(int AdaId)
        {
            var Adsc = await _context.Ads.FindAsync(AdaId);

            try
            {
                string imagePaths = "";
                var file = Request.Form.Files[0];
                var fileName = "file_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                if (Adsc.NameVideo != null) { 
                  imagePaths = Path.Combine(_environment.WebRootPath, "upload", "user_" + Adsc.CustomerId, Adsc.Token, "videos", Adsc.NameVideo);
                if (System.IO.File.Exists(imagePaths))
                {
                    try
                    {
                        System.IO.File.Delete(imagePaths);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new { success = false, message = "Error deleting file" });
                    }
                }
                }

                Adsc.NameVideo = fileName;
                _context.Ads.Update(Adsc);
                await _context.SaveChangesAsync();
                var imageDirectory = Path.Combine(_environment.WebRootPath, "upload", "user_" + Adsc.CustomerId, Adsc.Token, "videos");

                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                var imagePath = Path.Combine(imageDirectory, fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                string videoUrl = $"https://myserverhost-001-site1.itempurl.com/upload/user_{Adsc.CustomerId}/{Adsc.Token}/videos/{fileName}";
                return Json(new { videoUrl });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"حدث خطأ أثناء رفع الفيديو: {ex.Message}");
                return StatusCode(500, "حدث خطأ أثناء رفع الفيديو");
            }
        }
        public async Task<IActionResult> DeleteVedio(int id)
        {
            var AdsM = await _context.Ads.FindAsync(id);
           
            try
            {
                var fileName = AdsM.NameVideo;
                var imageDirectory = Path.Combine("upload", "user_" + AdsM.CustomerId, AdsM.Token, "videos", AdsM.NameVideo);
                var imagePath = Path.Combine(imageDirectory, fileName);

                if (System.IO.File.Exists(imagePath))
                {
                    try
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(new { success = false, message = "Error deleting file" });
                    }
                }
                AdsM.NameVideo = null;
                _context.Ads.Update(AdsM);
                await _context.SaveChangesAsync();

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false });
            }

        }
        public void SpacialDelete(int id)
        {
            try
            {
                var Adsc = _context.Attachments.Find(id);
                var Ads = _context.Ads.Find(Adsc.AdId);
                var fileName = Adsc.nameimage;
                var imageDirectory = Path.Combine(_environment.WebRootPath, "upload", "user_" + Ads.CustomerId, Ads.Token, "images");
                var imagePath = Path.Combine(imageDirectory, fileName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        
        ///

        // ASP.NET Core Controller
        [HttpPost]
        public async Task<IActionResult> ApproveAds(List<int> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var adModel = _context.Ads.FirstOrDefault(a => a.Id == id);
                    if (adModel != null)
                    {
                        try
                        {
                            if (adModel.AdStatusId == 1 && adModel.AdTypeId == 1)
                            {
                                var latestAd = await _context.Ads
                                    .Where(a => a.AdTypeId == 1 && a.AdStatusId == 2 && a.IsHide == true)
                                    .OrderByDescending(a => a.PublishDate)
                                    .FirstOrDefaultAsync();

                                if (latestAd != null)
                                {
                                    var expiryDate = latestAd.ExpiryDate.Value.AddDays(1);
                                    adModel.PublishDate = expiryDate;
                                }
                            }
                            else if (adModel.AdStatusId == 1 && adModel.AdTypeId == 2)
                            {
                                var latestAd = await _context.Ads
                                    .Where(a => a.AdTypeId == 2 && a.CategoryId == adModel.CategoryId && a.AreaId == adModel.AreaId && a.AdStatusId == 2 && a.IsHide == true)
                                    .OrderByDescending(a => a.PublishDate)
                                    .FirstOrDefaultAsync();

                                if (latestAd != null)
                                {
                                    var expiryDate = latestAd.ExpiryDate.Value.AddDays(1);
                                    adModel.PublishDate = expiryDate;
                                }
                            }
                            else if (adModel.AdStatusId == 1 && adModel.AdTypeId == 3)
                            {
                                var latestAd = await _context.Ads
                                    .Where(a => a.AdTypeId == 3 && a.CategoryId == adModel.CategoryId && a.AreaId == adModel.AreaId && a.AdStatusId == 2 && a.IsHide == true)
                                    .OrderByDescending(a => a.PublishDate)
                                    .FirstOrDefaultAsync();

                                if (latestAd != null)
                                {
                                    var expiryDate = latestAd.ExpiryDate.Value.AddDays(1);
                                    adModel.PublishDate = expiryDate;
                                }
                            }
                            else if (adModel.AdStatusId == 1 && (adModel.AdTypeId == 4 || adModel.AdTypeId == 5))
                            {
                                adModel.PublishDate = DateTime.Now;
                            }
                            adModel.AdStatusId = 2;
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
                    }
                }

                return Json(new { success = true, message = "تمت الموافقة على الإعلانات بنجاح." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "حدث خطأ أثناء معالجة الطلب." });
            }
        }

        public IActionResult RejectAds(List<int> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var ad = _context.Ads.FirstOrDefault(a => a.Id == id);
                    if (ad != null)
                    {
                        ad.AdStatusId = 3;
                        _context.Ads.Update(ad);
                        _context.SaveChanges();
                    }
                }

                return Json(new { success = true, message = "تمت الموافقة على الإعلانات بنجاح." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "حدث خطأ أثناء معالجة الطلب." });
            }
        }

        public IActionResult DeleteRow(int id)
        {
            var isDeleted = _context.Ads.Find(id);


            if (isDeleted != null)
            {
                isDeleted.IsHide = true;
                _context.Ads.Update(isDeleted);
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
       