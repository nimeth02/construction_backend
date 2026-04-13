using LevoHubBackend.Domain.Entities;

using System.Collections.Generic;
using System.Security.Claims;

namespace LevoHubBackend.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user, IEnumerable<string> roles, IEnumerable<Claim> claims);
}
