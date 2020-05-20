using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Interfaces.Services
{
    public interface IRolesClient_ : IRoleStore<Role>
    {
        
    }
}