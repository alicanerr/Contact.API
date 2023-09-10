using Autofac;
using Autofac.Extensions.DependencyInjection;
using Contact.API;
using Contact.Application.Abstraction;
using Contact.Services;
using Contact.Services.Contexts;
using Contact.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapProfiles));

builder.Services.AddScoped<IGenericUnitOfWork, UnitOfWork>();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerbuilder => containerbuilder.RegisterModule(new Modules()));


builder.Services.AddDbContext<ContactDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ContactDB"), optionsBuilder =>
    {
        optionsBuilder.MigrationsAssembly(Assembly.GetAssembly(typeof(ContactDbContext)).GetName().Name);
    });
});

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
