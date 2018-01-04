using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Models.Account
{

    public class AccountViewModel
    {
        public string ReturnUrl { get; set; }

        public string LoginUrl
        {
            get
            {
                return "/account/login?returnUrl=" + System.Web.HttpUtility.UrlEncode(ReturnUrl);
            }
        }

        public string ReturnUrlEncode
        {
            get
            {
                return System.Web.HttpUtility.UrlEncode(ReturnUrl);
            }
        }
    }
}
