using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.Enum
{
    public enum OAUTH2_LOGIN_PROVIDER_TYPE
    {
        [Description("微信")]
        WECHAT = 1,
        [Description("QQ")]
        QQ,
        [Description("支付宝")]
        ALIPAY,
        [Description("淘宝")]
        TAOBAO,
        [Description("小米")]
        MI,
        [Description("百度")]
        BAIDU,
    }
}
