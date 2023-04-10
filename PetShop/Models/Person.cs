
using PetShop.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetShop.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DisplayName("Nome")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [MinLength(3, ErrorMessage = "{0} precisa ter no mínimo {1} caracteres")]
        [MaxLength(100, ErrorMessage = "{0} possui o limite de {1} caracteres")]
        [DisplayName("Tipo")]
        public PersonType Type { get; set; }
    }
}
