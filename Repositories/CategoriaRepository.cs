using LanchesMac.Context;
using LanchesMac.Interfaces;
using LanchesMac.Models;

namespace LanchesMac.Repositories;

public class CategoriaRepository(AppDbContext contexto) : ICategoriaRepository
{
    private readonly AppDbContext _context = contexto;

    public IEnumerable<Categoria> Categorias => _context.Categorias;
}
