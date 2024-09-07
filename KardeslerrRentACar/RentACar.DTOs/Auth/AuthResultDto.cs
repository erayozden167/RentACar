using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DTOs.Auth
{
    public class AuthResultDto
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string ErrorMessage { get; set; }
        public string Mail { get; set; }
        public DateTime Expiration { get; set; }
    }

}
