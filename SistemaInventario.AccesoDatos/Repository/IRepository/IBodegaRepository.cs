using SistemaInventario.Modelos.Models;

namespace SistemaInventario.AccesoDatos.Repository.IRepository;

public interface IBodegaRepository : IRepository<Bodega>
{
    void Update(Bodega bodega);
}