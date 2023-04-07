
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

public class ContactService : IContactService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public ContactService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<ContactDTO>> GetContact(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new ContactProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<ContactDTO>.ForSuccess(result) :
            ServiceResponse<ContactDTO>.FromError(CommonErrors.ContactNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<ContactDTO>>> GetContacts(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new ContactProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<ContactDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> AddContact(ContactAddDTO contact, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null || (requestingUser != null && requestingUser.Role == UserRoleEnum.User)) 
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin and clients can add the contact section!", ErrorCodes.CannotAdd));
        }

        if (requestingUser.Contact != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "The current user already has a contact section", ErrorCodes.CannotAdd));
        }

        await _repository.AddAsync(new Contact
        {
            Mobile = contact.Mobile,
            Address = contact.Address,
            City = contact.City,
            UserId = requestingUser.Id
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateContact(ContactUpdateDTO contact, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null 
            || (requestingUser != null && (requestingUser.Role == UserRoleEnum.Client || requestingUser.Role == UserRoleEnum.User) && requestingUser.Id != contact.UserId))
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the Own user can update the contact section!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new ContactSpec(contact.Id), cancellationToken);

        if (entity != null) 
        {
            entity.Mobile = contact.Mobile ?? entity.Mobile;
            entity.Address = contact.Address ?? entity.Address;
            entity.City = contact.City ?? entity.City;

            await _repository.UpdateAsync(entity, cancellationToken); 
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteContact(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null 
            || (requestingUser != null && (requestingUser.Role == UserRoleEnum.Client || requestingUser.Role == UserRoleEnum.User) && requestingUser.Contact.Id != id))
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own user can delete the contact section!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Contact>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

}
