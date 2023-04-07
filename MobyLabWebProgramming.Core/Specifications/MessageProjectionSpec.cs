
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System.Linq.Expressions;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class MessageProjectionSpec : BaseSpec<MessageProjectionSpec, Message, MessageDTO>
{
    protected override Expression<Func<Message, MessageDTO>> Spec => e => new()
    {
        Id = e.Id,
        CarId = e.CarId,
        Text = e.Text
     };

    public MessageProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public MessageProjectionSpec(Guid id) : base(id)
    {
    }

    public MessageProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Text, searchExpr)); 
                                                                
    }
}