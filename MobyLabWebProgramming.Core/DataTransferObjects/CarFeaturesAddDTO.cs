
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.DataTransferObjects;


public class CarFeaturesAddDTO
{
    public Guid CarId { get; set; }
    public Car Car { get; set; } = default!;
    public Guid FeatureId { get; set; }
    public Feature Feature { get; set; } = default!;

}

