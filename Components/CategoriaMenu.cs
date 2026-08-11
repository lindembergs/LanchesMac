using LanchesMac.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Components;

public class CategoriaMenu(ICategoriaRepository _categoriaRepository) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var categorias = _categoriaRepository.Categorias
                        .OrderBy(c => c.CategoriaNome);
        return View(categorias);
    }
}
