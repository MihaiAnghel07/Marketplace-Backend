

using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class CarFeaturesSpec : BaseSpec<CarFeaturesSpec, CarFeatures>
{
    public CarFeaturesSpec(Guid id) : base(id)
    {
    }

    /*public CarSpec(string email)
    {
        Query.Where(e => e.Email == email);
    }*/
}
