using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Interfaces
{
    public interface IActivityRepository : IRepository<Activity>
    {
        //Task<IEnumerable<Activity>> GetAllWithDetailsAsync();

        //Task<Activity> GetByIdWithDetailsAsync(int id);

        public IEnumerable<Activity> GetByCategory(int categoryId);
    }
}
