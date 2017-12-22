using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.UserAccount
{
    /// <summary>
    /// 设置/重置密码
    /// </summary>
    public class RetrievePasswordAsyncRequest
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        public string Mobile { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        public string Captcha { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        [Required]
        public string ConfirmPassword { get; set; }
    }


    public enum RETRIEVE_PASSWORD_ASYNC_RETURN_CODE
    {

    }
}
