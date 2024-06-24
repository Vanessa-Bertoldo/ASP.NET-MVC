#region IMPORTA��O REFERENTE AO BANCO DE DADOS
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using web.students.Data.Contexts;
using web.students.Models;
using web.students.ViweModels;
using web.students.Logging;
using web.students.Data.Repository;
using web.students.Services;
#endregion

var builder = WebApplication.CreateBuilder(args);

#region INICIALIZANDO O BANCO DE DADOS
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)
);
#endregion

#region Registro IServiceCollection
builder.Services.AddSingleton<ICustomLogger, MockLogger>();
#endregion

#region Registro de Servicos e Repositorios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
#endregion


#region AutoMapper

// Configura��o do AutoMapper
var mapperConfig = new AutoMapper.MapperConfiguration(c => {
    // Permite que cole��es nulas sejam mapeadas
    c.AllowNullCollections = true;
    // Permite que valores de destino nulos sejam mapeados
    c.AllowNullDestinationValues = true;

    // Define o mapeamento de ClienteModel para ClienteCreateViewModel
    c.CreateMap<ClienteModel, ClienteCreateViewModel>();
    // Define o mapeamento reverso de ClienteCreateViewModel para ClienteModel
    c.CreateMap<ClienteCreateViewModel, ClienteModel>();
});

// Cria o mapper com base na configura��o definida
IMapper mapper = mapperConfig.CreateMapper();

// Registra o IMapper como um servi�o singleton no container de DI do ASP.NET Core
builder.Services.AddSingleton(mapper);

#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();