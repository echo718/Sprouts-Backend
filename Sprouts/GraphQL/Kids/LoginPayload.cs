using Sprouts.Models;

namespace Sprouts.GraphQL.Kids
{
    public record LoginPayload(
        Kid kid,
        string jwt);
}
