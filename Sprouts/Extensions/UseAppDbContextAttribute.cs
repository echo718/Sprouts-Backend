using System.Reflection;
using Sprouts.Data;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace Sprouts.Extensions
{
    public class UseAppDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member)
        {
            descriptor.UseDbContext<AppDbContext>();
        }
    }
}
