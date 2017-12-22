using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

using Murtain.Caching;
using Murtain.OAuth2.SDK.Enum;
using Murtain.Runtime.Security;
using Murtain.Exceptions;
using Murtain.Extensions;
using Murtain.GlobalSetting;
using Microsoft.Extensions.Logging;
using Murtain.Runtime.GraphicGen;
using System.Net;

namespace Murtain.OAuth2.Core.Captcha
{

    public class CaptchaManager : ICaptchaManager
    {

        private readonly ICacheManager cacheManager;
        private readonly IGlobalSettingManager globalSettingManager;

        private const string MESSAGE_CAPTCHA_CACHE_KEY = "";
        //private readonly IEmailSender emailSender;

        public ILogger Logger { get; set; }


        public CaptchaManager(IGlobalSettingManager globalSettingManager, ICacheManager cacheManager)
        {
            this.globalSettingManager = globalSettingManager;
            this.cacheManager = cacheManager;
            //this.emailSender = emailSender;
        }

        public async Task MessageCaptchaSendAsync(MESSAGE_CAPTCHA_TYPE type, string mobile, int expiredTime)
        {
            try
            {
                // generate captcha code
                var captcha = CaptchaGenderator.GetCaptcha();

                // call message captcha api

                // set cache
                cacheManager.Set("", new CaptchaMessageCacheData
                {
                    Captcha = captcha,
                    ExpiredTime = TimeSpan.FromMinutes(expiredTime),
                    Mobile = mobile,
                    Type = type
                });
            }
            catch (WebException e)
            {
                //Logger.Error(e.Message, e);
                throw new UserFriendlyException(CAPTCHA_MANAGER_RETURN_CODE.SMS_SERVICE_NOT_AVAILABLE);
            }
            catch (Exception ex)
            {
                //Logger.Error(e.Message, ex);
                throw new UserFriendlyException(CAPTCHA_MANAGER_RETURN_CODE.MESSAGE_CAPTCHA_SEND_FAILED);
            }
        }

        public Task<byte[]> GraphicCaptchaGenderatorAsync()
        {
            // build and return a graphic captcha bytes 
            return Task.FromResult(GraphicCaptchaManager.GetBytes());
        }

        public Task MessageCaptchaValidateAsync(MESSAGE_CAPTCHA_TYPE captchaType, string mobile, string captcha)
        {
            // try get cache data
            var cache = cacheManager.Get<string>("");

            if (cache == null)
            {
                throw new UserFriendlyException(CAPTCHA_MANAGER_RETURN_CODE.MESSAGE_CAPTCHA_IS_EXPIRED);
            }
            if (cache != captcha)
            {
                throw new UserFriendlyException(CAPTCHA_MANAGER_RETURN_CODE.MESSAGE_CAPTCHA_NOT_MATCHA);
            }

            return Task.FromResult(0);
        }

        public Task GraphicCaptchaValidateAsync(string cookie, string captcha)
        {
            return Task.FromResult(cookie != CryptoManager.EncryptDES(captcha));
        }

        public Task MessageCaptchaResendAsync(MESSAGE_CAPTCHA_TYPE captcha, string mobile, int expiredTime)
        {
            throw new NotImplementedException();
        }

        public Task EmailCaptchaSendAsync(MESSAGE_CAPTCHA_TYPE type, string mobile, int expiredTime)
        {
            throw new NotImplementedException();
        }

        public Task EmailCaptchaResendAsync(MESSAGE_CAPTCHA_TYPE type, string mobile, int expiredTime)
        {
            throw new NotImplementedException();
        }

        public Task EmailCaptchaValidateAsync(MESSAGE_CAPTCHA_TYPE type, string email, string captha)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GraphicCaptchaGenderatorAsync(string cookiename)
        {
            throw new NotImplementedException();
        }


        //public Task ValidateEmailCaptchaAsync(MESSAGE_CAPTCHA_TYPE captchaType, string email, string code)
        //{
        //    var cache = cacheManager.TryGet<string>(Constants.CacheNames.EmailCaptcha, captchaType.TryString(), email, code);

        //    if (cache == null)
        //    {
        //        throw new UserFriendlyException(CAPTCHA_MANAGER_RETURN_CODE.MESSAGE_CAPTCHA_IS_EXPIRED);
        //    }

        //    if (cache != code)
        //    {
        //        throw new UserFriendlyException(CAPTCHA_MANAGER_RETURN_CODE.MESSAGE_CAPTCHA_NOT_MATCHA);
        //    }

        //    return Task.FromResult(0);
        //}

        //public async Task EmailCaptchaSendAsync(MESSAGE_CAPTCHA_TYPE type, string email, int expiredTime)
        //{
        //    // generate captcha code
        //    string code = StringHelper.GenerateCaptcha();
        //    try
        //    {
        //        // send email
        //        await emailSender.SendAsync(email, "账号绑定邮箱安全通知", @"
        //            <p>亲爱的用户，您好</p>
        //            <p>您的验证码是：" + code + @"</p>
        //            <p>此验证码将用于验证身份，修改密码密保等。请勿将验证码透露给其他人。</p>
        //            <p>本邮件由系统自动发送，请勿直接回复！</p>
        //            <p>感谢您的访问，祝您使用愉快！</p>
        //            <p>此致</p>
        //            <p>IT应用支持</p>
        //        ");

        //        // set cache
        //        cacheManager.TrySet(Constants.CacheNames.EmailCaptcha, code, type.TryString(), email, code);

        //    }
        //    catch (Exception e)
        //    {
        //        Logger.Error(e.Message, e);
        //        throw new UserFriendlyException(CAPTCHA_MANAGER_RETURN_CODE.EMAIL_CAPTCHA_SEND_FAILED);
        //    }
        //}
        //public Task EmailCaptchaResendAsync(MESSAGE_CAPTCHA_TYPE type, string mobile, int expiredTime)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
