
namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ContactDTO
{
    public Guid Id { get; set; }
    public int Mobile { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string City { get; set; } = default!;
}
