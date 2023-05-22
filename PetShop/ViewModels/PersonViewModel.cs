using PetShop.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PetShop.ViewModels
{
    public class PersonViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [MinLength(3, ErrorMessage = "{0} precisa ter no mínimo {1} caracteres")]
        [MaxLength(100, ErrorMessage = "{0} possui o limite de {1} caracteres")]
        [DisplayName("Nome")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DisplayName("Tipo")]
        public PersonType Type { get; set; }

        [DisplayName("Foto")]
        public string? Photo { get; set; }

        [DisplayName("Foto")]
        public IFormFile? PhotoFile { get; set; }
    }
}
