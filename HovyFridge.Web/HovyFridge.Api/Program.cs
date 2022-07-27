using HovyFridge;
using HovyFridge.QueryBuilder;
using HovyFridge.Services.Auth;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"),
        b => b.MigrationsAssembly("HovyFridge.Api")));


builder.Services.AddScoped<JwtTokensService>();

//builder.Services.UseGenericRepositoryPattern();
builder.Services.UseQueryBuilderPattern();

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
