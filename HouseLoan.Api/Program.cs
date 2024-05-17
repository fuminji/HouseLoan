using HouseLoan.Api.Data;
using HouseLoan.Api.Repositories;
using HouseLoan.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LoanDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("HouseLoanConnectionString")));

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddScoped<ILoanCalculationService, LoanCalculationService>();
builder.Services.AddTransient<ILoanRepository, LoanRepository>();
builder.Services.AddHttpContextAccessor();



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
