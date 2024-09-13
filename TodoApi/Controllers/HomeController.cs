using AdminControl.Models;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AdminControl.Controllers
{
    public class HomeController : Controller
    {
        private readonly BloggingContext _context;
        public readonly IWebHostEnvironment _environment;

        private static bool _initialized = false;


        public HomeController(BloggingContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;

        }

        public IActionResult Index()
        {
            //Ads Report
            ////////////////////////////
            ViewBag.AdTotal = _context.Ads.Where(b=>!b.IsHide && !b.Customer.isDelete).Count();
            ViewBag.ViewsTotal = _context.Ads.Where(b => !b.IsHide && !b.Customer.isDelete).Sum(b => b.numberShow);
            ViewBag.ViewsCustomer = _context.Ads.Where(b => !b.IsHide).Select(b => b.CustomerId).Distinct().Count();
            
            //Max Category
            var topCategory = _context.Ads.Where(ad => ad.numberShow.HasValue && !ad.IsHide && !ad.Customer.isDelete) .GroupBy(ad => ad.CategoryId) 
                .Select(group => new{
                      CategoryId = group.Key,
                      TotalViews = group.Sum(ad => ad.numberShow),
                      
                })
                .OrderByDescending(result => result.TotalViews) .FirstOrDefault();

            //Max Category
            var topArea = _context.Ads.Where(ad => ad.numberShow.HasValue && !ad.IsHide && !ad.Customer.isDelete).GroupBy(ad => ad.AreaId)
                .Select(group => new {
                    AreaId = group.Key,
                    TotalViews = group.Sum(ad => ad.numberShow),

                })
                .OrderByDescending(result => result.TotalViews).FirstOrDefault();

            if (topCategory != null)
            {
                var cat = _context.categories.FirstOrDefault(b => b.Id == topCategory.CategoryId);
                var catArea = _context.Areas.FirstOrDefault(b => b.Id == topArea.AreaId);



                if (cat != null) 
                { 
                    ViewBag.ViewsCategory = cat.Name;
                    ViewBag.ViewsAreaCategory = catArea.Name;


                }
                else 
                { 
                    ViewBag.ViewsCategory = "لايوجد";
                    ViewBag.ViewsAreaCategory = "لايوجد";

                }


                //var area = _context.Areas.FirstOrDefault(b => b.Id == topCategory);


            }
            else {
                ViewBag.ViewsCategory = "لايوجد";
            }
            

            //End Max Category

            var MaxAd = _context.Ads.Where(b => !b.IsHide).OrderByDescending(b => b.numberShow).FirstOrDefault();
            if (MaxAd != null)
            {
                ViewBag.MaxAda = MaxAd.Title;
                ViewBag.MaxAdaNumberShow = MaxAd.numberShow;
                ViewBag.MaxAdaCategoryName = MaxAd.Category.Name;
                ViewBag.MaxAdanumberOfWhatsApp = MaxAd.numberOfWhatsApp;
                ViewBag.MaxAdaAreaName = MaxAd.Area.Name;
                ViewBag.MaxAdaId = MaxAd.Id;
                ViewBag.Count =1;
            }
            else
            {
                ViewBag.Count = 0;
            }
            var AllDataAds = _context.Ads.Where(b=>b.AdStatusId == 2 && !b.IsHide && !b.Customer.isDelete).ToList();
            
            //User Report
            /////////////////////////////////////
            ViewBag.UserTotal = _context.Customers.Where(b=>!b.isDelete).Count();
            ViewBag.NotificationUserTotal=_context.notificationCustomers.Where(b=>!b.customer.isDelete).Count();
            ViewBag.SuggestionTotal = _context.Suggestions.Count();
            var topUser = _context.AdViews.Where(av=>av.CustomerModel.isDelete==false).GroupBy(av => av.UserId).Select(group => new
            {
                UserId = group.Key,
                ViewCount = group.Count(),
            }).OrderByDescending(x => x.ViewCount).FirstOrDefault();
            if(topUser != null)
            {
                var UserToScore = _context.Customers.Find(topUser.UserId);
                @ViewBag.UserTopViews = UserToScore.username;
            }
            else{ @ViewBag.UserTopViews = "-"; }

            var topUserAd = _context.Ads.Where(b => !b.IsHide && !b.Customer.isDelete)
            .GroupBy(ad => ad.CustomerId).Select(group => new         
            {
                CustomerId = group.Key,  
                AdCount = group.Count()   
            }).OrderByDescending(x => x.AdCount).FirstOrDefault();

            if (topUser != null)
            {
                var UserName = _context.Customers.FirstOrDefault(c => c.Id == topUserAd.CustomerId);
                var countUser = _context.Ads.Where(ad=>!ad.IsHide && !ad.Customer.isDelete).Count(b => b.CustomerId == topUserAd.CustomerId);
                ViewBag.UserName = UserName.username;
                ViewBag.CustIdAda = UserName.Id;
                ViewBag.UserPhonerNumber = UserName.phoneNumber;
                ViewBag.UserEmail = UserName.email;
                ViewBag.UserCountAda = countUser;
                ViewBag.CountUser = 1;
            }
            else
            {
                ViewBag.CountUser = 0;
            }
            var adViews = _context.AdViews.Where(av=>!av.CustomerModel.isDelete).Include(av => av.Ad).ThenInclude(ad => ad.Category).ToList();

            // تجميع البيانات في الذاكرة
            var mostVisitedCategory = adViews
                .GroupBy(av => av.Ad.Category)
                .Select(g => new
                {
                    CategoryName = g.Key.Name,
                    VisitCount = g.Count()
                })
                .OrderByDescending(g => g.VisitCount)
                .Select(g => g.CategoryName)
                .FirstOrDefault();

            var mostVisitedArea = adViews
                .GroupBy(av => av.Ad.Area)
                .Select(g => new
                {
                    AreaName = g.Key.Name,
                    VisitCount = g.Count()
                })
                .OrderByDescending(g => g.VisitCount)
                .Select(g => g.AreaName)
                .FirstOrDefault();

            if (mostVisitedCategory != null)
            {
                ViewBag.TopCategrory = mostVisitedCategory;
                ViewBag.TopCategroryArea = mostVisitedArea;
            }
            else
            {
                ViewBag.TopCategrory = "لايوجد";
            }

            var adStatistics = _context.Ads.Where(ad=>!ad.IsHide && !ad.Customer.isDelete)
    .GroupBy(ad => new { ad.CustomerId, ad.Customer.username, ad.Customer.phoneNumber })
    .Select(group => new
    {
        CustomerId = group.Key.CustomerId,
        CustomerName = group.Key.username,
        CustomerPhone = group.Key.phoneNumber,
        TotalByStatus = new
        {
            PendingCount = group.Sum(ad => ad.AdStatusId == 1 ? 1 : 0),
            AcceptedCount = group.Sum(ad => ad.AdStatusId == 2 ? 1 : 0),
            RejectedCount = group.Sum(ad => ad.AdStatusId == 3 ? 1 : 0)
        },
        TopCategoryId = group.SelectMany(ad => ad.AdViews)
                             .GroupBy(av => av.Ad.CategoryId)
                             .Select(catGroup => new
                             {
                                 CategoryId = catGroup.Key,
                                 VisitCount = catGroup.Count()
                             })
                             .OrderByDescending(av => av.VisitCount)
                             .FirstOrDefault()
    })
    .ToList();

            var topCategoryViews = adStatistics.Select(stat => new
            {
                UserName = stat.CustomerName,
                UserPhone = stat.CustomerPhone,
                CategoryName = stat.TopCategoryId != null ?
                               _context.categories.FirstOrDefault(c => c.Id == stat.TopCategoryId.CategoryId)?.Name : "N/A",
                PendingAds = stat.TotalByStatus.PendingCount,
                ApprovedAds = stat.TotalByStatus.AcceptedCount,
                RejectedAds = stat.TotalByStatus.RejectedCount
            }).ToList();

            ViewBag.TopCategoryViews = topCategoryViews;

            ////////////////////////////






            return View(AllDataAds);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
