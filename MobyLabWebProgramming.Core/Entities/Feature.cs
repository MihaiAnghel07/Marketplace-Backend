using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;

public class Feature : BaseEntity
{
    public CarFeatureEnum FeatureValue { get; set; } = default!;

    public ICollection<CarFeatures> CarFeatures { get; set; } = default!;

}