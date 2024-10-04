using System.Linq.Expressions;
using SistemaInventario.AccesoDatos.Context;
using SistemaInventario.Modelos.Models;

namespace SistemaInventario.AccesoDatos.Repository.IRepository;

public interface IMarcaRepository : IRepository<Marca>
{
    void Update(Marca marca);
}