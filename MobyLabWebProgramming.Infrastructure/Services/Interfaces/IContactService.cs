
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IContactService
{
    public Task<ServiceResponse<ContactDTO>> GetContact(Guid id, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<ContactDTO>>> GetContacts(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> AddContact(ContactAddDTO contact, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse> UpdateContact(ContactUpdateDTO contact, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse> DeleteContact(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
