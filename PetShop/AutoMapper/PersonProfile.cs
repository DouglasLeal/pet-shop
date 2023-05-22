using AutoMapper;
using PetShop.Models;
using PetShop.ViewModels;

namespace PetShop.AutoMapper
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonViewModel>();
            CreateMap<PersonViewModel, Person>();
        }
    }
}
