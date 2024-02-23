using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace TinyLink.API.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly TinyLinkDbContext _context;
        private DbSet<T> _dbset;

        public GenericRepository()
        {
            this._context = new TinyLinkDbContext();
            _dbset = _context.Set<T>();
        }
        public GenericRepository(TinyLinkDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbset.Add(entity);
        }
        public void Delete(T entity)
        {
            T existing = _dbset.Find(entity);
            _dbset.Remove(existing);
        }
        public IEnumerable<T> GetAll()
        {
            return _dbset.ToList();
        }
        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbset.Where(expression).AsNoTracking();
        }
        public T GetById(Guid id)
        {
            return _dbset.Find(id);
        }
        public void Update(T entity)
        {
            _dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
