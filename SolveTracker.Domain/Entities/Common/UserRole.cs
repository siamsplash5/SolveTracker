namespace SolveTracker.Domain.Entities.Common;

[Flags]
public enum UserRole
{
    None,
    Programmer,
    Teacher,
    Mentor,
    Admin,
    All
}
