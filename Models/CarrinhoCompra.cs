using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models;

public class CarrinhoCompra(AppDbContext context)
{
    public string CarrinhoCompraId { get; set; }
    public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

    private readonly AppDbContext _context = context;
    public static CarrinhoCompra GetCarrinho(IServiceProvider services)
    {
        // Define uma sessão
        ISession session =
            services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

        // obtem um serviço do tipo do nosso contexto

        var context = services.GetService<AppDbContext>();

        // obtem ou gera o Id do carrinho
        string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

        // Atribui o id do carrinho

        session.SetString("CarrinhoId", carrinhoId);

        return new CarrinhoCompra(context)
        {
            CarrinhoCompraId = carrinhoId
        };
    }
    public void AdicionarAoCarrinho(Lanche lanche)
    {
        var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId &&
                s.CarrinhoCompraId == CarrinhoCompraId
        );

        if (carrinhoCompraItem == null)
        {
            carrinhoCompraItem = new CarrinhoCompraItem
            {
                CarrinhoCompraId = CarrinhoCompraId,
                Lanche = lanche,
                Quantidade = 1
            };
            _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
        }
        else
            carrinhoCompraItem.Quantidade++;

        _context.SaveChanges();
    }
    public int RemoverDoCarrinho(Lanche lanche)
    {
        var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
            s => s.Lanche.LancheId == lanche.LancheId &&
            s.CarrinhoCompraId == CarrinhoCompraId
        );
        var quandidadeLocal = 0;

        if (carrinhoCompraItem != null)
        {
            if (carrinhoCompraItem.Quantidade > 1)
            {
                carrinhoCompraItem.Quantidade--;
                quandidadeLocal = carrinhoCompraItem.Quantidade;
            }

            _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
        }
        _context.SaveChanges();
        return quandidadeLocal;
    }
    public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
    {
        return CarrinhoCompraItems ??
                (CarrinhoCompraItems =
                        _context.CarrinhoCompraItens
                        .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                        .Include(s => s.Lanche)
                        .ToList());
    }

    public void LimparCarrinho()
    {
        var carrinhoItens = _context.CarrinhoCompraItens
                            .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

        _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
        _context.SaveChanges();
    }

    public decimal GetCarrinhoTotal()
    {
        var total = _context.CarrinhoCompraItens
                    .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                    .Select(c => c.Lanche.Preco * c.Quantidade).Sum();

        return total;
    }
};

