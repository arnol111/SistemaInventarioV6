using System.Linq.Expressions;
using System.Runtime.Serialization.Json;
using Microsoft.EntityFrameworkCore;
using SistemaInventario.AccesoDatos.Context;
using SistemaInventario.AccesoDatos.Repository.IRepository;

namespace SistemaInventario.AccesoDatos.Repository;

public class Repository<T>: IRepository<T> where T: class
{
    private readonly ApplicationDbContext _context;
    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        this.dbSet = _context.Set<T>();
    }
    public async Task<T> GetById(int id)
    {
        return await dbSet.FindAsync(id); //Select by Id
    }

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
        string includeProperties = null,
        bool disableTracking = true)
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter); // Where ....
        }

        if (includeProperties != null)
        {
            foreach (var prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(prop); //
            }
        }

        if (orderBy != null)
        {
            query= orderBy(query);
        }

        if (!disableTracking)
        {
            query = query.AsNoTracking();
        }
        return await query.ToListAsync();
    }

    public async Task<T> GetFirst(Expression<Func<T, bool>> filter = null, string includeProperties = null, bool disableTracking = true)
    {
        IQueryable<T> query = dbSet;
        if (filter != null)
        {
            query = query.Where(filter); // Where ....
        }

        if (includeProperties != null)
        {
            foreach (var prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(prop); //
            }
        }

        if (!disableTracking)
        {
            query = query.AsNoTracking();
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task Add(T entity)
    {
        await dbSet.AddAsync(entity); //insert into table
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        dbSet.RemoveRange(entities);
    }
}