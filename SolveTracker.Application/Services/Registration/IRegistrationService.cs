using SolveTracker.Domain.Entities.Registration;

namespace SolveTracker.Application.Services.Registration;

public interface IRegistrationService
{
    Task CreateAccountAsync(RegistrationRequest model);
}
