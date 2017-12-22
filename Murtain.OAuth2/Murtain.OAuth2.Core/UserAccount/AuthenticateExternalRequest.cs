using System;
using System.Collections.Generic;
using System.Text;

namespace Murtain.OAuth2.Core.UserAccount
{

    public class AuthenticateExternalRequest
    {
        /// <summary>
        /// 登录提供程序
        /// </summary>
        public string LoginProvider { get; set; }
        /// <summary>
        /// 登录提供程序ID
        /// </summary>
        public string LoginProviderId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 街道地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }
    }
}
