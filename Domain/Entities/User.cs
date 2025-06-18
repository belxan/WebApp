using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User : BaseAuditableEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    [EmailAddress]
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string PasswordHash { get; set; } = default!;
    public string PasswordSalt { get; set; } = default!;
}