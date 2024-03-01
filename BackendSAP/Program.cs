using BackendSAP.Data;
using BackendSAP.Repositorios;
using BackendSAP.Repositorios.IRepositorios;
using Microsoft.EntityFrameworkCore;
using BackendSAP.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"));
}
);

//Agregamos los repositorios
builder.Services.AddScoped<IEstadoRepositorio, EstadoRepositorio>();
builder.Services.AddScoped<ICiudadRepositorio, CiudadRepositorio>();

//Agregar los Automappers
builder.Services.AddAutoMapper(typeof(PaginaWebMapper));

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
