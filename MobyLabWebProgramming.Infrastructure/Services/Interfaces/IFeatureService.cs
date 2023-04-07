
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IFeatureService
{
    public Task<ServiceResponse<FeatureDTO>> GetFeature(Guid id, CancellationToken cancellationToken = default);
   
    public Task<ServiceResponse<PagedResponse<FeatureDTO>>> GetFeatures(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
   
    public Task<ServiceResponse> AddFeature(FeatureAddDTO feature, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
   
    public Task<ServiceResponse> UpdateFeature(FeatureUpdateDTO feature, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse> DeleteFeature(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
