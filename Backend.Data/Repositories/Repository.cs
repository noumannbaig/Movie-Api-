using Backend.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Data.Repositories
{
    public class Repository<TDto, TId> : IRepository<TDto, TId> where TDto : class, IDisposable 
    {
        private readonly EFDataContext _context ;
        private DbSet<TDto> table ;

        public Repository(EFDataContext _context)
        {
            this._context = _context;
            table = _context.Set<TDto>();
        }

        public IEnumerable<TDto> GetAll()
        {
            return table.ToList();
        }

        public TDto GetByID(TId id)
        {
            return table.Find(id);
        }

        public void Insert(TDto item)
        {
            table.Add(item);
        }

        public void Update(TDto item)
        {
            table.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(TId id)
        {
            TDto existing = table.Find(id);
            table.Remove(existing);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            _context.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

    }
}
