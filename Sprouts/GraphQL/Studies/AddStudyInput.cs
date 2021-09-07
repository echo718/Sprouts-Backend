namespace Sprouts.GraphQL.Studies
{
    public record AddStudyInput(
        string Content,
        string Language,
        string? ImageURI
        );
}
