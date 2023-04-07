
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class CarDTO
{
    public Guid Id { get; set; }
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public int Year { get; set; } = default!;
    public int Km { get; set; } = default!;
    public float Price { get; set; } = default!;

    public ICollection<CarFeatures> CarFeatures { get; set; } = default!;


}
