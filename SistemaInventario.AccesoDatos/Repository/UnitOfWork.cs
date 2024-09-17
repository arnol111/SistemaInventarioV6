using SistemaInventario.AccesoDatos.Context;
using SistemaInventario.AccesoDatos.Repository.IRepository;

namespace SistemaInventario.AccesoDatos.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    
    public IBodegaRepository Bodega { get; private set; }
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Bodega = new BodegaRepository(_context);
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}