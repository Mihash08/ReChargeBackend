using Data.Entities;
using Data.Interfaces;

namespace ReCharge.Data.Interfaces
{
    public interface IUserRepository :IRepository<User>
    {
        User? GetByNumber(string number);
        User? GetByAccessToken(string accessToken);
    }
}
