using Data.Entities;
using Data.Interfaces;

namespace ReCharge.Data.Interfaces
{
    public interface IAdminUserRepository :IRepository<AdminUser>
    {
        AdminUser? GetByNumber(string number);
        AdminUser? GetByAccessToken(string accessToken);
    }
}
