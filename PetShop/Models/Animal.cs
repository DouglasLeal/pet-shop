﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using PetShop.Enum;

namespace PetShop.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DisplayName("Nome")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [MinLength(3, ErrorMessage = "{0} precisa ter no mínimo {1} caracteres")]
        [MaxLength(50, ErrorMessage = "{0} possui o limite de {1} caracteres")]
        [DisplayName("Cor")]        
        public string? Color { get; set; }

        [MaxLength(1000, ErrorMessage = "{0} possui o limite de {1} caracteres")]
        [DisplayName("Observação")]
        public string? Observation { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório")]
        [DisplayName("Tipo")]
        public AnimalType Type { get; set; }

        [DisplayName("Foto")]
        public string? Photo { get; set; }

        [DisplayName("Dono")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int PersonId { get; set; }

        [DisplayName("Dono")]
        public Person? Person { get; set; }
    }
}
