using SolveTracker.Domain.Entities.Common;

namespace SolveTracker.Domain.Entities.Registration;

public class RegistrationRequest
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
}
