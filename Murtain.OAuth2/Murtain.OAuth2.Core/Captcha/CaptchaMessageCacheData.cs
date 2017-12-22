using Murtain.OAuth2.SDK.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Murtain.OAuth2.Core.Captcha
{
    public class CaptchaMessageCacheData
    {
        public MESSAGE_CAPTCHA_TYPE Type { get; set; }

        public string Mobile { get; set; }

        public string Captcha { get; set; }

        public TimeSpan? ExpiredTime { get; set; }
    }
}
