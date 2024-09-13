using Microsoft.EntityFrameworkCore;
using System;
using TodoApi.Models;

namespace AdminControl.Models
{
    public class BloggingContext : DbContext
    {


        public BloggingContext(DbContextOptions<BloggingContext> opations) : base(opations)
        {

        }
      
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<AdModel> Ads { get; set; }
        public DbSet<AttachmentModel> Attachments { get; set; }
        public DbSet<FavoritesModel> Favorites { get; set; }
        public DbSet<SuggestionModel> Suggestions { get; set; }
        public DbSet<SuggestionSearch> suggestionSearches { get; set; }
        public DbSet<NotificationModel> Notifications { get; set; }
        public DbSet<AdType> AdTypes { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<AdStatus> adStatuses { get; set; }
        public DbSet<AdView> AdViews { get; set; }
        public DbSet<WhatsAppClick> WhatsAppClicks { get; set; }
        public DbSet<CallClick> CallClicks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<NotificationCustomer> notificationCustomers { get; set; }
        public DbSet<NotificationView> notificationViews { get; set; }
        public DbSet<Report> reports { get; set; }

        public DbSet<BackpDatabase> BackpDatabases { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<FavoritesModel>()
                .HasKey(f => new { f.CustomerId, f.AdId });

            modelBuilder.Entity<FavoritesModel>()
                .HasOne(f => f.Customer)
                .WithMany(c => c.Favorites)
                .HasForeignKey(f => f.CustomerId);

            modelBuilder.Entity<FavoritesModel>()
                .HasOne(f => f.Ad)
                .WithMany(a => a.Favorites)
                .HasForeignKey(f => f.AdId);

            modelBuilder.Entity<AttachmentModel>()
    .HasOne(f => f.Ad)
    .WithMany(a => a.Attachments)
    .HasForeignKey(f => f.AdId);
            base.OnModelCreating(modelBuilder);




            // إضافة بيانات التصنيفات المبدئية
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "تأجير" },
                new Category { Id = 2, Name = "بيع وشراء" },
                new Category { Id = 3, Name = "مطاعم" },
                new Category { Id = 4, Name = "كافيهات" },
                new Category { Id = 5, Name = "مسابح" },
                new Category { Id = 6, Name = "مقاولات" },
                new Category { Id = 7, Name = "صحي" },
                new Category { Id = 8, Name = "سيراميك" },
                new Category { Id = 9, Name = "كهربائي" },
                new Category { Id = 10, Name = "تنكر" },
                new Category { Id = 11, Name = "العاب" },
                new Category { Id = 12, Name = "بيع خضار" },
                new Category { Id = 13, Name = "بيع ألبان" },
                new Category { Id = 14, Name = "مشاتل" },
                new Category { Id = 15, Name = "بيطري" },
                new Category { Id = 16, Name = "حرف زراعية" },
                new Category { Id = 17, Name = "تصليح أجهزه" },
                new Category { Id = 18, Name = "فني ألمونيوم" },
                new Category { Id = 19, Name = "حداد" },
                new Category { Id = 20, Name = "تصليح اقفال" },
                new Category { Id = 21, Name = "معدات ثقيلة" },
                new Category { Id = 22, Name = "ستلايتوكاميرات" },
                new Category { Id = 23, Name = "بقالة" },
                new Category { Id = 24, Name = "أخري" }
            );


            modelBuilder.Entity<AdType>().HasData(
                new AdType { Id = 1, Name = "اعلان الصفحه الرئيسيه" },
                new AdType { Id = 2, Name = "اعلان الفئة" },
                new AdType { Id = 3, Name = "اعلان الاخضر " },
                new AdType { Id = 4, Name = "اعلان الذهبي" },
                new AdType { Id = 5, Name = "اعلان العادي" }

                );

            modelBuilder.Entity<Area>().HasData(
                new Area { Id = 1, Name = "العبدلي" },
                new Area { Id = 2, Name = "الوفرة" },
                new Area { Id = 3, Name = "شاليهات" }
                );

            modelBuilder.Entity<AdStatus>().HasData(
             new AdStatus { Id = 1, Status = "انتظار" },
             new AdStatus { Id = 2, Status = "مقبول" },
             new AdStatus { Id = 3, Status = "مرفوض" },
             new AdStatus { Id = 4, Status = "مؤرشف" }
             );
            










            //protected override void OnConfiguring(DbContextOptionsBuilder options)
            //{
            //    options.UseSqlServer("server=DESKTOP-0820HOU;Database=SchoolDB;Trusted_Connection=True;TrustServerCertificate=true");
            //}

        }
    }
}
