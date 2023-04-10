using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.Application.Models.User.Authentication
{
    public class PasswordRecoveryViewModel
    {
        public string EncryptedUserId { get; set; }
        public string PasswordRecoveryToken { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
