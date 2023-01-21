using Autofac;
using Autofac.Core;
using CampusRecruitment.Entities;
using CampusRecruitment.Entities.Entities;
using CampusRecruitment.Mapper;
using CampusRecruitment.Repository.Common;
using CampusRecruitment.Repository.Interface;
using CampusRecruitment.Repository.Repository;
using CampusRecruitment.Service;
using CampusRecruitment.Service.Interface;
using CampusRecruitment.Service.Service;
using CampusRecruitment.Utils.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args); //new ContainerBuilder();

// Add services to the container.

builder.Services.AddControllers();//.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.IgnoreObsoleteActions();
    c.IgnoreObsoleteProperties();
    c.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddCors(option =>
{
    option.AddPolicy("CorsPolicy",
        builders => builders.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

//Include Configurations in Appsettings
IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
// Duplicate here any configuration sources you use.
configurationBuilder.AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
var config = configurationBuilder.Build();
configurationBuilder.AddJsonFile($"appsettings.{config["EnvironmentName"]}.json", optional: true);

AppConfigBuilder appConfigBuilder = new AppConfigBuilder();
appConfigBuilder.BuildAppSettingProvider(configurationBuilder.Build());

builder.Services.AddDbContext<CampusRecruitmentContext>(x => x.UseSqlServer(AppSetting.GetConfigValue("CampusRecruitmentDatabase")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<ILookUpRepository, LookUpRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ILookUpGroupService, LookUpGroupService>();
builder.Services.AddScoped<ILookUpGroupRepository, LookUpGroupRepository>();

AutoMapper.Mapper.Initialize(mc =>
{
    mc.AddProfile(new AutoMapperConfig());
});


var app = builder.Build();


//Register Components
//builder.RegisterType<UnitOfWork>().As(typeof(IUnitOfWork)).InstancePerLifetimeScope();

////Service Registration
//builder.RegisterAssemblyTypes(Assembly.Load("Impac.TPOSubmitDoc.Services"))
//    .Where(t => t.Name.EndsWith("Service"))
//    .AsImplementedInterfaces().InstancePerLifetimeScope();

////Repository Registration
//builder.RegisterAssemblyTypes(Assembly.Load("Impac.TPOSubmitDoc.Repository"))
//    .Where(t => t.Name.EndsWith("Repository"))
//    .AsImplementedInterfaces().InstancePerLifetimeScope();

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
