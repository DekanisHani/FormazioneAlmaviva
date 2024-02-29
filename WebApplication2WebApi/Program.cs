using Microsoft.EntityFrameworkCore;
using WebApplication2WebApi.Models.DB;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddHttpClient().AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<masterContext>(options => options.UseSqlServer("Data Source=AC-HDEKANIS\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=true;"));
builder.Services.AddMediatR(configuration =>
configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
