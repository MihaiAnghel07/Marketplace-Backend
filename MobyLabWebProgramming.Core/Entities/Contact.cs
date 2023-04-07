

namespace MobyLabWebProgramming.Core.Entities;

public class Contact : BaseEntity
{ 
    public int Mobile { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string City { get; set; } = default!;

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

}
