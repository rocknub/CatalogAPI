using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogAPI.Migrations
{
    public partial class PopulaCategorias : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Matrizes', 'https://www.todamateria.com.br/matrizes-resumo/')");
            mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Matriz Inversa', 'https://www.todamateria.com.br/matriz-inversa/')");
            mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Multiplacação de Matrizes', 'https://www.todamateria.com.br/multiplicacao-de-matrizes/')");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
        }
    }
}
