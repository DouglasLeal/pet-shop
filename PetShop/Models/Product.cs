using PetShop.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DisplayName("Nome")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DisplayName("Preço de custo")]
        [Range(0, 1000)]
        public decimal CostPrice { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DisplayName("Preço de venda")]
        [Range(0, 1000)]
        public decimal SalePrice { get; set; }

        [DisplayName("Descrição")]
        [StringLength(1000, ErrorMessage = "A {0} deve ter no máximo 1000 caracteres.")]
        public string? Description { get; set; }

        [DisplayName("Tipo")]
        public AnimalType? Type { get; set; }
    }
}
