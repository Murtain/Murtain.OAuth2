using Murtain.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Models.Account
{
    [AutoMap(typeof(ValidateCaptchaViewModel))]
    public class ValidateCaptchaInput : AccountViewModel
    {
        [Required(ErrorMessage = "请输入6位短信验证码")]
        public string Captcha { get; set; }
        [Required(ErrorMessage = "图形验证码失效，请重新返回上一步注册")]
        public string GraphicCaptcha { get; set; }
        [Required(ErrorMessage = "手机号码失效，请重新返回上一步注册")]
        public string Mobile { get; set; }

        [RegularExpression("true", ErrorMessage = "请阅读《法律声明》及《隐私条款》")]
        public string Agreement { get; set; } = "true";
    }
}
