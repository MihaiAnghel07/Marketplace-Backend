
using MobyLabWebProgramming.Core.Constants;
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

public class CarService : ICarService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    
    public CarService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository; 
    }

    public async Task<ServiceResponse<CarDTO>> GetCar(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new CarProjectionSpec(id), cancellationToken); 

        return result != null ?
            ServiceResponse<CarDTO>.ForSuccess(result) :
            ServiceResponse<CarDTO>.FromError(CommonErrors.CarNotFound); 
    }
    
    public async Task<ServiceResponse<PagedResponse<CarDTO>>> GetCars(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new CarProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<CarDTO>>.ForSuccess(result);
    }
    
    public async Task<ServiceResponse> AddCar(CarAddDTO car, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null || (requestingUser != null && requestingUser.Role == UserRoleEnum.Client)) 
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Admin and Users can add cars!", ErrorCodes.CannotAdd));
        }

        await _repository.AddAsync(new Car
        {
            Brand = car.Brand,
            Model = car.Model,
            Year = car.Year,
            Km = car.Km,
            Price = car.Price,
            UserId = requestingUser.Id
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
    
    public async Task<ServiceResponse> UpdateCar(CarUpdateDTO car, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null 
            || (requestingUser != null && requestingUser.Role == UserRoleEnum.Client) 
            || (requestingUser != null && requestingUser.Role == UserRoleEnum.User && requestingUser.Id.ToString() != car.UserId.ToString()))
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Admin or the Own user can update the car specifications!", ErrorCodes.CannotUpdate));
        }
        
        var entity = await _repository.GetAsync(new CarSpec(car.Id), cancellationToken);

        if (entity != null)
        {
            entity.Brand = car.Brand ?? entity.Brand;
            entity.Model = car.Model ?? entity.Model;
            entity.Year = car.Year ?? entity.Year;
            entity.Km = car.Km ?? entity.Km;
            entity.Price = car.Price ?? entity.Price;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteCar(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {   
        if (requestingUser == null || (requestingUser != null && requestingUser.Role == UserRoleEnum.Client))
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Admin or the Own user can delete the car!", ErrorCodes.CannotDelete));
        }

        if (requestingUser != null && requestingUser.Role == UserRoleEnum.Admin)
        {
            await _repository.DeleteAsync<Car>(id, cancellationToken);
            return ServiceResponse.ForSuccess();
        }

        if (requestingUser != null && requestingUser.Role == UserRoleEnum.User)
        {
            foreach (var car in requestingUser.Cars)
            {
                if (car != null && car.Id.ToString() == id.ToString()) 
                {
                    await _repository.DeleteAsync<Car>(id, cancellationToken);
                    return ServiceResponse.ForSuccess();
                }
            }
        }


        return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the Admin or the Own user can delete the car!", ErrorCodes.CannotDelete));
    }

}
