using Microsoft.Extensions.Options;
using MongoDbProject.Services.CategoryServices;
using MongoDbProject.Services.CustomerServices;
using MongoDbProject.Services.GoogleCloud;
using MongoDbProject.Services.ProductServices;
using MongoDbProject.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 

builder.Services.AddScoped<ICategoryService,CategoryService>(); 
builder.Services.AddScoped<ICustomerService,CustomerService>(); 
builder.Services.AddScoped<IProductService,ProductService>(); 

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); //AUTOMAPER CONF�G

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings")); //Veri taban� ba�lant� Adresi

builder.Services.Configure<GCSConfigOptions>(builder.Configuration);
builder.Services.AddSingleton<ICloudStorageService, CloudStorageService>(); 

builder.Services.AddScoped<IDatabaseDateSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
