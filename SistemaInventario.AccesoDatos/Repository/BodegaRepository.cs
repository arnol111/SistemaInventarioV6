using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Context;
using SistemaInventario.AccesoDatos.Repository.IRepository;
using SistemaInventario.Modelos.Models;

namespace SistemaInventario.AccesoDatos.Repository;

public class BodegaRepository : Repository<Bodega>, IBodegaRepository
{
    private readonly ApplicationDbContext _context;
    
    public BodegaRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public void Update(Bodega bodega)
    {
        var bodegaDb = _context.Bodegas.FirstOrDefault(b => b.Id == bodega.Id);
        if (bodegaDb != null)
        {
            bodegaDb.Descripcion = bodega.Descripcion;
            bodegaDb.Nombre = bodega.Nombre;
            bodegaDb.Estado = bodega.Estado;
            
            _context.SaveChanges();
        }
    }
}