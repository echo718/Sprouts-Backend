using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using Sprouts.Data;
using Sprouts.Models;
using Sprouts.GraphQL.Kids;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Sprouts.GraphQL.Studies
{
    public class StudyType : ObjectType<Study>
    {
        protected override void Configure(IObjectTypeDescriptor<Study> descriptor)
        {
            descriptor.Field(p => p.Id).Type<NonNullType<IdType>>();
            descriptor.Field(p => p.Content).Type<NonNullType<StringType>>();
            descriptor.Field(p => p.Language).Type<NonNullType<StringType>>();
            descriptor.Field(p => p.ImageURI).Type<NonNullType<StringType>>();

            descriptor
                .Field(p => p.Kid)
                .ResolveWith<Resolvers>(r => r.GetKid(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<KidType>>();

          

            descriptor.Field(p => p.Modified).Type<NonNullType<DateTimeType>>();
            descriptor.Field(p => p.Created).Type<NonNullType<DateTimeType>>();

        }


        private class Resolvers
        {
            public async Task<Kid> GetKid(Study study, [ScopedService] AppDbContext context,
               CancellationToken cancellationToken)
            {
                return await context.Kids.FindAsync(new object[] { study.KidId }, cancellationToken);
            }

        
        }
    }
}
