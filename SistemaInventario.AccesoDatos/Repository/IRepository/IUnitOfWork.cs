namespace SistemaInventario.AccesoDatos.Repository.IRepository;

public interface IUnitOfWork : IDisposable
{
    IBodegaRepository Bodega { get; }
    
    ICategoriaRepository Categoria { get; }
    
    Task SaveChangesAsync();
}