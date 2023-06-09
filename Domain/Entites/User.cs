using Domain.Common;
using Domain.Entites.IdentityEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Table("user")]

public class User : BaseAuditableEntity
{
    public ICollection<Role> Roles { get; set; }

    [Column("username")]
    public string Name { get; set; }
    public string Email { get; set; }

    [Column("password")]
    public string Password { get; set; }

}
