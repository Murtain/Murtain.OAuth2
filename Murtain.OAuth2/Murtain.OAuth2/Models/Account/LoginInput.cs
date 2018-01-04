using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Models.Account
{
    public class LoginInput : AccountViewModel
    {
        [Required(ErrorMessage = "请输入手机号/邮箱")]
        public string Username { get; set; }
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }

        public bool RememberLogin { get; set; }
    }
}
