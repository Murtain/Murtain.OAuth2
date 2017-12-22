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
    /// 验证图形验证码并发送短信验证码
    /// </summary>
    public class ValidateGraphicCaptchaAndSendMessageCaptchaAsyncRequest
    {
        /// <summary>
        /// 图形验证码
        /// </summary>
        [Required]
        public string GraphicCaptcha { get; set; }
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
    /// 验证图形验证码
    /// </summary>
    public enum VALIDATE_GRAPHIC_CAPTCHA_AND_SEND_MESSAGE_CAPTCHA_RETURN_CODE
    {
        /// <summary>
        /// 短信发送次数超出限制
        /// </summary>
        [Description("短信发送次数超出限制")]
        MESSAGES_SENT_OVER_LIMIT,
        /// <summary>
        /// 短信服务不可用
        /// </summary>
        [Description("短信服务不可用")]
        SMS_SERVICE_NOT_AVAILABLE,
        /// <summary>
        /// 短信发送失败
        /// </summary>
        [Description("短信发送失败")]
        MESSAGE_CAPTCHA_SEND_FAILED,
        /// <summary>
        /// 手机号已注册
        /// </summary>
        [Description("手机号已注册")]
        MOBILE_ALREADY_EXISTS,
        /// <summary>
        /// 手机号不存在
        /// </summary>
        [Description("手机号不存在")]
        MOBILE_NOT_EXISTS
    }
}
