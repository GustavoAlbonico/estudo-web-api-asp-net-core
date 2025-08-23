using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias (nome, imagem_url) VALUES('Bebida','bebida.jpg')");
            migrationBuilder.Sql("INSERT INTO Categorias (nome, imagem_url) VALUES('Lanches','lanches.jpg')");
            migrationBuilder.Sql("INSERT INTO Categorias (nome, imagem_url) VALUES('Sobremesas','sobremesas.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM CATEGORIAS");
        }
    }
}
