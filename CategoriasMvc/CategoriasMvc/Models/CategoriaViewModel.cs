using System.ComponentModel.DataAnnotations;

namespace CategoriasMvc.Models
{
    public class CategoriaViewModel
    {
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "O nome da categoria pe obrigatóri")]
        public string? Nome { get; set; }

        [Required]
        [Display(Name = "Imagem")]
        public string? ImagemUrl { get; set; }
    }
}
