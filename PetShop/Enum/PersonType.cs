using System.ComponentModel.DataAnnotations;

namespace PetShop.Enum
{
    public enum PersonType
    {
        [Display(Name = "Cliente")]
        Customer,
        [Display(Name = "Empregado")]
        Employee
    }
}
