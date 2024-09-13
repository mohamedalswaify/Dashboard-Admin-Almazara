using AdminControl.Models;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers
{
    public class NotificationModel : Controller
    {
        private readonly BloggingContext _context;
        public readonly IWebHostEnvironment _environment;

        private static bool _initialized = false;


        public NotificationModel(BloggingContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }
        public IActionResult Index()
        {
            var allDataNotification = _context.Notifications.ToList().OrderByDescending(b => b.CreatedDate);
            return View(allDataNotification);
        }
        public IActionResult GetDetailsMessage(int id)
        {
                var data = _context.Notifications.Find(id);

                if (data != null)
                {
                    return Ok(new { data = data.MessageBody });
                }
                else
                {
                    return NotFound(new { message = "التفاصيل غير موجودة" });
                }
        }
        public IActionResult DetailsNotification(int id)
        {
            var notification = _context.Notifications.Find(id);
            return View(notification);
        }
        public IActionResult DeleteRow(int id)
        {
            var isDeleted = _context.notificationCustomers.Find(id);


            if (isDeleted != null)
            {
                _context.notificationCustomers.Remove(isDeleted);
                _context.SaveChanges();
                return Ok(new { success = true });
            }
            else
            {
                return BadRequest(new { success = false });
            }
        }
        public IActionResult ApproveNotification(List<int> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var Noti = _context.notificationCustomers.FirstOrDefault(a => a.Id == id);
                    if (Noti != null)
                    {
                        _context.notificationCustomers.Remove(Noti);
                        _context.SaveChanges();
                    }
                }

                return Json(new { success = true, message = "تم الحذف بنجاح." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "حدث خطأ أثناء معالجة الطلب." });
            }
        }
        public IActionResult DeleteRowNotfication(int id)
        {
            var isDeletednotificationCustomers = _context.notificationCustomers.Where(b => b.Id==id ).ToList();
            var isDeleted = _context.Notifications.Find(id);
            foreach (var notificationCutm in isDeletednotificationCustomers)
            {
                var Noti = _context.notificationCustomers.FirstOrDefault(a => a.NotificationModelId == notificationCutm.Id);
                if (Noti != null)
                {
                    _context.notificationCustomers.Remove(Noti);
                    _context.SaveChanges();
                }
            }

            if (isDeleted != null)
            {
                _context.Notifications.Remove(isDeleted);
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
