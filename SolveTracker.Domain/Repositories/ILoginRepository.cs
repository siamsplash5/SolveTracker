using SolveTracker.Domain.Entities.Login;

namespace SolveTracker.Domain.Repositories;

public interface ILoginRepository
{
    Task<LoginResponse> IsLoginInformationValidAsync(LoginRequest loginRequest);
}
