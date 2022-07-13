using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogAPI.Migrations
{
    public partial class PopulaProdutos : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "VALUES ('Pudin de Limão Doce', 'Pudim mágico com textura doce e sabor de azede.', 50.00, 'pudindocelimao.png', 15, now(), 1)");
            mb.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "VALUES ('Bolo de Limão', 'Bolo mágico com textura azeda.', 75.00, 'bolodelimao.png', 10, now(), 2)");
            mb.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) " +
                "VALUES ('Torta Doce', 'Torta mágica com textura doce.', 25.00, 'pudindocelimao.png', 5, now(), 3)");
            
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Produtos");
        }
    }
}
