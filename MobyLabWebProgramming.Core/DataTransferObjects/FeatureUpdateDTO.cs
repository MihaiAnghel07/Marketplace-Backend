
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record FeatureUpdateDTO(Guid Id, CarFeatureEnum? FeatureValue = default);
