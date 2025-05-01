using SolveTracker.Domain.Entities.Common;

namespace SolveTracker.Domain.Entities.Login;

public class LoginResponse
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public UserRole Role { get; set; }
}
