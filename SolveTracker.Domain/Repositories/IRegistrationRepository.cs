using SolveTracker.Domain.Entities.Registration;

namespace SolveTracker.Domain.Repositories;

public interface IRegistrationRepository
{
    Task CreateAccountAsync(RegistrationRequest model);
    Task UpdateAccount(RegistrationRequest model);
    Task DeleteAccount(RegistrationRequest model);
}
