using System.Collections.Generic;
using System.Security.Claims;

namespace WebStore.Domain.Dtos.Identity
{
    public abstract class ClaimDto : UserDto
    {
        public IEnumerable<Claim> Claims { get; set; }
    }

    public class AddClaimDTO : ClaimDto { }

    public class RemoveClaimDTO : ClaimDto { }

    public class ReplaceClaimDTO : UserDto
    {
        public Claim Claim { get; set; }

        public Claim NewClaim { get; set; }
    }
}