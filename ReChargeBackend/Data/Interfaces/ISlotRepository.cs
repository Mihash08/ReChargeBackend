using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ISlotRepository : IRepository<Slot>
    {
        IEnumerable<Slot> GetAllByActivityId(int activityId);
        IEnumerable<Slot> GetSlotsByActivityIdAndTime(int activityId, DateTime dateTime);
        IEnumerable<Slot> GetSlotsByCategoryIdAndTime(int categoryId, DateTime dateTime);
    }
}
