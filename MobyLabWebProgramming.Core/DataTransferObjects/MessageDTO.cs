
namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class MessageDTO
{
    public Guid Id { get; set; }
    public int CarId { get; set; } = default!;
    public string Text { get; set; } = default!;
}
