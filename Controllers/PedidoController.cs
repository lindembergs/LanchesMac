using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;

public class PedidoController(IPedidoRepository _pedido, CarrinhoCompra _carrinhoCompra) : Controller
{
    public IActionResult Checkout()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Checkout(Pedido pedido)
    {
        int totalItensPedido = 0;
        decimal precoTotalPedido = 0.0m;

        // Obtem os itens do carrinho de compra do cliente
        List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItens();
        _carrinhoCompra.CarrinhoCompraItems = items;

        // Verifica se existem itens de pedido

        if (_carrinhoCompra.CarrinhoCompraItems.Count == 0)
            ModelState.AddModelError("", "Seu carrinho esta vazio, que tal incluir um lanche...");

        // Calcular o total de itens e o total do pedido
        foreach (var item in items)
        {
            totalItensPedido += item.Quantidade;
            precoTotalPedido += item.Lanche.Preco * item.Quantidade;
        }

        // Atribui os valores obtidos ao pedido

        pedido.TotalItensPedido = totalItensPedido;
        pedido.PedidoTotal = precoTotalPedido;

        // Valida os dados do pedido
        if (ModelState.IsValid)
        {
            // cria o pedido e os detalhes
            _pedido.CriarPedido(pedido);

            // define mensagens ao cliente

            ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
            ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoTotal();

            // limpar o carrinho
            _carrinhoCompra.LimparCarrinho();

            // Exibe a View com dados do cliente e do Pedido

            return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
        }

        return View(pedido);
    }
}
