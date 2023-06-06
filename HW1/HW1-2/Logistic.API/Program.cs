using Logistic.Core.Interfaces;
using Logistic.Core;
using Logistic.Models;
using Logistic.DAL.Interfaces;
using Logistic.DAL;
using Logistic.Models.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IInMemoryRepository<Vehicle>, InMemoryRepository<Vehicle>>();
builder.Services.AddScoped<IEntityLoadingService<Vehicle>, VehicleService>();

//builder.Services.AddSingleton<ISerializeRepository<Vehicle>, JsonRepository<Vehicle>>();
//builder.Services.AddSingleton<ISerializeRepository<Vehicle>, XmlRepository<Vehicle>>();
//builder.Services.AddScoped<IReportService<Vehicle>, ReportService<Vehicle>>();

builder.Services.AddSingleton<ISerializeRepository<Vehicle>, JsonRepository<Vehicle>>();
builder.Services.AddSingleton<ISerializeRepository<Vehicle>, XmlRepository<Vehicle>>();
builder.Services.AddScoped<IReportService<Vehicle>>(provider =>
{
    var jsonRepository = new JsonRepository<Vehicle>();
    var xmlRepository = new XmlRepository<Vehicle>();
    return new ReportService<Vehicle>(jsonRepository, xmlRepository);
});

builder.Services.AddSingleton<IInMemoryRepository<Warehouse>, InMemoryRepository<Warehouse>>();
builder.Services.AddScoped<IEntityLoadingService<Warehouse>, WarehouseService>();

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
