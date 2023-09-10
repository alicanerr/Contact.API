using Contact.Application.Repositories;
using Contact.Domain.Models;
using Contact.Services.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Services.Repositories
{
    public class ContactInformationRepository : GenericRepository<ContactInformation>, IContactInformationRepository
    {
        public ContactInformationRepository(ContactDbContext context) : base(context)
        {
        }
    }
}
