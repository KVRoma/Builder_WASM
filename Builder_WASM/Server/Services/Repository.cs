using Builder_WASM.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Builder_WASM.Server.Services
{
    public class Repository<TEntity> where TEntity : class
    {
        internal ApplicationDbContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null!,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                var result = await orderBy(query).ToListAsync();
                return result;
            }
            else
            {
                var result = await query.ToListAsync();
                return result;
            }
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {          
            return (await dbSet.FindAsync(id))!;
        }
               

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id)!;
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Delete(IEnumerable<TEntity> entityToDelete)
        {
            foreach (TEntity entity in entityToDelete)
            {
                if (context.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
                dbSet.Remove(entity);
            }
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate); 
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual bool Exist(object id)
        {
            return (dbSet.Find(id) != null) ? true : false;
        }
    }
}
