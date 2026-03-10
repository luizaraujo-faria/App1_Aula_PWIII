using System.ComponentModel.DataAnnotations;

namespace PrimeiroApp.Models
{
    public class Usuario
    {
        [Display(Name = "Código")]
        public int UsuId { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório!")]
        [Display(Name = "Nome")]
        public string nomeUsu { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "Cargo é obrigatório!")]
        public string Cargo { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "Data de nascimento é obrigatória!")]
        [DataType(DataType.DateTime)]
        public DateTime DataNasc { get; set; }
    }
}
