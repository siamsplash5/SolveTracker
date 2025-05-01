using SolveTracker.DBContext;
using SolveTracker.Models.Login;

namespace SolveTracker.Repositories.Login
{
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
}
