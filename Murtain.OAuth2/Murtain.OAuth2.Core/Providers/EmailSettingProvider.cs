using Murtain.GlobalSetting.Provider;
using System;
using System.Collections.Generic;
using System.Text;
using Murtain.GlobalSetting.Models;

namespace Murtain.OAuth2.Core.Providers
{
    public class EmailSettingProvider : GlobalSettingProvider
    {
        public override IEnumerable<GlobalSetting.Models.GlobalSetting> GetSettings()
        {
            return new GlobalSetting.Models.GlobalSetting[] { };
        }
    }
}
