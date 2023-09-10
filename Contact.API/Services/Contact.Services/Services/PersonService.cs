using Contact.Application.Abstraction;
using Contact.Application.Dtos;
using Contact.Application.Repositories;
using Contact.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Services.Services
{
    public class PersonService : GenericServices<Person>, IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IContactInformationRepository _contactInformationRepository;
        public PersonService(IGenericRepository<Person> repository, IGenericUnitOfWork unitofWork, IPersonRepository personRepository, IContactInformationRepository contactInformationRepository) : base(repository, unitofWork)
        {
            _personRepository = personRepository;
            _contactInformationRepository = contactInformationRepository;
        }
        public List<PersonContactDto> GetPersonContactInformations(Guid id)
        {
           var queryablePerson = _personRepository.GetAll();
           var dataPerson = queryablePerson.Where(x=>x.Id == id);
            var queryableContactInformations = _contactInformationRepository.GetAll();
           var listDataInformations = queryableContactInformations.Where(x => x.PersonId == id);

            var query = from prsn in dataPerson
                        join contact in listDataInformations on prsn.Id equals contact.PersonId
                        select new PersonContactDto
                        {
                            Name = prsn.Name,
                            Surname = prsn.Surname,
                            Company = prsn.Company,
                            ContactInformationId = contact.Id,
                            Information = contact.Information,
                            Location = contact.Location,
                            Mail = contact.Mail,
                            PersonId = contact.PersonId,
                            Phone = contact.Phone
                        };

            var result = query.ToList();
            return result;
        }
    }
}
