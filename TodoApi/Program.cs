using AdminControl.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//builder.Services.AddDbContext<BloggingContext>(opations => opations.UseSqlServer(
//    builder.Configuration.GetConnectionString("conn2")
//    ));

builder.Services.AddDbContext<BloggingContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("conn2"))
                   .UseLazyLoadingProxies());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AdLogin}/{action=Login}/{id?}");



app.Run();
