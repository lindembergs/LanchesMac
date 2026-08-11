using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Repositories;

public class LancheRepository(AppDbContext context) : ILancheRepository
{
    private readonly AppDbContext _context = context;

    public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);

    public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.
                                Where(l => l.IsLanchePreferido)
                                .Include(c => c.Categoria);

    public Lanche GetLancheById(int lancheId) =>
        _context.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
}
