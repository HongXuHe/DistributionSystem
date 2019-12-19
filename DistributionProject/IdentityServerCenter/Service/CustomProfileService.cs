using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerCenter.Service
{
    public class CustomProfileService : IProfileService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<IdentityUser> _userClaimsPrincipalFactory;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CustomProfileService(UserManager<IdentityUser> userManager, IUserClaimsPrincipalFactory<IdentityUser> userClaimsPrincipalFactory,
            RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            this._roleManager = roleManager;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(subjectId);
            var cliamPrinciple = await _userClaimsPrincipalFactory.CreateAsync(user);
            var cliams = cliamPrinciple.Claims.ToList();
            cliams.Add(new Claim(JwtClaimTypes.PreferredUserName, user.UserName));
            cliams.Add(new Claim("ttt", "tao"));
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                cliams.Add(new Claim(JwtClaimTypes.Role, role));
            }
            context.IssuedClaims = cliams;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = false;
            var subjectId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub").Value;
            var user = await _userManager.FindByIdAsync(subjectId);
            context.IsActive = user != null;
        }
    }
}
