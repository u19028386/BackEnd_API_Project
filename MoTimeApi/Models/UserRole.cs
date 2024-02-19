using System;
using System.Collections.Generic;

namespace MoTimeApi.Models;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public int? AdminId { get; set; }

    public string? UserRoleName { get; set; }

    public string? UserRoleDescription { get; set; }

    public virtual SystemAdministrator? Admin { get; set; }

    public virtual ICollection<PermissionLevel> PermissionLevels { get; set; } = new List<PermissionLevel>();

    public virtual ICollection<UserUserRole> UserUserRoles { get; set; } = new List<UserUserRole>();
}
