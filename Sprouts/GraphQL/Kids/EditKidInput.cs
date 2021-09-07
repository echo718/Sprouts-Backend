namespace Sprouts.GraphQL.Kids
{
    public record EditKidInput(
        string KidId,
        string? Name,
        string? Age,
        string? GitHub,
        string? ImageURI);
}
