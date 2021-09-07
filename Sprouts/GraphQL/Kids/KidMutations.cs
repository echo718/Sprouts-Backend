using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using Sprouts.Models;
using Sprouts.Data;
using Sprouts.Extensions;
using Octokit;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections.Generic;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Authorization;
using System.Linq;

namespace Sprouts.GraphQL.Kids
{
    [ExtendObjectType(name: "Mutation")]
    public class KidMutations
    {
        [UseAppDbContext]
        [Authorize]
        public async Task<Kid> EditSelfAsync(EditSelfInput input, ClaimsPrincipal claimsPrincipal,
             [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {

            var kidIdStr = claimsPrincipal.Claims.First(c => c.Type == "kidId").Value;

                       var kidId = int.Parse(kidIdStr);
             var kid = await context.Kids.FindAsync(new object[] { kidId }, cancellationToken);

            kid.Name = input.Name ?? kid.Name;
            kid.Age = input.Age ?? kid.Age;
            // student.GitHub = input.GitHub ?? student.GitHub;
             kid.ImageURI = input.ImageURI ?? kid.ImageURI;

           // context.Kids.Add(kid);

            await context.SaveChangesAsync(cancellationToken);

            return kid;

        }

        [UseAppDbContext]
        public async Task<LoginPayload> LoginAsync(LoginInput input, [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var client = new GitHubClient(new ProductHeaderValue("Sprouts"));

            var request = new OauthTokenRequest(Startup.Configuration["Github:ClientId"], Startup.Configuration["Github:ClientSecret"], input.Code);

            var tokenInfo = await client.Oauth.CreateAccessToken(request);

            if (tokenInfo.AccessToken == null)
            {
                throw new GraphQLRequestException(ErrorBuilder.New()
                    .SetMessage("Bad code")
                    .SetCode("AUTH_NOT_AUTHENTICATED")
                    .Build());
            }

            client.Credentials = new Credentials(tokenInfo.AccessToken);
            var user = await client.User.Current();


            var kid = await context.Kids.FirstOrDefaultAsync(s => s.GitHub == user.Login, cancellationToken);
            if (kid == null)
            {
                kid = new Kid
                {
                    Name = user.Name ?? user.Login,
                    Age = "none",
                    GitHub = user.Login,
                    ImageURI = user.AvatarUrl,
                };

                context.Kids.Add(kid);
                await context.SaveChangesAsync(cancellationToken);
            }

            // authentication successful so generate jwt token
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.Configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>{
                new Claim("kidId", kid.Id.ToString()),
            };

            var jwtToken = new JwtSecurityToken(
                "Sprouts",
                "Sprouts",
                claims,
                expires: DateTime.Now.AddDays(90),
                signingCredentials: credentials);

            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new LoginPayload(kid, token);
        }

    }
}
