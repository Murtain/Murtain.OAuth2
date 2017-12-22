using Murtain.OAuth2.SDK.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Murtain.OAuth2.SDK.UserAccount
{
    public class ValidateMessageCaptchaAsyncRequest
    {
        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        public string Captcha { get; set; }
        /// <summary>
        /// 验证码类型
        /// </summary>
        [Required]
        public MESSAGE_CAPTCHA_TYPE CaptchaType { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        public string Mobile { get; set; }
    }

    /// <summary>
    /// 验证短信验证码返回码
    /// </summary>
    public enum VALIDATE_MESSAGE_CAPTCHA_RETURN_CODE
    {
        /// <summary>
        /// 验证码无效
        /// </summary>
        [Description("验证码无效")]
        INVALID_CAPTCHA = 21000,
        /// <summary>
        /// 验证码已过期，请重新获取验证码
        /// </summary>
        [Description("验证码已过期，请重新获取验证码")]
        EXPIRED_CAPTCHA,
    }

}
