using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User : BaseAuditableEntity
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required] 
    public string Salt { get; set; }
}