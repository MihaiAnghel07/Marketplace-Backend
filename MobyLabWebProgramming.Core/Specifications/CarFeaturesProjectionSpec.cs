
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;
using System.Linq.Expressions;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class CarFeaturesProjectionSpec : BaseSpec<CarFeaturesProjectionSpec, CarFeatures, CarFeaturesDTO>
{
    protected override Expression<Func<CarFeatures, CarFeaturesDTO>> Spec => e => new()
    {
        CarId = e.CarId,
        Car = e.Car,
        FeatureId = e.FeatureId,
        Feature= e.Feature
    };

    public CarFeaturesProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public CarFeaturesProjectionSpec(Guid id) : base(id)
    {
    }

    public CarFeaturesProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Id.ToString(), searchExpr)); 
    }
}

