using System.ComponentModel.DataAnnotations;

namespace PetShop.Enum
{
    public enum AnimalType
    {
        [Display(Name = "Gato")]
        Cat,
        [Display(Name = "Cachorro")]
        Dog
    }
}
