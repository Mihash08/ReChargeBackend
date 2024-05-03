using Data.Entities;
using Data.Interfaces;

namespace ReCharge.Data.Interfaces
{
    public interface IAdminUserRepository :IRepository<AdminUser>
    {
        Task<AdminUser?> GetByNumberAsync(string number);
        Task<AdminUser?> GetByAccessTokenAsync(string accessToken);
    }
}
