using Murtain.Caching;
using Murtain.Exceptions;
using Murtain.OAuth2.Core.Captcha;
using Murtain.OAuth2.Core.UserAccount;
using Murtain.OAuth2.SDK.Enum;
using Murtain.OAuth2.SDK.UserAccount;
using Murtain.Runtime.GraphicGen;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.ApplicationService.UserAccount
{

    public class UserAccountService : IUserAccountService
    {
        //    private readonly ICacheManager cacheManager;
        private readonly ICaptchaManager captchaManager;
        //    private readonly IUserAccountManager userAccountManager;

        public UserAccountService(ICaptchaManager captchaManager)
        {
            //this.cacheManager = cacheManager;
            this.captchaManager = captchaManager;
            //this.userAccountManager = userAccountManager;
        }


        //    public async Task ValidateMessageCaptchaAsync(ValidateMessageCaptchaAsyncRequest input)
        //    {
        //        await captchaManager.MessageCaptchaValidateAsync(input.CaptchaType, input.Mobile, input.Captcha);
        //    }

        //    public async Task LocalRegistrationAsync(LocalRegistrationAsyncRequest input)
        //    {
        //        await userAccountManager.LocalRegistrationAsync(input.Mobile, input.Password);
        //    }

        //    public async Task ValidateGraphicCaptchaAndSendMessageCaptchaAsync(ValidateGraphicCaptchaAndSendMessageCaptchaAsyncRequest input)
        //    {
        //        // try find user by mobile
        //        var user = await userAccountManager.FindAsync(input.Mobile);

        //        switch (input.CaptchaType)
        //        {
        //            case MESSAGE_CAPTCHA_TYPE.REGISTER:

        //                // validate user mobile is exsit
        //                if (user != null)
        //                {
        //                    throw new UserFriendlyException(VALIDATE_GRAPHIC_CAPTCHA_AND_SEND_MESSAGE_CAPTCHA_RETURN_CODE.MOBILE_ALREADY_EXISTS);
        //                }

        //                // validate register graphic captcha 
        //                await captchaManager.GraphicCaptchaValidateAsync("",input.GraphicCaptcha);

        //                // send register message captcha and it valid 10 minutes
        //                await captchaManager.MessageCaptchaSendAsync(MESSAGE_CAPTCHA_TYPE.REGISTER, input.Mobile, 10);

        //                break;

        //            case MESSAGE_CAPTCHA_TYPE.RETRIEVE_PASSWORD:

        //                // validate user mobile is exsit

        //                if (user == null)
        //                {
        //                    throw new UserFriendlyException(VALIDATE_GRAPHIC_CAPTCHA_AND_SEND_MESSAGE_CAPTCHA_RETURN_CODE.MOBILE_NOT_EXISTS);
        //                }
        //                // validate retrieve password graphic captcha 
        //                await captchaManager.GraphicCaptchaValidateAsync("", input.GraphicCaptcha);

        //                // send retrieve password message captcha
        //                await captchaManager.MessageCaptchaSendAsync(MESSAGE_CAPTCHA_TYPE.RETRIEVE_PASSWORD, input.Mobile, 10);
        //                break;

        //            default:
        //                break;
        //        }
        //    }

        //    public async Task RetrievePasswordAsync(RetrievePasswordAsyncRequest input)
        //    {
        //        // try find user by mobile
        //        var user = await userAccountManager.FindAsync(input.Mobile);
        //    }

        public Task<byte[]> GetGraphicCaptchaAsync(MESSAGE_CAPTCHA_TYPE type)
        {

            switch (type)
            {
                case MESSAGE_CAPTCHA_TYPE.REGISTER:

                    // register graphic captcha
                    return Task.FromResult(GraphicCaptchaManager.GetBytes());

                case MESSAGE_CAPTCHA_TYPE.RETRIEVE_PASSWORD:

                    // reset password graphic captcha
                    return Task.FromResult(GraphicCaptchaManager.GetBytes());

                default:
                    break;
            }

            throw new UserFriendlyException(GETGRAPHIC_CAPTCHA_ASYNC_RETURN_CODE.INVALID_GRAPHIC_CAPTCHA);
        }

        //    public async Task ValidateGraphicCaptchaAndResendMessageCaptchaAsync(ValidateGraphicCaptchaAndResendMessageCaptchaAsyncRequest input)
        //    {
        //        await captchaManager.MessageCaptchaResendAsync(MESSAGE_CAPTCHA_TYPE.RETRIEVE_PASSWORD, input.Mobile, 10);
        //    }
    }
}
