using SolveTracker.Domain.Entities.Login;

namespace SolveTracker.Application.Services.Login;

public interface ILoginService
{
    Task<LoginResponse> IsLoginInformationValidAsync(LoginRequest loginRequest);
}
