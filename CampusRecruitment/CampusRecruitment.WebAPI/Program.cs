using Autofac;
using CampusRecruitment.Entities;
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

var builder = WebApplication.CreateBuilder(args); //new ContainerBuilder();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.IgnoreObsoleteActions();
    c.IgnoreObsoleteProperties();
    c.CustomSchemaIds(type => type.FullName);
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
