
using Murtain.OAuth2.Configuration;
using Murtain.OAuth2.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Murtain.OAuth2.Models
{

    public class LoginViewModel : LoginInput
    {
        public LoginViewModel()
        {
            this.ExternalProviders = new HashSet<ExternalProvider>();
        }
        public bool AllowRememberLogin { get; set; }
        public bool EnableLocalLogin { get; set; }

        public IEnumerable<ExternalProvider> ExternalProviders { get; set; }
        public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders.Where(x => !String.IsNullOrWhiteSpace(x.DisplayName));

        public bool IsExternalLoginOnly => EnableLocalLogin == false && ExternalProviders?.Count() == 1;
        public string ExternalLoginScheme => ExternalProviders?.SingleOrDefault()?.AuthenticationScheme;
    }

}