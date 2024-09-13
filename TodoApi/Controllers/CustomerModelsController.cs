using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminControl.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Hosting;
using FirebaseAdmin.Messaging;
using Newtonsoft.Json.Linq;
using TodoApi.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdminControl.Controllers
{
    public class CustomerModelsController : Controller
    {
        private readonly BloggingContext _context;
        public readonly IWebHostEnvironment _environment;

        private static bool _initialized = false;


        public CustomerModelsController(BloggingContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }

        // GET: CustomerModels
        public async Task<IActionResult> Index()
        {

            var result = _context.Customers.Where(b => !b.isDelete && b.Ad.Count > 0)
            .Select(cs => new SelectListItem {
            Value = cs.Id.ToString(),
            Text = $"{cs.username}  - {cs.phoneNumber} - عدد الاعلانات: {cs.Ad.Count}"
            }).ToList();
            ViewBag.ListItmeData = new SelectList(result, "Value", "Text");
            return View(await _context.Customers.Where(b => !b.isDelete).ToListAsync());
        }

        // GET: CustomerModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerModel = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerModel == null)
            {
                return NotFound();
            }

            return View(customerModel);
        }

        // GET: CustomerModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CustomerModel customerModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerModel);
        }

        // GET: CustomerModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerModel = await _context.Customers.FindAsync(id);
            if (customerModel == null)
            {
                return NotFound();
            }
            return View(customerModel);
        }

        // POST: CustomerModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,username,email,phoneNumber,password,isblock,token")] CustomerModel customerModel)
        {
            if (id != customerModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerModelExists(customerModel.Id))
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
            return View(customerModel);
        }

        // GET: CustomerModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerModel = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerModel == null)
            {
                return NotFound();
            }

            return View(customerModel);
        }

        // POST: CustomerModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerModel = await _context.Customers.FindAsync(id);
            if (customerModel != null)
            {
                _context.Customers.Remove(customerModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerModelExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
         public IActionResult CheckEmail(string email, string phone)
         {
            var customer = _context.Customers.FirstOrDefault(b => b.email == email || b.phoneNumber == phone);

            if (customer == null || !IsValidEmail(email))
            {
                return Ok(new { exists = false });
            }
            else
            {
                return Ok(new { exists = true });
            }
         }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public IActionResult CheckEmailEdit(string email, string phone)
        {
            var emailExists = _context.Customers.FirstOrDefault(b => (email != null && b.email == email) || b.phoneNumber == phone);

            if (email == null || emailExists == null || (emailExists.email == email && emailExists.phoneNumber == phone))
            {
                return Ok(new { exists = false });
            }
            else
            {
                return Ok(new { exists = true });
            }

        }
        public IActionResult DeleteRow(int id)
        {
            var isDeleted = _context.Customers.Find(id);


            if (isDeleted != null)
            {
                isDeleted.isDelete = true;
                _context.Customers.Update(isDeleted);
                _context.SaveChanges();
                return Ok(new { success = true });
            }
            else
            {
                return BadRequest(new { success = false });
            }
        }
        public IActionResult GetDetails(int id)
        {
            var details = _context.Ads.Where(ad => !ad.IsHide);
            return View(details);


        }
        public async Task<IActionResult> SendNotification(int title, IFormFile imageName, List<string> selectedIds)
        {
            InitializeFirebase();
            var Ada = _context.Ads.FirstOrDefault(b => b.Id == title);
            var AdaImage = _context.Attachments.FirstOrDefault(b=>b.Id==Ada.Id);
            int count = 0;
            int notificationId = 0;


            if (AdaImage != null)
            {
                NotificationModel notificationModel = new NotificationModel
                {
                    Title = Ada.Title,
                    MessageBody = Ada.Description,
                    Image = AdaImage.nameimage,
                    AdModelId = Ada.Id
                };

                _context.Notifications.Add(notificationModel);
                await _context.SaveChangesAsync();
                notificationId = notificationModel.Id;
            }
            else
            {
                NotificationModel notificationModel = new NotificationModel
                {
                    Title = Ada.Title,
                    MessageBody = Ada.Description,
                    Image = null,
                    AdModelId = Ada.Id
                };

                _context.Notifications.Add(notificationModel);
                await _context.SaveChangesAsync();
                notificationId = notificationModel.Id;
            }

            foreach (var item in selectedIds)
            {
                var message = new Message()
                {
                    Notification = new Notification()
                    {
                        Title = Ada.Title,
                        Body = Ada.Description,
                    },
                    Token = item,
                };
                try
                {
                    string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                    var customer = _context.Customers.FirstOrDefault(c => c.token == item);
                    if (customer != null)
                    {
                        NotificationCustomer notificationCustomer = new NotificationCustomer
                        {
                            NotificationModelId = notificationId,
                            CustomerModelId = customer.Id,
                        };

                        _context.notificationCustomers.Add(notificationCustomer);
                        await _context.SaveChangesAsync();
                        count++;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            var NotificationID = _context.Notifications.FirstOrDefault(b => b.Id == notificationId);
            NotificationID.NumberOfSend = count;
            _context.SaveChanges();
            return Ok();
        }

        public async Task<IActionResult> SendNotificationSimpel(string titleNoti, string bodyMassage, IFormFile imageName, List<string> selectedIds)
        {
            InitializeFirebase();
            int count = 0;
            NotificationModel notificationModel = new NotificationModel
            {
                Title = titleNoti,
                MessageBody = bodyMassage
            };

            _context.Notifications.Add(notificationModel);
            await _context.SaveChangesAsync();

            int notificationId = notificationModel.Id;

            foreach (var item in selectedIds)
            {
                var message = new Message()
                {
                    Notification = new Notification()
                    {
                        Title = titleNoti,
                        Body = bodyMassage,
                    },
                    Token = item,
                };
                try
                {
                    string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                    var customer = _context.Customers.FirstOrDefault(c => c.token == item);
                    if (customer != null)
                    {
                        NotificationCustomer notificationCustomer = new NotificationCustomer
                        {
                            NotificationModelId = notificationId,
                            CustomerModelId = customer.Id,
                        };

                        _context.notificationCustomers.Add(notificationCustomer);
                        await _context.SaveChangesAsync();
                        count++;

                    }
                }
                catch (Exception ex)
                {

                }
            }

                var NotificationID = _context.Notifications.FirstOrDefault(b => b.Id == notificationId);
                NotificationID.NumberOfSend = count;
                _context.SaveChanges();
                return Ok();
        }

        public async Task<IActionResult> SendNotificationSimpelAll(string titleNotiall, string bodyMassageall)
        {
            InitializeFirebase();
            try
            {
                NotificationModel notificationModel = new NotificationModel
                {
                    Title = titleNotiall,
                    MessageBody = bodyMassageall
                };

                _context.Notifications.Add(notificationModel);
                await _context.SaveChangesAsync();

                var message = new Message()
                {
                    Notification = new Notification()
                    {
                        Title = titleNotiall,
                        Body = bodyMassageall,
                    },
                    Topic ="all"
                };

                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            }
            catch (Exception)
            {

                throw;
            }
           

            return Ok();
        }
        public void InitializeFirebase()
        {
            if (_initialized)
                return;

            string pathToServiceAccountKey = Path.Combine(_environment.WebRootPath, "FirebaseJson", "FireBaseCodeJson.json");

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(pathToServiceAccountKey),
            });

            _initialized = true;
        }
        public IActionResult GetAddModel(int selectedValue)
        {
            var jsonData = _context.Ads.Where(b => b.CustomerId == selectedValue &&!b.IsHide && b.AdStatusId==2).Select(b => new
            {
                value = b.Id,
                text = b.Title
            });
            return Json(jsonData);
        }



    }
}
