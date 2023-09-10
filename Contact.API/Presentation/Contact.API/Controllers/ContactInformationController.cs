using AutoMapper;
using Contact.Application.Abstraction;
using Contact.Application.Dtos;
using Contact.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInformationController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IContactInformationService _contactInformationService;

        public ContactInformationController(IMapper mapper, IContactInformationService contactInformationService)
        {
            _mapper = mapper;
            _contactInformationService = contactInformationService;
        }
        [HttpGet]
        public async Task<IActionResult> GettAllContactInformations()
        {
            var contactInformation = await _contactInformationService.GetAllAsync();
            var contactInformationDto = _mapper.Map<List<ContactInformationDto>>(contactInformation.ToList());
            return CreateActionResult(CustomResponseDto<List<ContactInformationDto>>.Success(200, contactInformationDto));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var contactInformation = await _contactInformationService.GetByIdAsync(id);
            var contactInformationDto = _mapper.Map<PersonDto>(contactInformation);
            return CreateActionResult(CustomResponseDto<PersonDto>.Success(200, contactInformationDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ContactInformationDto contactInformationDto)
        {
            var contactInformation = await _contactInformationService.AddAsync(_mapper.Map<ContactInformation>(contactInformationDto));
            return CreateActionResult(CustomResponseDto<ContactInformation>.Success(201, contactInformation));
        }
        [HttpPut]
        public async Task<IActionResult> Update(ContactInformationDto contactInformationDto)
        {
            await _contactInformationService.UpdateAsync(_mapper.Map<ContactInformation>(contactInformationDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var bus = await _contactInformationService.GetByIdAsync(id);
            await _contactInformationService.RemoveAsync(bus);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
