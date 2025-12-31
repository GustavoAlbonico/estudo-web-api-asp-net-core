using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using APICatalogo.Validations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace APICatalogo.Models;

[Table("Produtos")]
public class Produto
{
    //[Key]
    public int Id { get; set; }

    //[Required]
    //[StringLength(80, MinimumLength = 5, ErrorMessage = "O nome deve ter pelo menos 5 e 80 caracteres")]
    //[PrimeiraLetraMaiuscula] //validação de atributo reutilizavel
    public string? Nome { get; set; }

    [Required]
    [StringLength(300, ErrorMessage = "A descrição deve ter no maximo {1} caracteres")]
    public string? Descricao { get; set; }

    [Required]
    [Range(1, 1000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }

    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }

    [UseSorting]//graphQL
    [JsonIgnore] //não exige a necessidade de informar esse campo
    [BindNever] //faz não vincular informações ao parametro
    public Categoria? Categoria { get; set; }

    //COMENTADO POR CAUSA DO GRAPHQL
    //Validação de atributo não reutilizavel 
    //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    //{
    //    if (!string.IsNullOrEmpty(this.Nome))
    //    {
    //        var primeiraLetra = this.Nome[0].ToString();
    //        if (primeiraLetra != primeiraLetra.ToUpper())
    //        {
    //            yield return new ValidationResult("A primeira letra do produto deve ser maiúscula",[nameof(this.Nome)]);
    //        }
    //    }

    //    if (this.Estoque <= 0)
    //    {
    //        yield return new ValidationResult("O estoque deve ser maior que zero", [nameof(this.Estoque)]);
    //    }
    //}

}