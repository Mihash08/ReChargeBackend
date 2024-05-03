using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ISlotRepository : IRepository<Slot>
    {
        Task<IEnumerable<Slot>> GetAllByActivityIdAsync(int activityId);
        Task<IEnumerable<Slot>> GetSlotsByActivityIdAndTimeAsync(int activityId, DateTime dateTime);
        Task<IEnumerable<Slot>> GetSlotsByCategoryIdAndTimeAsync(int categoryId, DateTime dateTime);
    }
}
