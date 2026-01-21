using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VShop.DiscountApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedCoupons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into coupons (coupon_code, discount)" +
                              "Values('VSHOP_PROMO_10',10)");
            migrationBuilder.Sql("Insert into coupons (coupon_code, discount)" +
                                 "Values('VSHOP_PROMO_20',20)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from coupons");
        }
    }
}
