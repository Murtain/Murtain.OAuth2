using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.Enum
{
    /// <summary>
    /// 邮件验证码类型
    /// </summary>
    public enum EMAIL_CAPTCHA
    {
        /// <summary>
        /// 绑定
        /// </summary>
        [Description("绑定")]
        BIND = 1,
        /// <summary>
        /// 变更
        /// </summary>
        [Description("变更")]
        CHANGE = 2,
    }
}
