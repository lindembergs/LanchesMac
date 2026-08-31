using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace LanchesMac.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "Admin")]
public class AdminProdutosController(AppDbContext context) : Controller
{
    private readonly AppDbContext _context = context;

    public async Task<IActionResult> Index() =>
      View(await _context.Lanches.Include(l => l.Categoria).AsNoTracking().ToListAsync());

    public async Task<IActionResult> Create()
    {
        await CarregarCategoriasAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Lanche lanche)
    {
        ModelState.Remove(nameof(Lanche.LancheId));

        if (!ModelState.IsValid)
        {
            await CarregarCategoriasAsync(lanche.CategoriaId);
            return View(lanche);
        }

        _context.Lanches.Add(lanche);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var lanche = await _context.Lanches.FindAsync(id);
        if (lanche is null) return NotFound();

        await CarregarCategoriasAsync(lanche.CategoriaId);
        return View(lanche);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Lanche lanche)
    {
        if (id != lanche.LancheId) return NotFound();
        if (!ModelState.IsValid)
        {
            await CarregarCategoriasAsync(lanche.CategoriaId);
            return View(lanche);
        }

        _context.Update(lanche);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var lanche = await _context.Lanches.FindAsync(id);
        if (lanche is not null)
        {
            var itensCarrinho = await _context.CarrinhoCompraItens
              .Where(item => item.Lanche.LancheId == id)
              .ToListAsync();

            _context.CarrinhoCompraItens.RemoveRange(itensCarrinho);
            _context.Lanches.Remove(lanche);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task CarregarCategoriasAsync(int? categoriaId = null)
    {
        ViewBag.Categorias = new SelectList(
          await _context.Categorias.AsNoTracking().OrderBy(c => c.CategoriaNome).ToListAsync(),
          nameof(Categoria.CategoriaId), nameof(Categoria.CategoriaNome), categoriaId);
    }
}
