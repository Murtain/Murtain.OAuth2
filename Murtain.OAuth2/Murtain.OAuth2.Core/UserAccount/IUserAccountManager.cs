using Murtain.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Core.UserAccount
{
    ///// <summary>
    ///// This interface is about the user account services.
    ///// </summary>
    //public interface IUserAccountManager : IApplicationService, IUnitOfWorkService
    //{
    //    /// <summary>
    //    /// This method get the user account entity by username and password.
    //    /// </summary>
    //    /// <param name="username">username</param>
    //    /// <param name="password">password</param>
    //    /// <returns></returns>
    //    Task<Domain.Entities.UserAccount> AuthenticateLocalAsync(string username, string password);
    //    /// <summary>
    //    /// This method get the user account entity by username and password.
    //    /// If the user does not exist, create a user by user cliams. 
    //    /// </summary>
    //    /// <param name="username"></param>
    //    /// <param name="password"></param>
    //    /// <returns></returns>
    //    Task<Domain.Entities.UserAccount> AuthenticateExternalAsync(AuthenticateExternalRequest input);
    //    /// <summary>
    //    /// This method get the user account entity by subject id.
    //    /// </summary>
    //    /// <param name="subId"></param>
    //    /// <returns></returns>
    //    Task<Domain.Entities.UserAccount> GetProfileDataAsync(string subId);
    //    /// <summary>
    //    /// local registration by mobile
    //    /// </summary>
    //    /// <param name="request"></param>
    //    /// <returns></returns>
    //    Task LocalRegistrationAsync(string mobile, string password);
    //    /// <summary>
    //    /// check user mobile is exist
    //    /// </summary>
    //    /// <param name="mobile"></param>
    //    /// <returns></returns>
    //    Task<Domain.Entities.UserAccount> FindAsync(string mobile);
    //    /// <summary>
    //    /// set or change password
    //    /// </summary>
    //    /// <param name="request"></param>
    //    /// <returns></returns>
    //    Task SavePasswordAsync(string mobile, string password);
    //    /// <summary>
    //    /// set or change email
    //    /// </summary>
    //    /// <param name="request"></param>
    //    /// <returns></returns>
    //    Task SaveEmailAsync(string mobile, string email);
    //}
}
