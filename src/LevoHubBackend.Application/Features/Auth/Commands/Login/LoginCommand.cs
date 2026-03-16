using LevoHubBackend.Application.DTOs.Auth;
using MediatR;

namespace LevoHubBackend.Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<LoginResponse>
{
    public LoginRequest LoginRequest { get; set; }

    public LoginCommand(LoginRequest loginRequest)
    {
        LoginRequest = loginRequest;
    }
}
