using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerCenter
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("api")
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                     ClientId="mvc",
                     ClientName="mvc_name",
                     ClientSecrets= new []{ new Secret("mvc_secret".Sha256())},
                     AllowedGrantTypes=GrantTypes.Implicit,
                    AllowedScopes =
                    {
                        "api",
                        IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                        "roles"
                    },
                    RedirectUris={"https://localhost:44371/signin-oidc"},
                    PostLogoutRedirectUris={"https://localhost:44371/signout-callback-oidc"},
                    RequireClientSecret=false,
                     RequireConsent=false
                }
            };
        }
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles","admin",new string[]{"role"})
            };
        }
    }
}
