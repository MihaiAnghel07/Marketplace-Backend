
namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record ContactUpdateDTO(Guid Id, int? Mobile = default, string? Address = default, string? City = default, Guid? UserId = default);
