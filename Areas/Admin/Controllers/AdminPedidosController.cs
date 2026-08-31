using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "Admin")]
public class AdminPedidosController(AppDbContext context) : Controller
{
  private readonly AppDbContext _context = context;

  public async Task<IActionResult> Index() =>
    View(await _context.Pedidos.AsNoTracking().OrderByDescending(p => p.PedidoEnviado).ToListAsync());

  public async Task<IActionResult> Details(int id)
  {
    var pedido = await _context.Pedidos
      .Include(p => p.PedidoItens)
      .ThenInclude(i => i.Lanche)
      .AsNoTracking()
      .FirstOrDefaultAsync(p => p.PedidoId == id);

    return pedido is null ? NotFound() : View(pedido);
  }

  public async Task<IActionResult> Edit(int id)
  {
    var pedido = await _context.Pedidos.FindAsync(id);
    return pedido is null ? NotFound() : View(pedido);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, DateTime? pedidoEntregueEm)
  {
    var pedido = await _context.Pedidos.FindAsync(id);
    if (pedido is null) return NotFound();

    pedido.PedidoEntregueEm = pedidoEntregueEm;
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Delete(int id)
  {
    var pedido = await _context.Pedidos.FindAsync(id);
    if (pedido is not null)
    {
      _context.Pedidos.Remove(pedido);
      await _context.SaveChangesAsync();
    }
    return RedirectToAction(nameof(Index));
  }
}
