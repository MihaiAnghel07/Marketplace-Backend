
using MobyLabWebProgramming.Core.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class FeatureSpec : BaseSpec<FeatureSpec, Feature>
{
    public FeatureSpec(Guid id) : base(id)
    {
    }

    /*public FeatureSpec(string email)
    {
        Query.Where(e => e.Email == email);
    }*/
}
