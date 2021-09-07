using System.Linq;
using System.Security.Claims;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using Sprouts.Data;
using Sprouts.Extensions;
using Sprouts.Models;

namespace Sprouts.GraphQL.Kids
{
    [ExtendObjectType(name: "Query")]
    public class KidQueries
    {
        [UseAppDbContext]
        [UsePaging]
        public IQueryable<Kid> GetKids([ScopedService] AppDbContext context)
        {
            return context.Kids;
        }

        [UseAppDbContext]
        public Kid GetKid([GraphQLType(typeof(NonNullType<IdType>))] string id, [ScopedService] AppDbContext context)
        {
            return context.Kids.Find(int.Parse(id));
        }

        [UseAppDbContext]
        [Authorize]
        public Kid GetSelf(ClaimsPrincipal claimsPrincipal, [ScopedService] AppDbContext context)
        {
            var kidIdStr = claimsPrincipal.Claims.First(c => c.Type == "kidId").Value;

            return context.Kids.Find(int.Parse(kidIdStr));
        }

    }
}
