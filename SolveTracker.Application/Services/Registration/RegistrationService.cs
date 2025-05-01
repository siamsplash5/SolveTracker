using SolveTracker.Domain.Entities.Registration;
using SolveTracker.Domain.Repositories;

namespace SolveTracker.Application.Services.Registration;

public class RegistrationService(IRegistrationRepository registrationRepository) : IRegistrationService
{
    public async Task CreateAccountAsync(RegistrationRequest request)
    {
        try
        {
            await registrationRepository.CreateAccountAsync(request);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
