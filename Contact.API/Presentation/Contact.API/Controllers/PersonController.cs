using AutoMapper;
using Contact.Application.Abstraction;
using Contact.Application.Dtos;
using Contact.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IPersonService _personService;

        public PersonController(IMapper mapper, IPersonService personService)
        {
            _mapper = mapper;
            _personService = personService;
        }
        [HttpGet]
        public async Task<IActionResult> GettAllPersons()
        {
            var persons = await _personService.GetAllAsync();
            var personsDto = _mapper.Map<List<PersonDto>>(persons.ToList());
            return CreateActionResult(CustomResponseDto<List<PersonDto>>.Success(200, personsDto));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var persons = await _personService.GetByIdAsync(id);
            var personsDto = _mapper.Map<PersonDto>(persons);
            return CreateActionResult(CustomResponseDto<PersonDto>.Success(200, personsDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(PersonDto personDto)
        {
            var person = await _personService.AddAsync(_mapper.Map<Person>(personDto));
            return CreateActionResult(CustomResponseDto<Person>.Success(201, person));
        }
        [HttpPut]
        public async Task<IActionResult> Update(PersonDto personDto)
        {
            await _personService.UpdateAsync(_mapper.Map<Person>(personDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var bus = await _personService.GetByIdAsync(id);
            await _personService.RemoveAsync(bus);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        [HttpGet("getcontact/{id}")]
        public async Task<IActionResult> GetPersonContactInformations(Guid id)
        {
            var persons = _personService.GetPersonContactInformations(id);
            var personsDto = _mapper.Map<List<PersonContactDto>>(persons.ToList());
            return CreateActionResult(CustomResponseDto<List<PersonContactDto>>.Success(200, personsDto));
        }
    }
}
