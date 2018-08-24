using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;


namespace IdentityServer
{
    public class Config
    {
        // scopes define the resources in your system

        public static IEnumerable<IdentityResource> GetIdentityResources()

        {

            return new List<IdentityResource>

            {

                new IdentityResources.OpenId(),

                new IdentityResources.Profile(),

            };

        }

        public static IEnumerable<ApiResource> GetApiResources()

        {

            return new List<ApiResource>

            {

                new ApiResource("basket", "Basket Service")

            };

        }



        // clients want to access resources (aka scopes)

        public static IEnumerable<Client> GetClients()

        {

            // client credentials client

            return new List<Client>

            {

                new Client

                {

                    ClientId = "client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,


                    ClientSecrets =

                    {

                        new Secret("secret".Sha256())

                    },

                    AllowedScopes = { "basket" }

                },



                // resource owner password grant client

                new Client

                {

                    ClientId = "ro.client",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,



                    ClientSecrets =

                    {

                        new Secret("secret".Sha256())

                    },

                    AllowedScopes = { "basket" }

                },



                // OpenID Connect hybrid flow and client credentials client (MVC)

                new Client

                {

                    ClientId = "mvc",

                    ClientName = "MVC Client",

                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    //AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,

                    RequireConsent = false,

                  

                    ClientSecrets =

                    {

                        new Secret("secret".Sha256())

                    },


                    RedirectUris = { "http://localhost:5003/signin-oidc" },

                    PostLogoutRedirectUris = { "http://localhost:5003/signout-callback-oidc" },


                    AllowedScopes =

                    {

                        IdentityServerConstants.StandardScopes.OpenId,

                        IdentityServerConstants.StandardScopes.Profile,

                        "basket"

                    },

                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = true

                }

            };

        }



    }
}
