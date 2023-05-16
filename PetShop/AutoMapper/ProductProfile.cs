using AutoMapper;
using PetShop.Models;
using PetShop.ViewModels;

namespace PetShop.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();
        }
    }
}
