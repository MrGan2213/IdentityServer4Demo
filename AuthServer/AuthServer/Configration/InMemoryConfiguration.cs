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
    }
}
