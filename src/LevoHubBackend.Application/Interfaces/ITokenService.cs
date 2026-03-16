using LevoHubBackend.Domain.Entities;

namespace LevoHubBackend.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user, IEnumerable<string> roles, IEnumerable<string> permissions);
}
