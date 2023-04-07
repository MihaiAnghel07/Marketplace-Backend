using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class CarFeaturesDTO
{
    public Guid CarId { get; set; }
    public Car Car { get; set; } = default!;
    public Guid FeatureId { get; set; }
    public Feature Feature { get; set; } = default!;

}
