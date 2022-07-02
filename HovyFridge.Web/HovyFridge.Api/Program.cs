using HovyFridge.Api.Data;
using HovyFridge.Api.Data.Repository.GenericRepositoryPattern;
using HovyFridge.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql("Host=127.0.0.1;Port=5432;Database=usersdb;Username=postgres;Password=root");
});


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
