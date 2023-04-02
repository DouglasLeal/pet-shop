using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PetShop.Models
{
    public class Animal
    {
        public int Id { get; set; }
        [DisplayName("Nome")]
        public string? Name { get; set; }
        [DisplayName("Cor")]
        public string? Color { get; set; }
        [DisplayName("Observação")]
        public string? Observation { get; set; }
        [DisplayName("Tipo")]
        public AnimalType Type { get; set; }
    }

    public enum AnimalType
    {
        [Display(Name = "Gato")]
        Cat,
        [Display(Name = "Cachorro")]
        Dog
    }
}
