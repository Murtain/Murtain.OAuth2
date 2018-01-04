using Murtain.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Models.Account
{
    [AutoMap(typeof(PasswordViewModel))]
    public class PasswordInput : AccountViewModel
    {
        [Required(ErrorMessage = "手机号失效，请返回重新注册")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "短信验证码失效，请返回重新注册。")]
        public string Captcha { get; set; }
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }
        [Required(ErrorMessage = "请再次输入密码")]
        public string ConfirmPassword { get; set; }

        [RegularExpression("true", ErrorMessage = "请阅读《法律声明》及《隐私条款》")]
        public string Agreement { get; set; } = "true";
    }
}
