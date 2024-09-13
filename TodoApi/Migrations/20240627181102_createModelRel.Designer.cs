﻿// <auto-generated />
using System;
using AdminControl.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TodoApi.Migrations
{
    [DbContext(typeof(BloggingContext))]
    [Migration("20240627181102_createModelRel")]
    partial class createModelRel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AdminControl.Models.AdModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AdStatusId")
                        .HasColumnType("int");

                    b.Property<int>("AdTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsHide")
                        .HasColumnType("bit");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameVideo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumberOfBathrooms")
                        .HasColumnType("int");

                    b.Property<int?>("NumberOfRooms")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Space")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhatsAppNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("numberOfCall")
                        .HasColumnType("int");

                    b.Property<int?>("numberOfWhatsApp")
                        .HasColumnType("int");

                    b.Property<int?>("numberShow")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdStatusId");

                    b.HasIndex("AdTypeId");

                    b.HasIndex("AreaId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Ads");
                });

            modelBuilder.Entity("AdminControl.Models.AdStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("adStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Status = "انتظار"
                        },
                        new
                        {
                            Id = 2,
                            Status = "مقبول"
                        },
                        new
                        {
                            Id = 3,
                            Status = "مرفوض"
                        });
                });

            modelBuilder.Entity("AdminControl.Models.AdType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AdTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "اعلان الصفحه الرئيسيه"
                        },
                        new
                        {
                            Id = 2,
                            Name = "اعلان الفئة"
                        },
                        new
                        {
                            Id = 3,
                            Name = "اعلان الاخضر "
                        },
                        new
                        {
                            Id = 4,
                            Name = "اعلان الذهبي"
                        },
                        new
                        {
                            Id = 5,
                            Name = "اعلان العادي"
                        });
                });

            modelBuilder.Entity("AdminControl.Models.AdView", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ViewedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AdId");

                    b.ToTable("AdViews");
                });

            modelBuilder.Entity("AdminControl.Models.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Areas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "العبدلي"
                        },
                        new
                        {
                            Id = 2,
                            Name = "الوفرة"
                        },
                        new
                        {
                            Id = 3,
                            Name = "شاليهات"
                        });
                });

            modelBuilder.Entity("AdminControl.Models.AttachmentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdId")
                        .HasColumnType("int");

                    b.Property<string>("nameimage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("AdminControl.Models.CallClick", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ClickedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdId");

                    b.ToTable("CallClicks");
                });

            modelBuilder.Entity("AdminControl.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "تأجير"
                        },
                        new
                        {
                            Id = 2,
                            Name = "بيع وشراء"
                        },
                        new
                        {
                            Id = 3,
                            Name = "مطاعم"
                        },
                        new
                        {
                            Id = 4,
                            Name = "كافيهات"
                        },
                        new
                        {
                            Id = 5,
                            Name = "مسابح"
                        },
                        new
                        {
                            Id = 6,
                            Name = "مقاولات"
                        },
                        new
                        {
                            Id = 7,
                            Name = "صحي"
                        },
                        new
                        {
                            Id = 8,
                            Name = "سيراميك"
                        },
                        new
                        {
                            Id = 9,
                            Name = "كهربائي"
                        },
                        new
                        {
                            Id = 10,
                            Name = "تنكر"
                        },
                        new
                        {
                            Id = 11,
                            Name = "العاب"
                        },
                        new
                        {
                            Id = 12,
                            Name = "بيع خضار"
                        },
                        new
                        {
                            Id = 13,
                            Name = "بيع ألبان"
                        },
                        new
                        {
                            Id = 14,
                            Name = "مشاتل"
                        },
                        new
                        {
                            Id = 15,
                            Name = "بيطري"
                        },
                        new
                        {
                            Id = 16,
                            Name = "حرف زراعية"
                        },
                        new
                        {
                            Id = 17,
                            Name = "تصليح أجهزه"
                        },
                        new
                        {
                            Id = 18,
                            Name = "فني ألمونيوم"
                        },
                        new
                        {
                            Id = 19,
                            Name = "حداد"
                        },
                        new
                        {
                            Id = 20,
                            Name = "تصليح اقفال"
                        },
                        new
                        {
                            Id = 21,
                            Name = "معدات ثقيلة"
                        },
                        new
                        {
                            Id = 22,
                            Name = "ستلايتوكاميرات"
                        },
                        new
                        {
                            Id = 23,
                            Name = "بقالة"
                        },
                        new
                        {
                            Id = 24,
                            Name = "أخري"
                        });
                });

            modelBuilder.Entity("AdminControl.Models.CustomerModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Isconfirm")
                        .HasColumnType("bit");

                    b.Property<string>("confirm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("isblock")
                        .HasColumnType("bit");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("AdminControl.Models.FavoritesModel", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("AdId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("CustomerId", "AdId");

                    b.HasIndex("AdId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("AdminControl.Models.NotificationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdModelId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MessageBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdModelId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("AdminControl.Models.SuggestionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isRead")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Suggestions");
                });

            modelBuilder.Entity("AdminControl.Models.SuggestionSearch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("suggestionSearches");
                });

            modelBuilder.Entity("AdminControl.Models.WhatsAppClick", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ClickedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdId");

                    b.ToTable("WhatsAppClicks");
                });

            modelBuilder.Entity("TodoApi.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("TodoApi.Models.NotificationCustomer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerModelId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("NotificationModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerModelId");

                    b.HasIndex("NotificationModelId");

                    b.ToTable("notificationCustomers");
                });

            modelBuilder.Entity("AdminControl.Models.AdModel", b =>
                {
                    b.HasOne("AdminControl.Models.AdStatus", "AdStatus")
                        .WithMany("AdModels")
                        .HasForeignKey("AdStatusId");

                    b.HasOne("AdminControl.Models.AdType", "AdType")
                        .WithMany()
                        .HasForeignKey("AdTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdminControl.Models.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdminControl.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdminControl.Models.CustomerModel", "Customer")
                        .WithMany("Ad")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AdStatus");

                    b.Navigation("AdType");

                    b.Navigation("Area");

                    b.Navigation("Category");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("AdminControl.Models.AdView", b =>
                {
                    b.HasOne("AdminControl.Models.AdModel", "Ad")
                        .WithMany("AdViews")
                        .HasForeignKey("AdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ad");
                });

            modelBuilder.Entity("AdminControl.Models.AttachmentModel", b =>
                {
                    b.HasOne("AdminControl.Models.AdModel", "Ad")
                        .WithMany("Attachments")
                        .HasForeignKey("AdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ad");
                });

            modelBuilder.Entity("AdminControl.Models.CallClick", b =>
                {
                    b.HasOne("AdminControl.Models.AdModel", "Ad")
                        .WithMany("CallClicks")
                        .HasForeignKey("AdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ad");
                });

            modelBuilder.Entity("AdminControl.Models.FavoritesModel", b =>
                {
                    b.HasOne("AdminControl.Models.AdModel", "Ad")
                        .WithMany("Favorites")
                        .HasForeignKey("AdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdminControl.Models.CustomerModel", "Customer")
                        .WithMany("Favorites")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ad");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("AdminControl.Models.NotificationModel", b =>
                {
                    b.HasOne("AdminControl.Models.AdModel", "adModel")
                        .WithMany("NotificationModel")
                        .HasForeignKey("AdModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("adModel");
                });

            modelBuilder.Entity("AdminControl.Models.WhatsAppClick", b =>
                {
                    b.HasOne("AdminControl.Models.AdModel", "Ad")
                        .WithMany("WhatsAppClicks")
                        .HasForeignKey("AdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ad");
                });

            modelBuilder.Entity("TodoApi.Models.NotificationCustomer", b =>
                {
                    b.HasOne("AdminControl.Models.CustomerModel", "customer")
                        .WithMany("NotificationCustomer")
                        .HasForeignKey("CustomerModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdminControl.Models.NotificationModel", "Notification")
                        .WithMany("NotificationCustomer")
                        .HasForeignKey("NotificationModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Notification");

                    b.Navigation("customer");
                });

            modelBuilder.Entity("AdminControl.Models.AdModel", b =>
                {
                    b.Navigation("AdViews");

                    b.Navigation("Attachments");

                    b.Navigation("CallClicks");

                    b.Navigation("Favorites");

                    b.Navigation("NotificationModel");

                    b.Navigation("WhatsAppClicks");
                });

            modelBuilder.Entity("AdminControl.Models.AdStatus", b =>
                {
                    b.Navigation("AdModels");
                });

            modelBuilder.Entity("AdminControl.Models.CustomerModel", b =>
                {
                    b.Navigation("Ad");

                    b.Navigation("Favorites");

                    b.Navigation("NotificationCustomer");
                });

            modelBuilder.Entity("AdminControl.Models.NotificationModel", b =>
                {
                    b.Navigation("NotificationCustomer");
                });
#pragma warning restore 612, 618
        }
    }
}
