using Murtain.OAuth2.SDK.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.UserAccount
{
    /// <summary>
    /// 重发短信验证码
    /// </summary>
    public class ValidateGraphicCaptchaAndResendMessageCaptchaAsyncRequest
    {
        /// <summary>
        /// 图像验证码
        /// </summary>
        [Required]
        public string GraphicCaptcha { get; set; }
        /// <summary>
        /// 短信验证码类型
        /// </summary>
        [Required]
        public MESSAGE_CAPTCHA_TYPE MessageCaptchaType { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [Required]
        public string Mobile { get; set; }
    }
}
