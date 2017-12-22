using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.OAuth2.SDK.Enum;
using Murtain.Dependency;

namespace Murtain.OAuth2.Core.Captcha
{
    public interface ICaptchaManager : ITransientDependency
    {
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="captcha">短信验证码</param>
        /// <param name="mobile">手机号</param>
        /// <param name="expiredTime">失效时间（分钟）</param>
        /// <returns></returns>
        Task MessageCaptchaSendAsync(MESSAGE_CAPTCHA_TYPE captcha, string mobile, int expiredTime);
        /// <summary>
        /// 重发短信验证码
        /// </summary>
        /// <param name="captcha">短信验证码</param>
        /// <param name="mobile">手机号</param>
        /// <param name="expiredTime">失效时间（分钟）</param>
        /// <returns></returns>
        Task MessageCaptchaResendAsync(MESSAGE_CAPTCHA_TYPE captcha, string mobile, int expiredTime);
        /// <summary>
        /// 验证短信验证码
        /// </summary>
        /// <param name="type">验证码类型</param>
        /// <param name="mobile">手机号</param>
        /// <param name="captha">短信验证码</param>
        /// <returns></returns>
        Task MessageCaptchaValidateAsync(MESSAGE_CAPTCHA_TYPE type, string mobile, string captha);
        /// <summary>
        /// 发送邮件验证码
        /// </summary>
        /// <param name="type">验证码类型</param>
        /// <param name="mobile">手机号</param>
        /// <param name="expiredTime">失效时间（分钟）</param>
        /// <returns></returns>
        Task EmailCaptchaSendAsync(MESSAGE_CAPTCHA_TYPE type, string mobile, int expiredTime);
        /// <summary>
        /// 重发邮件验证码
        /// </summary>
        /// <param name="type">验证码类型</param>
        /// <param name="mobile">手机号</param>
        /// <param name="expiredTime">失效时间（分钟）</param>
        /// <returns></returns>
        Task EmailCaptchaResendAsync(MESSAGE_CAPTCHA_TYPE type, string mobile, int expiredTime);
        /// <summary>
        /// 验证邮件验证码
        /// </summary>
        /// <param name="type">验证码类型</param>
        /// <param name="mobile">手机号</param>
        /// <param name="captha">邮件验证码</param>
        /// <returns></returns>
        Task EmailCaptchaValidateAsync(MESSAGE_CAPTCHA_TYPE type, string email, string captha);
        /// <summary>
        /// 生成图形验证码
        /// </summary>
        /// <param name="cookiename">cookie name</param>
        /// <returns></returns>
        Task<byte[]> GraphicCaptchaGenderatorAsync(string cookiename);
        /// <summary>
        /// 验证图形验证码
        /// </summary>
        /// <param name="cookiename">cookie name</param>
        /// <param name="captcha">图形验证码</param>
        /// <returns></returns>
        Task GraphicCaptchaValidateAsync(string cookiename, string captcha);
    }
}
