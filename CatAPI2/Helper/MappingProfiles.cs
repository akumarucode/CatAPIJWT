using AutoMapper;
using CatAPI2.Dto;
using CatAPI2.Models;

namespace CatAPI2.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        
        {
            CreateMap<Breed, BreedDto>();
            CreateMap<BreedDto, Breed>();

            CreateMap<Cat, CatDto>();
            CreateMap<CatDto, Cat>();

            CreateMap<Country, CountryDto>();

            CreateMap<Owner, OwnerDto>();
        }
    }
}
