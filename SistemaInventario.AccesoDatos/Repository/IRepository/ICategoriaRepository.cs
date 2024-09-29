using SistemaInventario.Modelos.Models;

namespace SistemaInventario.AccesoDatos.Repository.IRepository;

public interface ICategoriaRepository: IRepository<Categoria>
{
    void Update(Categoria categoria);
}