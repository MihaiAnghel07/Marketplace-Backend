
using MobyLabWebProgramming.Core.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class MessageSpec : BaseSpec<MessageSpec, Message>
{
    public MessageSpec(Guid id) : base(id)
    {
    }

    /*public MessageSpec(string email)
    {
        Query.Where(e => e.Email == email);
    }*/
}
