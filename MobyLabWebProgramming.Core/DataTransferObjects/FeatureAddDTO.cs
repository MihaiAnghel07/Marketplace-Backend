
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;


public class FeatureAddDTO
{
    public CarFeatureEnum FeatureValue { get; set; } = default!;
}