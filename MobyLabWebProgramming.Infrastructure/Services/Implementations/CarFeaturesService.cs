
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


public class CarFeaturesService : ICarFeaturesService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public CarFeaturesService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<CarFeaturesDTO>> GetCarFeatures(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new CarFeaturesProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<CarFeaturesDTO>.ForSuccess(result) :
            ServiceResponse<CarFeaturesDTO>.FromError(CommonErrors.CarFeaturesNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<CarFeaturesDTO>>> GetCarsFeatures(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new CarFeaturesProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<CarFeaturesDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> AddCarFeatures(CarFeaturesAddDTO carFeatures, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null || (requestingUser != null && requestingUser.Role == UserRoleEnum.Client))
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Admin and Users can add CarFeatures!", ErrorCodes.CannotAdd));
        }

        await _repository.AddAsync(new CarFeatures
        {
            CarId = carFeatures.CarId,
            Car = carFeatures.Car,
            FeatureId = carFeatures.FeatureId,
            Feature = carFeatures.Feature,
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateCarFeatures(CarFeaturesUpdateDTO carFeatures, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null
            || (requestingUser != null && requestingUser.Role == UserRoleEnum.Client)
            || (requestingUser != null && requestingUser.Role == UserRoleEnum.User && !requestingUser.Cars.Contains(carFeatures.Car)))
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Admin or the Own user can update the car features", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new CarFeaturesSpec(carFeatures.Id), cancellationToken);

        if (entity != null)
        {
            entity.Car = carFeatures.Car ?? entity.Car;
            entity.CarId = carFeatures.CarId ?? entity.CarId;
            entity.FeatureId = carFeatures.FeatureId ?? entity.FeatureId;
            entity.Feature = carFeatures.Feature ?? entity.Feature;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteCarFeatures(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        
        if (requestingUser != null && requestingUser.Role == UserRoleEnum.Admin)
        {
            await _repository.DeleteAsync<CarFeatures>(id, cancellationToken);
            return ServiceResponse.ForSuccess();
        }

        if (requestingUser != null && requestingUser.Role == UserRoleEnum.User)
        {
            foreach (var car in requestingUser.Cars)
            {
                if (car != null)
                {
                    foreach (var carFeature in car.CarFeatures)
                    {
                        if (carFeature != null) 
                        {
                            if (carFeature.Id == id)
                            {
                                await _repository.DeleteAsync<CarFeatures>(id, cancellationToken);
                                return ServiceResponse.ForSuccess();
                            }
                        }
                    }
                }
            }
        }


        return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Admin or the Own user can delete the car features!", ErrorCodes.CannotDelete));
    }

}