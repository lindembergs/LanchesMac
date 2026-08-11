using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesMac.Migrations
{
    /// <inheritdoc />
    public partial class InsertLanches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO Lanches
(Nome, DescricaoCurta, DescricaoDetalhada, Preco, ImagemUrl, ImagemThumbnailUrl, IsLanchePreferido, EmEstoque, CategoriaId)
VALUES

('X-Burguer',
'Hambúrguer tradicional',
'Hambúrguer bovino, queijo, alface, tomate e molho especial.',
25.90,
'https://images.unsplash.com/photo-1568901346375-23c9450c58cd',
'https://images.unsplash.com/photo-1568901346375-23c9450c58cd?w=300',
1,
1,
1),

('X-Salada',
'Hambúrguer com salada',
'Hambúrguer bovino, queijo, alface, tomate, cebola e maionese.',
27.50,
'https://images.unsplash.com/photo-1550547660-d9450f859349',
'https://images.unsplash.com/photo-1550547660-d9450f859349?w=300',
0,
1,
1),

('X-Bacon',
'Hambúrguer com bacon',
'Hambúrguer bovino, bacon crocante, queijo cheddar e molho barbecue.',
31.90,
'https://images.unsplash.com/photo-1571091718767-18b5b1457add',
'https://images.unsplash.com/photo-1571091718767-18b5b1457add?w=300',
1,
1,
1),

('Cheeseburger',
'Clássico cheeseburger',
'Hambúrguer bovino, queijo cheddar e ketchup.',
21.90,
'https://images.unsplash.com/photo-1586190848861-99aa4a171e90',
'https://images.unsplash.com/photo-1586190848861-99aa4a171e90?w=300',
0,
1,
1),

('Sanduíche Natural de Frango',
'Frango desfiado com ingredientes integrais',
'Pão integral, frango desfiado, alface, tomate e creme de ricota.',
22.90,
'https://images.unsplash.com/photo-1528735602780-2552fd46c7af',
'https://images.unsplash.com/photo-1528735602780-2552fd46c7af?w=300',
1,
1,
2),

('Sanduíche Integral de Atum',
'Atum com vegetais frescos',
'Pão integral, atum, cenoura ralada, alface e tomate.',
24.90,
'https://images.unsplash.com/photo-1509722747041-616f39b57569',
'https://images.unsplash.com/photo-1509722747041-616f39b57569?w=300',
0,
1,
2),

('Wrap Integral de Peito de Peru',
'Wrap saudável',
'Tortilha integral recheada com peito de peru, queijo branco, rúcula e tomate.',
23.50,
'https://images.unsplash.com/photo-1512621776951-a57141f2eefd',
'https://images.unsplash.com/photo-1512621776951-a57141f2eefd?w=300',
1,
1,
2),

('Sanduíche Veggie Integral',
'Lanche vegetariano',
'Pão integral, queijo branco, cenoura, pepino, tomate e alface.',
21.50,
'https://images.unsplash.com/photo-1546069901-ba9599a7e63c',
'https://images.unsplash.com/photo-1546069901-ba9599a7e63c?w=300',
0,
1,
2);
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Lanches");
        }
    }
}
