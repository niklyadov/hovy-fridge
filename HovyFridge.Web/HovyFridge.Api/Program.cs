using HovyFridge.Api.Services;
using HovyFridge.Data;
using HovyFridge.Data.Repository.GenericRepositoryPattern;
using HovyFridge.Services.Auth;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"), 
        b => b.MigrationsAssembly("HovyFridge.Api")));

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
builder.Services.AddScoped<JwtTokensService>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<FridgeAccessLevelsService>();
builder.Services.AddScoped<ProductSuggestionsService>();

builder.Services.AddControllers();
//builder.Services.AddApiVersioning(cfg =>
//{
//    cfg.DefaultApiVersion = new ApiVersion(1, 0);
//    cfg.AssumeDefaultVersionWhenUnspecified = true;
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//app.UseMiddleware<Authorization>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
