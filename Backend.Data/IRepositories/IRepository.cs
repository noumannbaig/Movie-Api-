using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Data.IRepositories
{
    public interface IRepository<TDto , TId> where TDto : IDisposable
    {
        IEnumerable<TDto> GetAll();
        TDto GetByID(TId id);
        void Insert(TDto item);
        void Delete(TId id);
        void Update(TDto item);
        void Save();
    }
}
