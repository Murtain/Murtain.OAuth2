using Murtain.EntityFramework.Provider;
using Murtain.OAuth2.Domain.Entities;
using Murtain.OAuth2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Murtain.OAuth2.Infrastructure.Repositories
{
    public class UserAccountRepository : Repository<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(IEntityFrameworkDbContextProvider<ModelsContainer> dbContextProvider)
             : base(dbContextProvider)
        {

        }
    }
}
