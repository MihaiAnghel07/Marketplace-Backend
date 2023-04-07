

using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record CarFeaturesUpdateDTO(Guid Id, Guid? CarId = default, Car? Car = default, Feature? Feature = default, Guid? FeatureId = default);

