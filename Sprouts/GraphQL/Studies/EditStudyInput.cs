using HotChocolate;
using HotChocolate.Types;

namespace Sprouts.GraphQL.Studies
{
    public record EditStudyInput(

         [property: GraphQLType(typeof(NonNullType<IdType>))]

        string StudyId,
        string Content,
        string Language,
        string? ImageURI);
}
