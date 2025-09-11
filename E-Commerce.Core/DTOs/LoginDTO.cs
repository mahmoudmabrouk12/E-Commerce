using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.DTOs
{
    public record LoginDTO
    {
        public string Email { get; set; }
        public string Pass { get; set; }
    }
    public record ResetPasswordDTO : LoginDTO
    {

        public string Token { get; set; }

    }
}
