using eShopSolution.Application.Catalog.Products;
using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.Utilities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;


var builder = WebApplication.CreateBuilder(args);

// =====================
// Add services
// =====================

// MVC
builder.Services.AddControllersWithViews();

// DbContext
builder.Services.AddDbContext<EShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(SystemConstains.MainConectionString)));

// DI
builder.Services.AddTransient<IStorageService, FileStorageService >();
builder.Services.AddTransient<IPublicProductService, PublicProductService>();
builder.Services.AddTransient<IManageProductService, ManageProductService>();

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Swagger eShop Solution",
        Version = "v1"
    });
});

var app = builder.Build();

// =====================
// Configure request pipeline
// =====================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger eShopSolution V1");
});

// Static assets (.NET 9)
app.MapStaticAssets();

// MVC routing
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
