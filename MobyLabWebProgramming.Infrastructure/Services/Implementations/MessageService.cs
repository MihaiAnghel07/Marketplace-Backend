
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


public class MessageService : IMessageService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public MessageService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<MessageDTO>> GetMessage(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new MessageProjectionSpec(id), cancellationToken); 

        return result != null ?
            ServiceResponse<MessageDTO>.ForSuccess(result) :
            ServiceResponse<MessageDTO>.FromError(CommonErrors.MessageNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<MessageDTO>>> GetMessages(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new MessageProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<MessageDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> AddMessage(MessageAddDTO message, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {

        await _repository.AddAsync(new Message
        {
            CarId = message.CarId,
            Text = message.Text,
            UserId = requestingUser.Id
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateMessage(MessageUpdateDTO message, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null 
            || (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != message.UserId))
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the Own user can update the message!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new MessageSpec(message.Id), cancellationToken);

        if (entity != null)
        {
            entity.CarId = message.CarId ?? entity.CarId;
            entity.Text = message.Text ?? entity.Text;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteMessage(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {

        if (requestingUser != null && requestingUser.Role == UserRoleEnum.Admin)
        {
            await _repository.DeleteAsync<Message>(id, cancellationToken);
            return ServiceResponse.ForSuccess();
        }

        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            foreach (var message in requestingUser.Messages)
            {
                if (message != null && message.Id.ToString() == id.ToString())
                {
                    await _repository.DeleteAsync<Message>(id, cancellationToken);
                    return ServiceResponse.ForSuccess();
                }
            }
        }

        return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the Own user can delete the message!", ErrorCodes.CannotDelete));
    }

}
