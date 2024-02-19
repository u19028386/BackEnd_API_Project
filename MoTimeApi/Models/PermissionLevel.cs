using System;
using System.Collections.Generic;

namespace MoTimeApi.Models;

public partial class PermissionLevel
{
    public int PermissionId { get; set; }

    public int? UserRoleId { get; set; }

    public string? PermissionName { get; set; }

    public string? PermissionDescription { get; set; }

    public string? PermissionScope { get; set; }

    public virtual UserRole? UserRole { get; set; }
}
