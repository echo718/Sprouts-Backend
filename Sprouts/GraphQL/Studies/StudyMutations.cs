using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using Sprouts.Models;
using Sprouts.Data;
using Sprouts.Extensions;
using System.Security.Claims;
using System.Linq;
using HotChocolate.AspNetCore;

namespace Sprouts.GraphQL.Studies
{
    [ExtendObjectType(name: "Mutation")]
    public class StudyMutations
    {
        [UseAppDbContext]
        public async Task<Study> AddStudyAsync(AddStudyInput input, ClaimsPrincipal claimsPrincipal,
            [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var kidIdStr = claimsPrincipal.Claims.First(c => c.Type == "kidId").Value;

            var study = new Study
            {
                Content = input.Content,
                Language = input.Language,
                ImageURI = input.ImageURI,
                KidId = int.Parse(kidIdStr),
                Modified = DateTime.Now,
                Created = DateTime.Now,
            };
            context.Studies.Add(study);

            await context.SaveChangesAsync(cancellationToken);

            return study;
        }

        [UseAppDbContext]
        public async Task<Study> EditStudyAsync(EditStudyInput input, ClaimsPrincipal claimsPrincipal,
            [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {

            var kidIdStr = claimsPrincipal.Claims.First(c => c.Type == "kidId").Value;
            var study = await context.Studies.FindAsync(int.Parse(input.StudyId));

            if (study.KidId != int.Parse(kidIdStr))
            {
                throw new GraphQLRequestException(ErrorBuilder.New()
                    .SetMessage("Not owned by kid")
                    .SetCode("AUTH_NOT_AUTHORIZED")
                    .Build());
            }

            study.Content = input.Content ?? study.Content;
            study.Language = input.Language ?? study.Language;
            study.ImageURI = input.ImageURI ?? study.ImageURI;
            study.Modified = DateTime.Now;

          //  context.Studies.Add(study);

            await context.SaveChangesAsync(cancellationToken);

            return study;
        }

        [UseAppDbContext]
        public async Task<Study?> DelStudyAsync(DelStudyInput input, ClaimsPrincipal claimsPrincipal,
          [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var study = await context.Studies.FindAsync(int.Parse(input.StudyId));

            context.Studies.Remove(study);

            await context.SaveChangesAsync(cancellationToken);

            return study;
        }
    }
}
