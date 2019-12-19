using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImplictClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Json("cc");
        }
        [Authorize]
        public IActionResult Secret()
        {
            foreach (var claim in User.Claims)
            {

            }
            return Json("Secret");
        }
        [Authorize(Roles ="admin")]
        public IActionResult SecretRole()
        {
            return Json("Role admin");
        }
    }
}