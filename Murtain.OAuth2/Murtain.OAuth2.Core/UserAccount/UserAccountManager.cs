using Murtain.OAuth2.Domain.Repositories;
using Murtain.Runtime.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Core.UserAccount
{
    //public class UserAccountManager : IUserAccountManager
    //{
    //    private readonly IUserAccountRepository userAccountRepository;


    //    public UserAccountManager(IUserAccountRepository userAccountRepository)
    //    {
    //        this.userAccountRepository = userAccountRepository;
    //    }

    //    public async Task LocalRegistrationAsync(string mobile, string password)
    //    {
    //        if (userAccountRepository.Any(x => x.Mobile == mobile))
    //        {
    //            throw new UserFriendlyException(USER_ACCOUNT_MANAGER_RETURN_CODE.USER_ALREADY_EXISTS);
    //        }

    //        var entity = new Domain.Entities.UserAccount
    //        {
    //            Mobile = mobile,
    //            Password = encryptPassword(password),
    //            SubjectId = take32Id()
    //        };

    //        await userAccountRepository.AddAsync(entity);
    //    }
    //    public async Task<Domain.Entities.UserAccount> AuthenticateLocalAsync(string username, string password)
    //    {
    //        var user = await userAccountRepository.FirstOrDefaultAsync(x => x.Mobile == username || x.Email == username);

    //        if (user == null || user.Password != encryptPassword(password))
    //        {
    //            return null;
    //        }

    //        return user;
    //    }
    //    public async Task<Domain.Entities.UserAccount> AuthenticateExternalAsync(AuthenticateExternalRequest input)
    //    {
    //        // try find user with login provider and provider id
    //        var user = await userAccountRepository.FirstOrDefaultAsync(x => x.LoginProvider == input.LoginProvider && x.LoginProviderId == input.LoginProviderId);

    //        // if not found create an new user
    //        if (user == null)
    //        {
    //            var entity = input.MapTo<Domain.Entities.UserAccount>();
    //            entity.SubId = take32Id();

    //            return await userAccountRepository.AddAsync(entity);
    //        }

    //        return user;
    //    }
    //    public async Task<Domain.Entities.UserAccount> GetProfileDataAsync(string subId)
    //    {
    //        return await userAccountRepository.FirstOrDefaultAsync(x => x.SubId == subId);
    //    }

    //    public async Task<Domain.Entities.UserAccount> FindAsync(string mobile)
    //    {
    //        return await userAccountRepository.FirstOrDefaultAsync(x => x.Mobile == mobile);
    //    }

    //    public async Task SavePasswordAsync(string mobile, string password)
    //    {
    //        var user = await userAccountRepository.FirstOrDefaultAsync(x => x.Mobile == mobile);

    //        if (user == null)
    //        {
    //            throw new UserFriendlyException(USER_ACCOUNT_MANAGER_RETURN_CODE.USER_NOT_EXISTS);
    //        }

    //        user.Password = encryptPassword(password);

    //        await userAccountRepository.UpdatePropertyAsync(user, x => new { x.Password });
    //    }

    //    public async Task SaveEmailAsync(string mobile, string email)
    //    {
    //        var user = await userAccountRepository.FirstOrDefaultAsync(x => x.Mobile == mobile);

    //        if (user == null)
    //        {
    //            throw new UserFriendlyException(USER_ACCOUNT_MANAGER_RETURN_CODE.USER_NOT_EXISTS);
    //        }

    //        user.Email = email;

    //        await userAccountRepository.UpdatePropertyAsync(user, x => new { x.Password });
    //    }


    //    private string encryptPassword(string password)
    //    {
    //        return CryptoManager.EncryptMD5(password).ToUpper();
    //    }
    //    private string take32Id()
    //    {
    //        return Guid.NewGuid().ToString("N").ToUpper();
    //    }

    //}
}
