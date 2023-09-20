using Hotel.AccessData.Repositories;
using Hotel.ApplicationLogic.InterfacesUseCabinType;
using Hotel.ApplicationLogic.InterfacesUseCaseCabins;
using Hotel.ApplicationLogic.InterfacesUseCaseCabinType;
using Hotel.ApplicationLogic.InterfacesUseCaseMaintenance;
using Hotel.ApplicationLogic.InterfacesUseCaseUser;
using Hotel.ApplicationLogic.UseCase;
using Hotel.ApplicationLogic.UsesCases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Obligatorio_1.Interfaces;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var rutaArchivo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Hotel.WebApi.xml");
builder.Services.AddSwaggerGen(opciones =>
{
    //Se agrega la opcion de autenticarse en Swagger

    opciones.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
    {
        Description = "Autorizacion estandar mediante esquema Bearer",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    opciones.OperationFilter<SecurityRequirementsOperationFilter>();

    opciones.IncludeXmlComments(rutaArchivo);
    opciones.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Documentación de Hotel.WebApi.",
        Description = "Aqui se encuentran todos los endpoint activos para utilizar los servicios del proyecto Hotel Obligatorio P3",
        Contact = new OpenApiContact
        {
            Email = "federico.millot@gmail.com"
        },
        Version = "v1"
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{ 
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes
        (builder.Configuration.GetSection("AppSettings:SecretTokenKey").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };

});

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

});

builder.Services.AddScoped<ICabinRepository, SqlRepositoryCabin>();
builder.Services.AddScoped<IMaintenanceRepository, SqlRepositoryMaintenance>();
builder.Services.AddScoped<IUserRepository, SqlRepositoryUsers>();
builder.Services.AddScoped<ICabinTypeRepository, SqlRepositoryCabinType>();

builder.Services.AddScoped<IGetAllCabinsDtoUC, CabinUC>();
builder.Services.AddScoped<IGetByCapacityCabinsDtoUC, CabinUC>();
builder.Services.AddScoped<IGetByNameCabinDtoUC, CabinUC>();
builder.Services.AddScoped<IGetByTypeCabinDtoUC, CabinUC>();
builder.Services.AddScoped<IGetOnlyEnableCabinDtoUC, CabinUC>();
builder.Services.AddScoped<IAddCabinDtoUC, CabinUC>();
builder.Services.AddScoped<IGetPictureNameUC, CabinUC>();
builder.Services.AddScoped<IDeleteCabinUC, CabinUC>();
builder.Services.AddScoped<IGetCabinDtoByCostUC, CabinUC>();
builder.Services.AddScoped<IGetByIdCabinUC, CabinUC>();

builder.Services.AddScoped<ILoginUC, UserUC>();
builder.Services.AddScoped<ILoginDtoUC, UserUC>();

builder.Services.AddScoped<IGetAllCabinTypeDtoUC, CabinTypeUC>();
builder.Services.AddScoped<IGetByIdCabinTypeDtoUC, CabinTypeUC>();
builder.Services.AddScoped<IGetByNameCabinTypeDtoUC, CabinTypeUC>();
builder.Services.AddScoped<IAddCabinTypeDtoUC, CabinTypeUC>();
builder.Services.AddScoped<IUpdateCabinTypeUC, CabinTypeUC>();
builder.Services.AddScoped<IDeleteCabinTypeUC, CabinTypeUC>();
builder.Services.AddScoped<IGetByIdCabinTypeUC, CabinTypeUC>();


builder.Services.AddScoped<IAddMaintenanceDtoUC, MaintenanceUC>();
builder.Services.AddScoped<IGetMaintenancesDtoByDateUC, MaintenanceUC>();
builder.Services.AddScoped<IGetMaintenancesByCabinCapacityUC, MaintenanceUC>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
