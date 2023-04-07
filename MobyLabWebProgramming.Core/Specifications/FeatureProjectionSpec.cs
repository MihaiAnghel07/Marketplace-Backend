
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;
using System.Linq.Expressions;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class FeatureProjectionSpec : BaseSpec<FeatureProjectionSpec, Feature, FeatureDTO>
{
    protected override Expression<Func<Feature, FeatureDTO>> Spec => e => new()
    {
        Id = e.Id,
        FeatureValue = e.FeatureValue
    };

    public FeatureProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public FeatureProjectionSpec(Guid id) : base(id)
    {
    }

    public FeatureProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.FeatureValue, searchExpr));
    }
}
