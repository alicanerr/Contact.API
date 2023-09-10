using Autofac;
using Contact.Application.Abstraction;
using Contact.Application.Repositories;
using Contact.Services.Repositories;
using Contact.Services.Services;

namespace Contact.API
{
    public class Modules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericServices<>)).As(typeof(IGenericServices<>)).InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IGenericUnitOfWork>();

            builder.RegisterType<PersonRepository>().As<IPersonRepository>();
            builder.RegisterType<PersonService>().As<IPersonService>();

            builder.RegisterType<ContactInformationRepository>().As<IContactInformationRepository>();
            builder.RegisterType<ContactInformationService>().As<IContactInformationService>();

        }
    }
}
