using AutoMapper;
using PetShop.Models;
using PetShop.ViewModels;

namespace PetShop.AutoMapper
{
    public class AnimalProfile : Profile
    {
        public AnimalProfile()
        {
            CreateMap<Animal, AnimalViewModel>();
            CreateMap<AnimalViewModel, Animal>();
        }
    }
}
