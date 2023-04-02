
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetShop.Models
{
    public class Person
    {
        public int Id { get; set; }
        [DisplayName("Nome")]
        public string? Name { get; set; }
        [DisplayName("Tipo")]
        public PersonType Type { get; set; }
    }

    public enum PersonType
    {
        [Display(Name = "Cliente")]
        Customer,
        Employee
    }
}
