using Contact.Application.Abstraction;
using Contact.Application.Repositories;
using Contact.Services.Contexts;
using Contact.Services.Repositories;
using Contact.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Services
{
    public static class ServiceRegitration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            //services.AddSingleton<IFilmService, IFilmService>();
            //services.AddDbContext<ContactDbContext>(x =>
            //x.UseSqlServer(Configuration.ConnectionString), ServiceLifetime.Singleton);
            //services.AddDbContext<ContactDbContext>(x=>
            //)

            services.AddDbContext<ContactDbContext>(options =>
            {
                var builder = new NpgsqlConnectionStringBuilder
                {
                    Host = "localhost", // PostgreSQL sunucu adresi
                    Port = 5432, // PostgreSQL port numarası
                    Database = "ContactDB", // Veritabanı adı
                    Username = "postgres", // Veritabanı kullanıcı adı
                    Password = "123456" // Veritabanı şifresi
                };

                options.UseNpgsql(builder.ToString());
            }, ServiceLifetime.Singleton);


            services.AddScoped<IPersonRepository,PersonRepository>();
            services.AddScoped<IPersonService, PersonService>();

            services.AddScoped<IContactInformationRepository, ContactInformationRepository>();
            services.AddScoped<IContactInformationService, ContactInformationService>();
        }
    }
}
