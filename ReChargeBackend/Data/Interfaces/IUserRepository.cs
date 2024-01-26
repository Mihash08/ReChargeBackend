using Data.Entities;
using Data.Interfaces;

namespace SportsStore.Data.Interfaces
{
    public interface IUserRepository :IRepository<User>
    {
        User? GetByNumber(string number);
    }
}
