using System;
using System.Collections.Generic;
using System.Text;

namespace Murtain.OAuth2.Core.Captcha
{
    public static class CaptchaGenderator
    {
        public static string GetCaptcha()
        {
            return new Random().Next(100000, 999999).ToString();
        }
    }
}
