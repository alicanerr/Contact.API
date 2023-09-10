using AutoMapper;
using Contact.Application.Dtos;
using Contact.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Services
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<ContactInformation, ContactInformationDto>().ReverseMap();

        }
    }
}
