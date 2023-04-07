using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ICarFeaturesService
{
    public Task<ServiceResponse<CarFeaturesDTO>> GetCarFeatures(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<CarFeaturesDTO>>> GetCarsFeatures(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddCarFeatures(CarFeaturesAddDTO carFeature, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateCarFeatures(CarFeaturesUpdateDTO carFeature, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteCarFeatures(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
