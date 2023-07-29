using Oil.Management.Shared;
using Oil.Management.Web.Context;
using Microsoft.EntityFrameworkCore;
using Oil.Management.Shared.Interfaces;
using Oil.Management.Services;
using Oil.Management.Shared.Settings;
using ApplMgt.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

//private readonly Common common = new Common();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));


// Register interface and classes
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IApplTaskService, ApplTaskService>();
builder.Services.AddScoped<IApplReferenceService, ApplReferenceService>();
builder.Services.AddScoped<IMasterService, MasterService>();
builder.Services.AddScoped<ITransaction, TransactionService>();

builder.Services.AddMemoryCache();


builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = null;
    o.JsonSerializerOptions.DictionaryKeyPolicy = null;
});

builder.Services.AddCors(p=>p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("").AllowAnyMethod().AllowAnyHeader();
}));
    
    // Add services to the container.


// geting value param json parse to  models
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppMgtSettingModel>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppMgtSettingModel>();
AppMgtSetting.ConnectionString = appSettings.ConnectionString;
AppMgtSetting.Secret = appSettings.Secret;

RefUploadService refUpload = new RefUploadService();
refUpload.InitConstant();



// set sessions
builder.Services.AddSession(opts =>
{
    opts.IdleTimeout = TimeSpan.FromSeconds(1000); // set hardcode time
});

// register session singeleton
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



var app = builder.Build();
//app.UsePathBase("/oil_inventory"); 

IConfiguration configuration = app.Configuration;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
