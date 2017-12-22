using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.Enum
{
    public enum CAPTCHA_MANAGER_RETURN_CODE
    {
        /// <summary>
        /// 邮件发送失败
        /// </summary>
        [Description("邮件发送失败")]
        EMAIL_CAPTCHA_SEND_FAILED,
        /// <summary>
        /// 短信发送失败
        /// </summary>
        [Description("短信发送失败")]
        MESSAGE_CAPTCHA_SEND_FAILED,
        /// <summary>
        /// 短信服务不可用
        /// </summary>
        [Description("短信服务不可用")]
        SMS_SERVICE_NOT_AVAILABLE,
        /// <summary>
        /// 验证码已失效
        /// </summary>
        [Description("验证码已失效")]
        MESSAGE_CAPTCHA_IS_EXPIRED,
        /// <summary>
        /// 验证码错误
        /// </summary>
        [Description("验证码错误")]
        MESSAGE_CAPTCHA_NOT_MATCHA,
        /// <summary>
        /// 图形验证码错误
        /// </summary>
        [Description("图形验证码错误")]
        INVALID_GRAPHIC_CAPTCHA
    }
}
