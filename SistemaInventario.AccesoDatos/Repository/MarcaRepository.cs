using SistemaInventario.AccesoDatos.Context;
using SistemaInventario.AccesoDatos.Repository.IRepository;
using SistemaInventario.Modelos.Models;

namespace SistemaInventario.AccesoDatos.Repository;

public class MarcaRepository : Repository<Marca>, IMarcaRepository
{
    private readonly ApplicationDbContext _context;
    
    public MarcaRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void Update(Marca marca)
    {
        var marcaDB = _context.Marcas.FirstOrDefault(b => b.Id == marca.Id);
        
        if (marcaDB != null)
        {
            marcaDB.Descripcion = marca.Descripcion;
            marcaDB.Nombre = marca.Nombre;
            marcaDB.Estado = marca.Estado;
            _context.SaveChanges();
        }
    }
}