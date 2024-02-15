using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll();

        TEntity? GetById(int id);

        TEntity Add(TEntity entity);

        void Delete(TEntity entity);

        void DeleteById(int id);
        TEntity Update(TEntity entity);
    }
}
