using PetShop.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PetShop.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DisplayName("Nome")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DisplayName("Preço de custo")]
        [Range(0, 1000, ErrorMessage = "O valor deve ser no mínimo {1} e no máximo {2}")]
        public decimal CostPrice { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DisplayName("Preço de venda")]
        [Range(0, 1000, ErrorMessage = "O valor deve ser no mínimo {1} e no máximo {2}")]
        public decimal SalePrice { get; set; }

        [DisplayName("Descrição")]
        [StringLength(1000, ErrorMessage = "A {0} deve ter no máximo 1000 caracteres.")]
        public string? Description { get; set; }

        [DisplayName("Tipo")]
        public AnimalType? Type { get; set; }

        [DisplayName("Foto")]
        public string? Photo { get; set; }

        [DisplayName("Foto")]
        public IFormFile? PhotoFile { get; set; }
    }
}
