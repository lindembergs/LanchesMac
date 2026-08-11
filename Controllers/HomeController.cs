using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;

namespace LanchesMac.Controllers;

public class HomeController(ILancheRepository _lancheRepository) : Controller
{

    public IActionResult Index()
    {
        // TempData["Nome"] = "Lindemberg";
        var homeViewModel = new HomeViewModel
        {
            LanchesPreferidos = _lancheRepository.LanchesPreferidos
        };

        return View(homeViewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
