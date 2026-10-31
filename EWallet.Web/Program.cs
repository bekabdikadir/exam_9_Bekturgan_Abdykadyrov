using System.Globalization;
using EWallet.BLL;
using EWallet.DAL;
using EWallet.DAL.Data;
using EWallet.DAL.Entities;
using EWallet.Web;
using EWallet.Web.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDataAccess(connectionString);
builder.Services.AddBusinessLogic();

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 6;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.User.AllowedUserNameCharacters = string.Empty;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
    .AddClaimsPrincipalFactory<AppClaimsPrincipalFactory>();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services
    .AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (_, factory) => factory.Create(typeof(SharedResource));
    });

builder.Services.AddOptions<MvcOptions>().Configure<IStringLocalizerFactory>((options, factory) =>
{
    var localizer = factory.Create(typeof(SharedResource));
    var provider = options.ModelBindingMessageProvider;
    provider.SetValueMustBeANumberAccessor(_ => localizer["Validation_Number"].Value);
    provider.SetAttemptedValueIsInvalidAccessor((_, _) => localizer["Validation_ValueInvalid"].Value);
    provider.SetValueIsInvalidAccessor(_ => localizer["Validation_ValueInvalid"].Value);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

var supportedCultures = new[] { "ru", "en" };
app.UseRequestLocalization(options =>
{
    options.SetDefaultCulture("ru");
    options.AddSupportedCultures(supportedCultures);
    options.AddSupportedUICultures(supportedCultures);
    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new CookieRequestCultureProvider()
    };
});

app.Use(async (context, next) =>
{
    CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
    await next();
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
