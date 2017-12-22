using Murtain.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Murtain.OAuth2.Domain.Entities
{
    public class UserAccount : SoftDeletePassivableEntityBase
    {
        /// <summary>
        /// 真实姓名
        /// </summary>
        [MaxLength(50)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(50)]
        public virtual string NickName { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        [MaxLength(50)]
        public string Birthday { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(50)]
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50)]
        public virtual string Email { get; set; }
        /// <summary>
        /// 街道地址
        /// </summary>
        [MaxLength(250)]
        public virtual string Address { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        [MaxLength(50)]
        public virtual string City { get; set; }
        /// <summary>
        /// 所在省
        /// </summary>
        [MaxLength(50)]
        public virtual string Province { get; set; }
        /// <summary>
        /// 所在国家
        /// </summary>
        [MaxLength(50)]
        public virtual string Country { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [MaxLength(50)]
        public virtual string Gender { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        [MaxLength(2000)]
        public virtual string Avatar { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(250)]
        public virtual string Password { get; set; }
        /// <summary>
        /// Subject
        /// </summary>
        [Required]
        [MaxLength(50)]
        public virtual string SubjectId { get; set; }
        /// <summary>
        /// 登录提供程序
        /// </summary>
        [MaxLength(50)]
        public virtual string LoginProvider { get; set; }
        /// <summary>
        /// 登录提供程序Id
        /// </summary>
        [MaxLength(50)]
        public virtual string LoginProviderId { get; set; }
    }
}
