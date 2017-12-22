using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.SDK.Enum
{
    public enum IDENTITY_SERVER_USER_SERVICE_RETURN_CODE
    {
        [Description("第三方授权登录失败")]
        AUTHENTICATEEXTERNAL_FAILED,

        [Description("获取用户信息失败")]
        GET_PROFILE_DATA_FAILED
    }
}
