using Murtain.OAuth2.SDK.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.UserAccount
{
    /// <summary>
    /// 本地注册
    /// </summary>
    public class LocalRegistrationAsyncRequest
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        public string Mobile { get; set; }
        /// <summary>
        /// 验证码类型
        /// </summary>
        [Required]
        public MESSAGE_CAPTCHA_TYPE CaptchaType { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }
    }

    /// <summary>
    /// 本地注册返回码
    /// </summary>
    public enum LOCAL_REGISTRATION_RETURN_CODE
    {
        /// <summary>
        /// 手机号码已存在
        /// </summary>
        [Description("手机号码已存在")]
        USER_ALREADY_EXISTS
    }
}
