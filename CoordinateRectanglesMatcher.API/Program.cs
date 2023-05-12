using CoordinateRectanglesMatcher;
using CoordinateRectanglesMatcher.DAO;
using CoordinateRectanglesMatcher.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
    .Build();

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRectangleService, RectangleService>();
builder.Services.AddSingleton<ICustomAuthenticationManager, CustomAuthenticationManager>();

var connectionString = config.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CoordinateMatcherDbContext>(
    options => options.UseSqlServer(connectionString));

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddGlobalErrorHandler();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();