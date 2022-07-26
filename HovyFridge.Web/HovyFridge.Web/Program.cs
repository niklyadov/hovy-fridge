using HovyFridge;
using HovyFridge.Data.Repository.GenericRepositoryPattern;
using HovyFridge.Services.Auth;
using HovyFridge.Web.Auth;
using HovyFridge.Web.Models;
using HovyFridge.Web.Services;
using HovyFridge.Web.Services.CodeConfirmation;
using HovyFridge.Web.Services.UserNotifier;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

// App configure
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"),
    b => b.MigrationsAssembly("HovyFridge.Web")
));

builder.Services.AddScoped<FridgesRepository>();
builder.Services.AddScoped<FridgeAccessLevelsRepository>();

builder.Services.AddScoped<ProductsRepository>();
builder.Services.AddScoped<ProductSuggestionsRepository>();

builder.Services.AddScoped<RecipesRepository>();
builder.Services.AddScoped<ShoppingListsRepository>();

builder.Services.AddScoped<UsersRepository>();

// Add services to the container.
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProductsService>();
builder.Services.AddScoped<FridgesService>();
builder.Services.AddScoped<VersionsService>();
builder.Services.AddSingleton<JwtTokensService>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<CodeConfirmationService>();
builder.Services.AddScoped<IUserNotifierService, TelegramUserNotifierService>();
builder.Services.AddScoped<FridgeAccessLevelsService>();
builder.Services.AddScoped<ProductSuggestionsService>();

var appConfiguration = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppConfiguration>(appConfiguration);

// add telegram bot client
builder.Services.AddSingleton<ITelegramBotClient>(
    new TelegramBotClient(appConfiguration.GetSection("TelegramBot").GetSection("Token").Value)
);

// auth configure
builder.Services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", options => { });
builder.Services.AddAuthorization(options => options.AddPolicy("BasicAuthentication", new AuthorizationPolicyBuilder("BasicAuthentication").RequireAuthenticatedUser().Build()));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
