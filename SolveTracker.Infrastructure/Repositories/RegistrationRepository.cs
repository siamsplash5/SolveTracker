using SolveTracker.Domain.Entities.Registration;
using SolveTracker.Domain.Repositories;
using SolveTracker.Infrastructure.DBContext;

namespace SolveTracker.Infrastructure.Repositories;

public class RegistrationRepository(IDapperDBContext dapperDBContext) : IRegistrationRepository
{
    private readonly string createNewAccountSP = "USERS_CreateNewAccount";

    public async Task CreateAccountAsync(RegistrationRequest model)
    {
        try
        {
            await dapperDBContext.ExecuteAsync(model, createNewAccountSP);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task UpdateAccount(RegistrationRequest model)
    {
        try
        {
            await Task.Delay(1000);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task DeleteAccount(RegistrationRequest model)
    {
        try
        {
            await Task.Delay(1000);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
