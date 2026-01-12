using System.ComponentModel.DataAnnotations;

namespace VShop.Web.Models;

public class CategoryViewModel
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
}
