using SolveTracker.Domain.Entities.Login;
using SolveTracker.Domain.Repositories;

namespace SolveTracker.Application.Services.Login;

public class LoginService (ILoginRepository loginRepository) : ILoginService
{
    public async Task<LoginResponse> IsLoginInformationValidAsync(LoginRequest loginRequest)
    {
        try
        {
            return await loginRepository.IsLoginInformationValidAsync(loginRequest);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
