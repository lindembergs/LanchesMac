using LanchesMac.Models;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Components;

public class CarrinhoCompraResumo(CarrinhoCompra _carrinhoCompra) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var itens = _carrinhoCompra.GetCarrinhoCompraItens();
        _carrinhoCompra.CarrinhoCompraItems = itens;

        var carrinhoCompraVM = new CarrinhoCompraViewModel
        {
            CarrinhoCompra = _carrinhoCompra,
            CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoTotal()
        };

        return View(carrinhoCompraVM);
    }
}
