using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VShop.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into products (name, price, description, stock, image_url,category_id)" +
                                 "Values('Caderno',7.55,'Caderno',10,'caderno1.jpg',1)");
            migrationBuilder.Sql("Insert into products (name, price, description, stock, image_url,category_id)" +
                                 "Values('Lápis',3.45,'Lápis Preto',20,'lapis1.jpg',1)");
            migrationBuilder.Sql("Insert into products (name, price, description, stock, image_url,category_id)" +
                                 "Values('Clips',5.33,'Clips para papel',10,'clips1.jpg',2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from products");
        }
    }
}
