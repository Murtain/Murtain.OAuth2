using Microsoft.AspNetCore.Mvc;
using Murtain.OAuth2.ApplicationService.UserAccount;
using Murtain.OAuth2.SDK.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Murtain.OAuth2.Controllers
{
    public class GraphicCaptchaController : Controller
    {
        private readonly IUserAccountService userAccountService;
        public GraphicCaptchaController(IUserAccountService userAccountService)
        {
            this.userAccountService = userAccountService;
        }

        [HttpGet]
        [Route("api/captcha/{captcha_type}")]
        public async Task<ActionResult> GetGraphicCaptchaAsync(MESSAGE_CAPTCHA_TYPE captcha_type)
        {
            return File(await userAccountService.GetGraphicCaptchaAsync(captcha_type), @"image/jpeg");
        }
    }
}
