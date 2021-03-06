using System.Data.Entity;
public class EFCommandRepository<T> : ICommandRepository<T> where T : class
{
    protected readonly DbContext dbContext;
    protected readonly DbSet<T> dbSet;

    public EFCommandRepository(DbContext dbContext)
    {
        if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
        this.dbContext = dbContext;
        dbSet = this.dbContext.Set<T>();
    }
    public virtual void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public virtual void AddRange(IEnumerable<T> entities)
    {
        dbSet.AddRange(entities);
    }

    public virtual T Find(params object[] keyValues)
    {
        return dbSet.Find(keyValues);
    }

    public virtual void Remove(T entity)
    {
        if (dbContext.Entry(entity).State == EntityState.Detached) dbSet.Attach(entity);
        dbSet.Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        try
        {
            dbContext.Configuration.AutoDetectChangesEnabled = false;
            foreach (var e in entities) Remove(e);
        }
        finally
        {
            dbContext.Configuration.AutoDetectChangesEnabled = true;
        }
    }

    public virtual void Update(T entity)
    {
        if (dbContext.Entry(entity).State == EntityState.Detached) dbSet.Attach(entity);
        dbContext.Entry(entity).State = EntityState.Modified;
    }

    public virtual void UpdateRange(IEnumerable<T> entities)
    {
        try
        {
            dbContext.Configuration.AutoDetectChangesEnabled = false;
            foreach (var e in entities) Update(e);
        }
        finally
        {
            dbContext.Configuration.AutoDetectChangesEnabled = true;
        }
    }
}