using HotChocolate;
using HotChocolate.Types;

namespace Sprouts.GraphQL.Studies
{
    public record DelStudyInput(

         [property: GraphQLType(typeof(NonNullType<IdType>))]

        string StudyId
       );
}
