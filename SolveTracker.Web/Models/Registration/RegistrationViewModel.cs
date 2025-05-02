using System.ComponentModel.DataAnnotations;

namespace SolveTracker.Web.Models.Registration;

public class RegistrationViewModel
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a role")]
    public int Role { get; set; }
}
