using System.Reflection;
using CoordinateRectanglesMatcher;
using CoordinateRectanglesMatcher.DAO;
using CoordinateRectanglesMatcher.Models;
using CoordinateRectanglesMatcher.Services;
using CoordinateRectanglesMatcher.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
    .Build();

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddControllers().AddFluentValidation(options =>
{
    // Validate child properties and root collection elements
    options.ImplicitlyValidateChildProperties = true;
    options.ImplicitlyValidateRootCollectionElements = true;

    // Automatic registration of validators in assembly
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRectangleService, RectangleService>();
builder.Services.AddSingleton<ICustomAuthenticationManager, CustomAuthenticationManager>();

builder.Services.AddSingleton<AuthModelValidator>();

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