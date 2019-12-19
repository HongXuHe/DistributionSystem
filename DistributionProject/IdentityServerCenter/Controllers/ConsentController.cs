using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServerCenter.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerCenter.Controllers
{
    public class ConsentController : Controller
    {
        private readonly IClientStore _clientStore;
        private readonly IResourceStore _resourceStore;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;

        public ConsentController(IClientStore clientStore, IResourceStore resourceStore, IIdentityServerInteractionService identityServerInteractionService)
        {
            this._clientStore = clientStore;
            this._resourceStore = resourceStore;
            this._identityServerInteractionService = identityServerInteractionService;
        }
        public async Task<ConsentViewModel> BuildConsentViewModel(string returnUrl)
        {
            var request = await _identityServerInteractionService.GetAuthorizationContextAsync(returnUrl);
            if (request == null)
            {
                return null;
            }
            var client = await _clientStore.FindClientByIdAsync(request.ClientId);
            var resource = await _resourceStore.FindEnabledResourcesByScopeAsync(request.ScopesRequested);
            ConsentViewModel model = new ConsentViewModel();
            model.ClientName = client.ClientName;
            model.ClientLogoUrl = client.LogoUri;
            model.IdentityScopes = resource.IdentityResources.Select(r => new ScopeViewModel
            {
                Name = r.Name,
                DisplayName = r.DisplayName,
                Description = r.Description,
                Checked = r.Required,
                Required = r.Required,
                Emphasize = r.Emphasize
            });
            model.ResorceScopes = resource.ApiResources.SelectMany(x => x.Scopes)
                .Select(r => new ScopeViewModel
                {
                    Name = r.Name,
                    DisplayName = r.DisplayName,
                    Description = r.Description,
                    Checked = r.Required,
                    Required = r.Required,
                    Emphasize = r.Emphasize
                });
            model.ReturnUrl = returnUrl;
            return model;
        }

        [HttpGet]
        public IActionResult Index(string returnUrl)
        {
            
            return View(BuildConsentViewModel(returnUrl).Result);
        }

        [HttpPost]
        public async Task<IActionResult> Index(InputConsentViewModel viewModel)
        {
            viewModel.Button = "yes";
            ConsentResponse response=null;
            if (viewModel.Button == "no")
            {
                response = ConsentResponse.Denied;
            }else if (viewModel.Button == "yes")
            {
                if(viewModel.ScopedConsent !=null && viewModel.ScopedConsent.Any())
                {
                    response = new ConsentResponse()
                    {
                        RememberConsent = viewModel.RememberConsent,
                        ScopesConsented = viewModel.ScopedConsent
                    };
                }
            }
            if(response != null)
            {
                var res =await _identityServerInteractionService.GetAuthorizationContextAsync(viewModel.ReturnUrl);
                await _identityServerInteractionService.GrantConsentAsync(res, response);
                Redirect(viewModel.ReturnUrl);
            }
            return View();
        }
    }
}
