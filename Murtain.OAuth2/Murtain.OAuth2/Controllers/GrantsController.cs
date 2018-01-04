using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Murtain.OAuth2.Configuration;
using Murtain.OAuth2.Models;

namespace IdentityServer4.Quickstart.UI
{
    /// <summary>
    /// This controller allows a user to revoke grants given to clients
    /// </summary>
    [SecurityHeaders]
    [Authorize(AuthenticationSchemes = IdentityServerConstants.DefaultCookieAuthenticationScheme)]
    public class GrantsController : Controller
    {
        private readonly IIdentityServerInteractionService interaction;
        private readonly IClientStore clients;
        private readonly IResourceStore resources;

        public GrantsController(IIdentityServerInteractionService interaction,
            IClientStore clients,
            IResourceStore resources)
        {
            this.interaction = interaction;
            this.clients = clients;
            this.resources = resources;
        }

        /// <summary>
        /// Render the page for grants
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View("Index", await BuildViewModelAsync());
        }

        /// <summary>
        /// Handle postback to revoke a client
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Revoke(string clientId)
        {
            await interaction.RevokeUserConsentAsync(clientId);
            return RedirectToAction("Index");
        }

        private async Task<GrantsViewModel> BuildViewModelAsync()
        {
            var grants = await interaction.GetAllUserConsentsAsync();

            var list = new List<GrantViewModel>();
            foreach (var grant in grants)
            {
                var client = await clients.FindClientByIdAsync(grant.ClientId);
                if (client != null)
                {
                    var resources = await this.resources.FindResourcesByScopeAsync(grant.Scopes);

                    var item = new GrantViewModel()
                    {
                        ClientId = client.ClientId,
                        ClientName = client.ClientName ?? client.ClientId,
                        ClientLogoUrl = client.LogoUri,
                        ClientUrl = client.ClientUri,
                        Created = grant.CreationTime,
                        Expires = grant.Expiration,
                        IdentityGrantNames = resources.IdentityResources.Select(x => x.DisplayName ?? x.Name).ToArray(),
                        ApiGrantNames = resources.ApiResources.Select(x => x.DisplayName ?? x.Name).ToArray()
                    };

                    list.Add(item);
                }
            }

            return new GrantsViewModel
            {
                Grants = list
            };
        }
    }
}