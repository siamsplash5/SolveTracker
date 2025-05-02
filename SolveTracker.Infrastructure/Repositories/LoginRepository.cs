using SolveTracker.Domain.Entities.Login;
using SolveTracker.Domain.Repositories;
using SolveTracker.Infrastructure.DBContext;

namespace SolveTracker.Infrastructure.Repositories;

public class LoginRepository (IDapperDBContext dapperDBContext) : ILoginRepository
{
    private readonly string CheckLoginInformationValidSP = "USERS_CheckLoginInformationValid";

    public async Task<LoginResponse> IsLoginInformationValidAsync(LoginRequest loginRequest)
    {
        try
        {
            return await dapperDBContext.GetInfoAsync<LoginRequest, LoginResponse>(loginRequest, CheckLoginInformationValidSP);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
