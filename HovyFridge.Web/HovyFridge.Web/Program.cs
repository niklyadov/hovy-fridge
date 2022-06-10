using HovyFridge.Services.Auth;
using HovyFridge.Web.Auth;
using HovyFridge.Web.Models;
using HovyFridge.Web.Repos;
using HovyFridge.Web.Services;
using HovyFridge.Web.Services.CodeConfirmation;
using HovyFridge.Web.Services.UserNotifier;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

// configure app
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
var appConfiguration = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppConfiguration>(appConfiguration);

// Add services to the container.

// some services
builder.Services.AddSingleton<UsersRepo>();
builder.Services.AddSingleton<JwtTokensService>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<CodeConfirmationService>();
builder.Services.AddScoped<IUserNotifierService, TelegramUserNotifierService>();

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
