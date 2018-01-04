using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Murtain.OAuth2.Configuration;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4;

namespace Murtain.OAuth2.Controllers
{
    /// <summary>
    /// This controller
    /// </summary>
    [SecurityHeaders]
    [Authorize(AuthenticationSchemes = IdentityServerConstants.DefaultCookieAuthenticationScheme)]
    public class PassportController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Security()
        {
            return View();
        }
        public IActionResult Support()
        {
            return View();
        }
    }
}