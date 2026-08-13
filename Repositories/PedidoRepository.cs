using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;

namespace LanchesMac.Repositories;

public class PedidoRepository(AppDbContext _context, CarrinhoCompra _carrinhoCompra) : IPedidoRepository
{
    public void CriarPedido(Pedido pedido)
    {
        pedido.PedidoEnviado = DateTime.Now;
        _context.Pedidos.Add(pedido);
        _context.SaveChanges();

        var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItems;

        foreach (var carrinhoItem in carrinhoCompraItens)
        {
            var pedidoDetail = new PedidoDetalhe()
            {
                Quantidade = carrinhoItem.Quantidade,
                LancheId = carrinhoItem.Lanche.LancheId,
                PedidoId = pedido.PedidoId,
                Preco = carrinhoItem.Lanche.Preco
            };
            _context.PedidoDetalhes.Add(pedidoDetail);
        }
        _context.SaveChanges();
    }
}
