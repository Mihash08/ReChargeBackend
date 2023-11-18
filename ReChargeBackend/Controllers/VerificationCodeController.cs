using BackendReCharge.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BackendReCharge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VerificationCodeController : ControllerBase
    {
        List<VerificationCode> _verificationCodes = new List<VerificationCode>()
            {
                new VerificationCode()
                {
                    Code = "1234",
                    Id = 1,
                    PhoneNumber = "+79251851096",
                }
            };
        private readonly ILogger<VerificationCodeController> _logger;

        public VerificationCodeController(ILogger<VerificationCodeController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetVerificationCodes")]
        public IEnumerable<VerificationCode> GetVerificationCodes()
        {
            return _verificationCodes;
        }
        [HttpGet(Name = "GetVerificationCode")]
        public VerificationCode GetVerificationCode(int id)
        {
            return _verificationCodes.Where(x => x.Id == id).First();
        }
        [HttpGet(Name = "GetVerificationCodeByNumber")]
        public VerificationCode GetVerificationCodeByNumber(string number)
        {
            return _verificationCodes.Where(x => x.PhoneNumber == number).First();
        }
    }
}