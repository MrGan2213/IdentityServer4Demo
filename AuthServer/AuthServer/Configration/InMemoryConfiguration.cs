using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Configration
{
    public class InMemoryConfiguration
    {
        public static IEnumerable<ApiResource> ApiResources()
        {
            return new[]
            {
                new ApiResource("socialnetwork","社交网络")

            };
        }
        public static IEnumerable<Client> Clients()
        {
            return new[]
            {
                new Client
                {
                    ClientId="socialnetwork",
                    ClientSecrets=new []{ new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    AllowedScopes=new[]{ "socialnetwork"}
                },
                new Client
                {
                    ClientId="mvc_implicit",//对应MvcClient端的clientId
                    ClientName="MvcClient",
                    AllowedGrantTypes=GrantTypes.Implicit,//使用Implicit flow时, 首先会重定向到Authorization Server, 然后登陆, 然后Identity Server需要知道是否可以重定向回到网站, 如果不指定重定向返回的地址的话, 我们的Session有可能就会被劫持

                    //RedirectUris是登陆成功之后重定向的网址, 这个网址(http://localhost:5002/signin-oidc)在MvcClient里, 
                    //openid connect中间件使用这个地址就会知道如何处理从authorization server返回的response. 这个地址将会在openid connect 中间件设置合适的cookies, 以确保配置的正确性
                    RedirectUris ={"http://localhost:5002/signin-oidc" },
                    
                    
                    //PostLogoutRedirectUris是登出之后重定向的网址. 有可能发生的情况是, 你登出网站的时候, 会重定向到Authorization Server, 并允许从Authorization Server也进行登出动作
                    PostLogoutRedirectUris ={ "http://localhost:5002/signout-callback-oidc"},

                    
                    
                    
                    AllowedScopes=new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "socialnetwork"
                    }
                }
            };
        }
        public static IEnumerable<TestUser> Users()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId="1",
                    Username="mail@qq.com",
                    Password="password"
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
    }
}
