
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;


public interface IMessageService
{
    public Task<ServiceResponse<MessageDTO>> GetMessage(Guid id, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<MessageDTO>>> GetMessages(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> AddMessage(MessageAddDTO message, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse> UpdateMessage(MessageUpdateDTO message, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteMessage(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}