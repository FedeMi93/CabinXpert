using Hotel.AccessData.Repositories;
using Hotel.ApplicationLogic.InterfacesUseCabinType;
using Hotel.ApplicationLogic.InterfacesUseCaseCabins;
using Hotel.ApplicationLogic.InterfacesUseCaseCabinType;
using Hotel.ApplicationLogic.InterfacesUseCaseMaintenance;
using Hotel.ApplicationLogic.InterfacesUseCaseUser;
using Hotel.ApplicationLogic.UseCase;
using Hotel.ApplicationLogic.UsesCases;
using Obligatorio_1.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICabinRepository, SqlRepositoryCabin>();
builder.Services.AddScoped<IMaintenanceRepository, SqlRepositoryMaintenance>();
builder.Services.AddScoped<IUserRepository, SqlRepositoryUsers>();
builder.Services.AddScoped<ICabinTypeRepository, SqlRepositoryCabinType>();

builder.Services.AddScoped<IGetAllCabinsUC, CabinUC>();
builder.Services.AddScoped<IGetByCapacityCabinUC, CabinUC>();
builder.Services.AddScoped<IGetByNameCabinUC, CabinUC>();
builder.Services.AddScoped<IGetByTypeCabinUC, CabinUC>();
builder.Services.AddScoped<IGetOnlyEnableCabinUC, CabinUC>();
builder.Services.AddScoped<IAddCabinUC, CabinUC>();
builder.Services.AddScoped<IGetPictureNameUC, CabinUC>();
builder.Services.AddScoped<IDeleteCabinUC, CabinUC>();


builder.Services.AddScoped<ILoginUC, UserUC>();

builder.Services.AddScoped<IGetAllCabinsTypeUC, CabinTypeUC>();
builder.Services.AddScoped<IGetByIdCabinTypeUC, CabinTypeUC>();
builder.Services.AddScoped<IGetByNameCabinTypeUC, CabinTypeUC>();
builder.Services.AddScoped<IAddCabinTypeUC, CabinTypeUC>();
builder.Services.AddScoped<IUpdateCabinTypeUC, CabinTypeUC>();
builder.Services.AddScoped<IDeleteCabinTypeUC, CabinTypeUC>();

builder.Services.AddScoped<IAddMaintenanceUC, MaintenanceUC>();
builder.Services.AddScoped<IGetMaintenanceByDateUC, MaintenanceUC>();


builder.Services.AddSession();

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}");

app.Run();
