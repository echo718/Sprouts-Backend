namespace Sprouts.GraphQL.Kids
{
    public record AddKidInput(
        string Name,
        string GitHub,
        string Age,
        string? ImageURI);
}
