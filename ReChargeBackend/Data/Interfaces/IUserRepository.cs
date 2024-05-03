using Data.Entities;
using Data.Interfaces;

namespace ReCharge.Data.Interfaces
{
    public interface IUserRepository :IRepository<User>
    {
        Task<User?> GetByNumberAsync(string number);
        Task<User?> GetByAccessTokenAsync(string accessToken);
    }
}
