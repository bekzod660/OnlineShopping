using System.ComponentModel.DataAnnotations;

namespace Application.Models.Token;

public class UserRefreshToken
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string RefreshToken { get; set; }
    public DateTime ExpireDate { get; set; }
}

