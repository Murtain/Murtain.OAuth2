using Murtain.Domain;
using Murtain.OAuth2.SDK.Enum;
using Murtain.OAuth2.SDK.UserAccount;
using System;
using System.Threading.Tasks;

namespace Murtain.OAuth2.ApplicationService.UserAccount
{

    public interface IUserAccountService : IApplicationService
    {
        ///// <summary>
        ///// validate graphic captcha and send message captcha
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task ValidateGraphicCaptchaAndSendMessageCaptchaAsync(ValidateGraphicCaptchaAndSendMessageCaptchaAsyncRequest input);
        ///// <summary>
        ///// validate message captcha
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task ValidateMessageCaptchaAsync(ValidateMessageCaptchaAsyncRequest input);
        ///// <summary>
        ///// local registration
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task LocalRegistrationAsync(LocalRegistrationAsyncRequest input);
        ///// <summary>
        ///// resend message captcha
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task ValidateGraphicCaptchaAndResendMessageCaptchaAsync(ValidateGraphicCaptchaAndResendMessageCaptchaAsyncRequest input);
        ///// <summary>
        ///// reset password
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task RetrievePasswordAsync(RetrievePasswordAsyncRequest input);
        /// <summary>
        /// get graphic captcha bytes
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<byte[]> GetGraphicCaptchaAsync(MESSAGE_CAPTCHA_TYPE type);
    }
}
