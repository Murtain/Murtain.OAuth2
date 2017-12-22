using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Murtain.EntityFramework;
using Murtain.OAuth2.Domain.Entities;
using System;

namespace Murtain.OAuth2.Infrastructure
{
    [DbContext(typeof(ModelsContainer))]
    public class ModelsContainer : EntityFrameworkDbContext
    {
        public ModelsContainer(DbContextOptions<ModelsContainer> options)
            : base(options)
        {
        }


        public virtual DbSet<UserAccount> UserAccount { get; set; }
    }
}
