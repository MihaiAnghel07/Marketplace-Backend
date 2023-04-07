
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System.Linq.Expressions;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ContactProjectionSpec : BaseSpec<ContactProjectionSpec, Contact, ContactDTO>
{
    protected override Expression<Func<Contact, ContactDTO>> Spec => e => new()
    {
        Id = e.Id,
        Mobile = e.Mobile,
        Address = e.Address,
        City = e.City
    };

    public ContactProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public ContactProjectionSpec(Guid id) : base(id)
    {
    }

    public ContactProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.City, searchExpr));
    }
}
