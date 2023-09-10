using Contact.Application.Abstraction;
using Contact.Application.Repositories;
using Contact.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Services.Services
{
    public class ContactInformationService : GenericServices<ContactInformation>, IContactInformationService
    {
        private readonly IContactInformationRepository _contactInformationRepository;
        public ContactInformationService(IGenericRepository<ContactInformation> repository, IGenericUnitOfWork unitofWork, IContactInformationRepository contactInformationRepository) : base(repository, unitofWork)
        {
            _contactInformationRepository = contactInformationRepository;
        }
    }
}
