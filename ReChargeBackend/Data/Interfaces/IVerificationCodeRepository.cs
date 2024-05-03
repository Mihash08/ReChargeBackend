using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IVerificationCodeRepository : IRepository<VerificationCode>
    {
        Task<VerificationCode> GetBySessionAsync(string sessionId);
    }
}
