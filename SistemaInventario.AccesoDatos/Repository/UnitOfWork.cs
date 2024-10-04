using SistemaInventario.AccesoDatos.Context;
using SistemaInventario.AccesoDatos.Repository.IRepository;

namespace SistemaInventario.AccesoDatos.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    
    public IBodegaRepository Bodega { get; private set; }
    
    public ICategoriaRepository Categoria { get; private set; }
    
    public IMarcaRepository Marca { get; private set; }
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Bodega = new BodegaRepository(_context);
        Categoria = new CategoriaRepository(_context);
        Marca = new MarcaRepository(_context);
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