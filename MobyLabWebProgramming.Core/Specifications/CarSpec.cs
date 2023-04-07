
using MobyLabWebProgramming.Core.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class CarSpec : BaseSpec<CarSpec, Car>
{
    public CarSpec(Guid id) : base(id)
    {
    }

    /*public CarSpec(string email)
    {
        Query.Where(e => e.Email == email);
    }*/
}
