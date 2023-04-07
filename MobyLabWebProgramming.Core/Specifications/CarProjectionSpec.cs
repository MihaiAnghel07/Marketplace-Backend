
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System.Linq.Expressions;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class CarProjectionSpec : BaseSpec<CarProjectionSpec, Car, CarDTO>
{
    protected override Expression<Func<Car, CarDTO>> Spec => e => new()
    {  
        Id = e.Id,
        Brand = e.Brand,
        Model = e.Model,
        Year = e.Year,
        Km = e.Km,
        Price = e.Price,
        CarFeatures = e.CarFeatures
    };

    public CarProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public CarProjectionSpec(Guid id) : base(id)
    {
    }

    public CarProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Brand, searchExpr));
    }
}
