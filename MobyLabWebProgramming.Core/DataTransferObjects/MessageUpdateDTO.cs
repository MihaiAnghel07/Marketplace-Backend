
namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record MessageUpdateDTO(Guid Id, int? CarId = default, string? Text = default, Guid? UserId = default);
