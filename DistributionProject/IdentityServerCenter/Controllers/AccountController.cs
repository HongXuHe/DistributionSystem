using IdentityServer4.Services;
using IdentityServerCenter.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerCenter.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,IIdentityServerInteractionService identityServerInteractionService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._identityServerInteractionService = identityServerInteractionService;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new AppUser() { ReturnUrl = returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> Login(AppUser user)
        {
            var userExist = await _userManager.FindByNameAsync(user.Name);
            if (userExist != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Name, user.Password, false, false);
                if (result.Succeeded)
                {
                    return Redirect(user.ReturnUrl);
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new AppUser() { ReturnUrl = returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> Register(AppUser user)
        {
            var userExist = await _userManager.FindByNameAsync(user.Name);
            if (userExist == null)
            {
                var idUser = new IdentityUser()
                {
                    UserName = user.Name,

                };

                var result = await _userManager.CreateAsync(idUser, user.Password);
                if (result.Succeeded)
                {
                    var r = await _userManager.AddClaimAsync(idUser, new System.Security.Claims.Claim("role", "admin"));
                    if (r.Succeeded)
                    {
                        var res = await _signInManager.PasswordSignInAsync(user.Name, user.Password, false, false);
                        if (res.Succeeded && user.ReturnUrl != null)
                        {
                            return Redirect(user.ReturnUrl);
                        }
                    }
                }
            }
            return View();
        }
    }
}
