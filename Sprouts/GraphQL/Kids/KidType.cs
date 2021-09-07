using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using HotChocolate.Types;
using Sprouts.Data;
using Sprouts.Models;
using Sprouts.GraphQL.Studies;

namespace Sprouts.GraphQL.Kids
{
    public class KidType : ObjectType<Kid>
    {
        protected override void Configure(IObjectTypeDescriptor<Kid> descriptor)
        {
            descriptor.Field(s => s.Id).Type<NonNullType<IdType>>();
            descriptor.Field(s => s.Name).Type<NonNullType<StringType>>();
            descriptor.Field(s => s.Age).Type<NonNullType<StringType>>();
            descriptor.Field(s => s.GitHub).Type<NonNullType<StringType>>();
            descriptor.Field(s => s.ImageURI).Type<NonNullType<StringType>>();

            descriptor
                .Field(s => s.Studies)
                .ResolveWith<Resolvers>(r => r.GetStudies(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<ListType<NonNullType<StudyType>>>>();

        }

        private class Resolvers
        {
            public async Task<IEnumerable<Study>> GetStudies(Kid kid, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
              
                return await context.Studies.Where(c => c.KidId == kid.Id).ToArrayAsync(cancellationToken);
            }

     
        }
    }
}
