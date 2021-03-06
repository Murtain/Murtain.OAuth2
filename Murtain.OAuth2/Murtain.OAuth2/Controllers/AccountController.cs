﻿using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Murtain.AutoMapper;
using Murtain.Extensions;
using Murtain.OAuth2.Configuration;
using Murtain.OAuth2.Models;
using Murtain.OAuth2.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Controllers
{
    /// <summary>
    /// This controller implements a typical login/logout/provision workflow for local and external accounts.
    /// The login service encapsulates the interactions with the user data store. This data store is in-memory only and cannot be used for production!
    /// The interaction service provides a way for the UI to communicate with identityserver for validation and context retrieval
    /// </summary>
    [SecurityHeaders]
    public class AccountController : Controller
    {
        private readonly TestUserStore store;
        private readonly IIdentityServerInteractionService interactions;
        private readonly IEventService events;
        private readonly AccountService accounts;

        public AccountController(IIdentityServerInteractionService interaction, IClientStore clientStore, IHttpContextAccessor httpContextAccessor, IAuthenticationSchemeProvider schemeProvider, IEventService events, ITempDataProvider tempDataProvider, TestUserStore users = null)
        {
            // if the TestUserStore is not in DI, then we'll just use the global users collection
            store = users ?? new TestUserStore(TestUsers.Users);
            interactions = interaction;
            this.events = events;
            accounts = new AccountService(interaction, httpContextAccessor, schemeProvider, clientStore);
        }
        /// <summary>
        /// Render the page for login
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            // build a model so we know what to show on the login page
            var vm = await accounts.BuildLoginViewModelAsync(returnUrl);

            if (vm.IsExternalLoginOnly)
            {
                // we only have one option for logging in and it's an external provider
                return await ExternalLogin(vm.ExternalLoginScheme, returnUrl);
            }
            TempData["ReturnUrl"] = returnUrl;
            return View(vm);
        }
        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("account/login")]
        public async Task<IActionResult> Login(LoginViewModel model, string button)
        {
            if (button != "login")
            {
                // the user clicked the "cancel" button
                var context = await interactions.GetAuthorizationContextAsync(model.ReturnUrl);
                if (context != null)
                {
                    // if the user cancels, send a result back into IdentityServer as if they 
                    // denied the consent (even if this client does not require consent).
                    // this will send back an access denied OIDC error response to the client.
                    await interactions.GrantConsentAsync(context, ConsentResponse.Denied);

                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    // since we don't have a valid context, then we just go back to the home page
                    return Redirect("~/");
                }
            }

            if (!ModelState.IsValid)
            {
                // something went wrong, show form with error
                var vm = await accounts.BuildLoginViewModelAsync(model);
                await ModelStateCleanAsync(vm);
                return View(vm);
            }

            // validate username/password against in-memory store
            if (!store.ValidateCredentials(model.Username, model.Password))
            {
                await events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials"));
                ModelState.AddModelError("", AccountOptions.InvalidCredentialsErrorMessage);

                // something went wrong, show form with error
                var vm = await accounts.BuildLoginViewModelAsync(model);
                return View(vm);
            }

            var user = store.FindByUsername(model.Username);
            await events.RaiseAsync(new UserLoginSuccessEvent(user.Username, user.SubjectId, user.Username));

            // only set explicit expiration here if user chooses "remember me". 
            // otherwise we rely upon expiration configured in cookie middleware.
            AuthenticationProperties props = null;
            if (AccountOptions.AllowRememberLogin && model.RememberLogin)
            {
                props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                };
            };

            // issue authentication cookie with subject ID and username
            await HttpContext.SignInAsync(user.SubjectId, user.Username, props);

            // make sure the returnUrl is still valid, and if so redirect back to authorize endpoint or a local page
            if (interactions.IsValidReturnUrl(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return Redirect("~/");
        }

        /// <summary>
        /// Render the page for validate user identity
        /// </summary>
        /// <returns></returns>
        [Route("account/validate-id")]
        public async Task<IActionResult> ValidateID(string returnUrl)
        {
            var vm = await accounts.BuildValidateIdViewModelAsync(returnUrl);
            return View(vm);
        }
        /// <summary>
        /// Post processing of validate user identity 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("account/validate-id")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidateID(ValidateIdInput input)
        {
            if (!ModelState.IsValid)
            {
                var vm = input.MapTo<ValidateIdViewModel>();
                await ModelStateCleanAsync(vm);
                return View(vm);
            }

            TempData["Mobile"] = input.Mobile;
            TempData["GraphicCaptcha"] = input.GraphicCaptcha;
            return RedirectToAction("validate-captcha", "account", new { returnUrl = input.ReturnUrl });
        }

        /// <summary>
        /// Render the validate message captcha page 
        /// </summary>
        /// <returns></returns>
        [Route("account/validate-captcha")]
        public async Task<IActionResult> ValidateCaptcha(string returnUrl)
        {
            var vm = await accounts.BuildValidateCaptchaViewModelAsync(returnUrl, TempData["Mobile"] as string, TempData["GraphicCaptcha"] as string);
            return View(vm);
        }
        /// <summary>
        /// Post processing of validate message captcha
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("account/validate-captcha")]
        public async Task<IActionResult> ValidateCaptcha(ValidateCaptchaInput input)
        {
            if (!ModelState.IsValid)
            {
                var vm = input.MapTo<ValidateCaptchaViewModel>();
                await ModelStateCleanAsync(vm);
                return View(vm);
            }
            TempData["Mobile"] = input.Mobile;
            TempData["Captcha"] = input.Captcha;
            return RedirectToAction("password", "account", new { returnUrl = input.ReturnUrl });
        }

        /// <summary>
        /// Render the password page
        /// </summary>
        /// <returns></returns>
        [Route("account/password")]
        public async Task<IActionResult> Password(string returnUrl)
        {
            var vm = await accounts.BuildPasswordViewModelAsync(returnUrl, TempData["Mobile"] as string, TempData["Captcha"] as string);
            return View(vm);
        }
        /// <summary>
        ///  Post processing of set password
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("account/password")]
        public async Task<IActionResult> Password(PasswordInput input)
        {
            if (!ModelState.IsValid)
            {
                var vm = input.MapTo<PasswordViewModel>();
                await ModelStateCleanAsync(vm);
                return View(vm);
            }

            return RedirectToAction("login", "account", new { returnUrl = input.ReturnUrl });
        }

        /// <summary>
        /// initiate roundtrip to external authentication provider
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl)
        {
            var props = new AuthenticationProperties()
            {
                RedirectUri = Url.Action("ExternalLoginCallback"),
                Items =
                {
                    { "returnUrl", returnUrl }
                }
            };

            // windows authentication needs special handling
            // since they don't support the redirect uri, 
            // so this URL is re-triggered when we call challenge
            if (AccountOptions.WindowsAuthenticationSchemeName == provider)
            {
                // see if windows auth has already been requested and succeeded
                var result = await HttpContext.AuthenticateAsync(AccountOptions.WindowsAuthenticationSchemeName);
                if (result?.Principal is WindowsPrincipal wp)
                {
                    props.Items.Add("scheme", AccountOptions.WindowsAuthenticationSchemeName);

                    var id = new ClaimsIdentity(provider);
                    id.AddClaim(new Claim(JwtClaimTypes.Subject, wp.Identity.Name));
                    id.AddClaim(new Claim(JwtClaimTypes.Name, wp.Identity.Name));

                    // add the groups as claims -- be careful if the number of groups is too large
                    if (AccountOptions.IncludeWindowsGroups)
                    {
                        var wi = wp.Identity as WindowsIdentity;
                        var groups = wi.Groups.Translate(typeof(NTAccount));
                        var roles = groups.Select(x => new Claim(JwtClaimTypes.Role, x.Value));
                        id.AddClaims(roles);
                    }

                    await HttpContext.SignInAsync(
                        IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme,
                        new ClaimsPrincipal(id),
                        props);
                    return Redirect(props.RedirectUri);
                }
                else
                {
                    // challenge/trigger windows auth
                    return Challenge(AccountOptions.WindowsAuthenticationSchemeName);
                }
            }
            else
            {
                // start challenge and roundtrip the return URL
                props.Items.Add("scheme", provider);
                return Challenge(props, provider);
            }
        }
        /// <summary>
        /// Post processing of external authentication
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            // read external identity from the temporary cookie
            var result = await HttpContext.AuthenticateAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);
            if (result?.Succeeded != true)
            {
                throw new Exception("External authentication error");
            }

            // retrieve claims of the external user
            var externalUser = result.Principal;
            var claims = externalUser.Claims.ToList();

            // try to determine the unique id of the external user (issued by the provider)
            // the most common claim type for that are the sub claim and the NameIdentifier
            // depending on the external provider, some other claim type might be used
            var userIdClaim = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject);
            if (userIdClaim == null)
            {
                userIdClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            }
            if (userIdClaim == null)
            {
                throw new Exception("Unknown userid");
            }

            // remove the user id claim from the claims collection and move to the userId property
            // also set the name of the external authentication provider
            claims.Remove(userIdClaim);
            var provider = result.Properties.Items["scheme"];
            var userId = userIdClaim.Value;

            // this is where custom logic would most likely be needed to match your users from the
            // external provider's authentication result, and provision the user as you see fit.
            // 
            // check if the external user is already provisioned
            var user = store.FindByExternalProvider(provider, userId);
            if (user == null)
            {
                // this sample simply auto-provisions new external user
                // another common approach is to start a registrations workflow first
                user = store.AutoProvisionUser(provider, userId, claims);
            }

            var additionalClaims = new List<Claim>();

            // if the external system sent a session id claim, copy it over
            // so we can use it for single sign-out
            var sid = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
            if (sid != null)
            {
                additionalClaims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));
            }

            // if the external provider issued an id_token, we'll keep it for signout
            AuthenticationProperties props = null;
            var id_token = result.Properties.GetTokenValue("id_token");
            if (id_token != null)
            {
                props = new AuthenticationProperties();
                props.StoreTokens(new[] { new AuthenticationToken { Name = "id_token", Value = id_token } });
            }

            // issue authentication cookie for user
            await events.RaiseAsync(new UserLoginSuccessEvent(provider, userId, user.SubjectId, user.Username));
            await HttpContext.SignInAsync(user.SubjectId, user.Username, provider, props, additionalClaims.ToArray());

            // delete temporary cookie used during external authentication
            await HttpContext.SignOutAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);

            // validate return URL and redirect back to authorization endpoint or a local page
            var returnUrl = result.Properties.Items["returnUrl"];
            if (interactions.IsValidReturnUrl(returnUrl) || Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect("~/");
        }

        /// <summary>
        /// Render the page for logout
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // build a model so the logout page knows what to display
            var vm = await accounts.BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await Logout(vm);
            }

            return View(vm);
        }
        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            // build a model so the logged out page knows what to display
            var vm = await accounts.BuildLoggedOutViewModelAsync(model.LogoutId);

            var user = HttpContext.User;
            if (user?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await HttpContext.SignOutAsync();

                // raise the logout event
                await events.RaiseAsync(new UserLogoutSuccessEvent(user.GetSubjectId(), user.GetDisplayName()));
            }

            // check if we need to trigger sign-out at an upstream identity provider
            if (vm.TriggerExternalSignout)
            {
                // build a return URL so the upstream provider will redirect back
                // to us after the user has logged out. this allows us to then
                // complete our single sign-out processing.
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });

                // this triggers a redirect to the external provider for sign-out
                return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
            }

            return View("LoggedOut", vm);
        }

        /// <summary>
        /// Clean model state validate errors
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task ModelStateCleanAsync(object model)
        {
            var properties = model.GetType().GetProperties().Select(x => x.Name).ToList();

            var error = ModelState
                    .Where(x => x.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    .Select(m => new { Order = properties.IndexOf(m.Key), Error = m.Value, Key = m.Key })
                    .OrderBy(m => m.Order)
                    .FirstOrDefault();

            if (error != null)
            {
                ModelState.Clear();
                ModelState.AddModelError(error.Key, string.Join(",", error.Error.Errors.Select(x => x.ErrorMessage)));
            }

            await Task.FromResult(0);
        }
    }
}
