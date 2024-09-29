using SistemaInventario.AccesoDatos.Context;
using SistemaInventario.AccesoDatos.Repository.IRepository;
using SistemaInventario.Modelos.Models;

namespace SistemaInventario.AccesoDatos.Repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    private readonly ApplicationDbContext _context;
    public CategoriaRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void Update(Categoria categoria)
    {
        var categoriaDB = _context.Categorias.FirstOrDefault(b => b.Id == categoria.Id);
        
        if (categoriaDB != null)
        {
            categoriaDB.Descripcion = categoria.Descripcion;
            categoriaDB.Nombre = categoria.Nombre;
            categoriaDB.Estado = categoria.Estado;
            _context.SaveChanges();
        }
    }
}