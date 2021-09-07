using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using Sprouts.Data;
using Sprouts.Models;
using Sprouts.Extensions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Sprouts.GraphQL.Studies
{
    [ExtendObjectType(name: "Query")]
    public class StudyQueries
    {
        [UseAppDbContext]
        [UsePaging]
        public IQueryable<Study> GetStudies(int id,[ScopedService] AppDbContext context)
        {
            // return context.Studies.OrderBy(c => c.Created);
            return context.Studies.Where(c => c.KidId == id).OrderBy(c => c.Created);
        }

        [UseAppDbContext]
        public Study GetStudy(int id, [ScopedService] AppDbContext context)
        {
            return context.Studies.Find(id);
           
        }
    }
}
