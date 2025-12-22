using System.ComponentModel.DataAnnotations;

namespace CategoriasMvc.Models;

public class ProdutoViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do produto é obrigatório")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "A descrição do produto é obrigatória")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "Informe o preço do produto")]
    public decimal Preco {  get; set; }

    [Display(Name = "Caminho da Imagem")]
    [Required(ErrorMessage = "A imagem do produto é obrigatória")]
    public string? ImagemUrl { get; set; }

    [Display(Name = "Categoria")]
    public int CategoriaId { get; set; }

    public override string? ToString()
    {
        return $"Id:{Id}\nNome {Nome}\nDescricao {Descricao}\nPreco {Preco}\nImagemUrl {ImagemUrl}\nCategoriaId {CategoriaId}";
    }
}
