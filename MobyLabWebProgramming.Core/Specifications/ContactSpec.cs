
using MobyLabWebProgramming.Core.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ContactSpec : BaseSpec<ContactSpec, Contact>
{
    public ContactSpec(Guid id) : base(id)
    {
    }

    /*public ContactSpec(string email)
    {
        Query.Where(e => e.Email == email);
    }*/
}
