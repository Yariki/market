using System.Reflection;
using Market.Shared.Application.Mappings;

namespace Market.Identity.Api.Application.Common.Mapping;

public class AuthMapping : MappingProfile
{
    public AuthMapping()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }
}