
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class FeatureDTO
{
    public Guid Id { get; set; }
    public CarFeatureEnum FeatureValue { get; set; } = default!;

}
