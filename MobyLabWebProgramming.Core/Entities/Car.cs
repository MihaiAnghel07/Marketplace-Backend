

namespace MobyLabWebProgramming.Core.Entities;

public class Car : BaseEntity
{
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public int Year { get; set; } = default!;
    public int Km { get; set; } = default!;
    public float Price { get; set; } = default!;

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public ICollection<CarFeatures> CarFeatures { get; set; } = default!;
}
