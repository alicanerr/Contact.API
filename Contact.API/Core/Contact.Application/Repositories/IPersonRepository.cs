using Contact.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Repositories
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
    }
}
