using System.Linq.Expressions;

namespace SistemaInventario.AccesoDatos.Repository.IRepository;

public interface IRepository<T> where T: class
{
    Task<T> GetById(int id);
    
    Task<IEnumerable<T>> GetAll(
        Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null, 
            bool disableTracking = true
        );
    
    Task<T> GetFirst(Expression<Func<T, bool>> filter = null,
        string includeProperties = null, 
        bool disableTracking = true
    );
    
    Task Add (T entity);
    void Remove (T entity);
    void RemoveRange(IEnumerable<T> entities);
}