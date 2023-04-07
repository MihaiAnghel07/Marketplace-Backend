
namespace MobyLabWebProgramming.Core.Entities;

public class CarFeatures : BaseEntity
{
    public Guid CarId { get; set; }
    public Car Car { get; set; } = default!;

    public Guid FeatureId { get; set; }
    public Feature Feature { get; set; } = default!;
}