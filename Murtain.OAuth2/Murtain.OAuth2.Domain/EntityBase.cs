using Murtain.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Murtain.OAuth2.Domain
{
    public class AuditedEntityBase : AuditedEntity
    {
        public override DateTime? CreateTime { get; set; }
        [MaxLength(50)]
        public override string CreateUser { get; set; }
        [MaxLength(50)]
        public override string ChangeUser { get; set; }
        public override DateTime? ChangeTime { get; set; }
    }

    public class SoftDeleteEntityBase : AuditedEntityBase, ISoftDeleteEntity
    {
        public virtual bool IsDeleted { get; set; }
    }

    public abstract class PassivableEntityBase : AuditedEntityBase, IPassivableEntity
    {
        public virtual bool IsActived { get; set; }
    }


    public abstract class SoftDeletePassivableEntityBase : AuditedEntityBase, IPassivableEntity, ISoftDeleteEntity
    {
        public virtual bool IsActived { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
