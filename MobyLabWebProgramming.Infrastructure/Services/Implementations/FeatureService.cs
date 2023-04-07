
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System.Net;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;


public class FeatureService : IFeatureService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public FeatureService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<FeatureDTO>> GetFeature(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new FeatureProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<FeatureDTO>.ForSuccess(result) :
            ServiceResponse<FeatureDTO>.FromError(CommonErrors.FeatureNotFound);
    }

   public async Task<ServiceResponse<PagedResponse<FeatureDTO>>> GetFeatures(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new FeatureProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<FeatureDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> AddFeature(FeatureAddDTO feature, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null || (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin))
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Admin can add Features!", ErrorCodes.CannotAdd));
        }

        await _repository.AddAsync(new Feature
        {
            FeatureValue = feature.FeatureValue
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateFeature(FeatureUpdateDTO feature, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null || (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin))
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Admin can update the feature!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new FeatureSpec(feature.Id), cancellationToken);

        if (entity != null)
        {
            entity.FeatureValue = feature.FeatureValue ?? entity.FeatureValue;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteFeature(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null || (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin))
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Admin can delete the feature!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Feature>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}