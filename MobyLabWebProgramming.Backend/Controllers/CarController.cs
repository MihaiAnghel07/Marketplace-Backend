using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Infrastructure.Authorization;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CarController : AuthorizedController
{
    protected readonly ICarService CarService;

    public CarController(ICarService carService, IUserService userService) : base(userService)
    {
        CarService = carService;
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<CarDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await CarService.GetCar(id)) :
            this.ErrorMessageResult<CarDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpGet] 
    public async Task<ActionResult<RequestResponse<PagedResponse<CarDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)                                                                                                                               
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await CarService.GetCars(pagination)) :
            this.ErrorMessageResult<PagedResponse<CarDTO>>(currentUser.Error);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] CarAddDTO car)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await CarService.AddCar(car, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] CarUpdateDTO car)
    {
        var currentUser = await GetCurrentUser();
        
        return currentUser.Result != null ?
            this.FromServiceResponse(await CarService.UpdateCar(car, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await CarService.DeleteCar(id, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }
}

