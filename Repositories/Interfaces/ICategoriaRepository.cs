using LanchesMac.Models;

namespace LanchesMac.Interfaces;

public interface ICategoriaRepository
{
    IEnumerable<Categoria> Categorias { get; }
}
