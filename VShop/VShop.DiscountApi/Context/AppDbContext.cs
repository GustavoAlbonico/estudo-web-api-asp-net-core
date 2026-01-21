using Microsoft.EntityFrameworkCore;
using VShop.DiscountApi.Models;

namespace VShop.DiscountApi.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
{
    public DbSet<Coupon> Coupons {  get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Coupon>()
            .Property(c => c.CouponCode)
            .HasMaxLength(30);

        modelBuilder.Entity<Coupon>()
            .Property(c => c.Discount)
            .HasPrecision(10, 2);

    }
}
