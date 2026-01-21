using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VShop.DiscountApi.Models;

public class Coupon
{
    public int Id { get; set; }

    public string? CouponCode { get; set; }
   
    public decimal Discount { get; set; }
}
