using Contact.Application.Dtos;
using Contact.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Application.Abstraction
{
    public interface IPersonService : IGenericServices<Person>
    {
        List<PersonContactDto> GetPersonContactInformations(Guid id);
    }
}
