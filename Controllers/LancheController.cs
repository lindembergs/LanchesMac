using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;

public class LancheController(ILancheRepository _lanches) : Controller
{
    public IActionResult List(string categoria)
    {
        ViewData["Titulo"] = "Todos os lanches";
        ViewData["Data"] = DateTime.Now;

        var total = "Total de lanches : ";
        var totalLanches = _lanches.Lanches.Count();

        ViewBag.Total = total;
        ViewBag.TotalLanches = totalLanches;

        IEnumerable<Lanche> lanches;
        string categoriaAtual = string.Empty;

        if (string.IsNullOrEmpty(categoria))
        {
            lanches = _lanches.Lanches.OrderBy(l => l.LancheId);
            categoriaAtual = "Todos os lanches";
        }
        else
        {
            lanches = _lanches.Lanches.Where(l => l.Categoria.CategoriaNome == categoria)
                      .OrderBy(c => c.Categoria.CategoriaNome);
            categoriaAtual = categoria;
        }

        var lanchesListViewModel = new LancheListViewModel
        {
            Lanches = lanches,
            CategoriaAtual = categoriaAtual
        };

        return View(lanchesListViewModel);
    }

    public IActionResult Details(int lancheId)
    {
        var lanche = _lanches.Lanches.FirstOrDefault(l => l.LancheId == lancheId);

        if (lanche is null)
            return NotFound();

        return View(lanche);
    }

    public ViewResult Search(string searchString)
    {
        IEnumerable<Lanche> lanches;
        string categoriaAtual = string.Empty;

        if (string.IsNullOrEmpty(searchString))
        {
            lanches = _lanches.Lanches.OrderBy(l => l.LancheId);
            categoriaAtual = "Todos os Lanches";
        }
        else
        {
            lanches = _lanches.Lanches.Where(l => l.Nome.ToLower().Contains(searchString.ToLower()));

            if (lanches.Any())
                categoriaAtual = "Lanches";
            else
                categoriaAtual = "Nenhum lanche foi encontrado";
        }

        return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
        {
            Lanches = lanches,
            CategoriaAtual = categoriaAtual
        });
    }
}
