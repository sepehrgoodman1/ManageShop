using ManageShop.Persistence.Ef;
using ManageShop.Persistence.Ef.ProductGroups;
using ManageShop.Services.ProductGroups;
using ManageShop.Services.ProductGroups.Contracts;
using Microsoft.EntityFrameworkCore;
using Taav.Contracts.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<EFDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ProductGroupService, ProductGroupAppService>();
builder.Services.AddScoped<ProductGroupRepository, EFProductGroupRepository>();

builder.Services.AddScoped<UnitOfWork, EFUnitOfWork>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
