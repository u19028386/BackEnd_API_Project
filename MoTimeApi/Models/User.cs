using System;
using System.Collections.Generic;

namespace MoTimeApi.Models;

public partial class User
{
    public int UserId { get; set; }

    public int? TitleId { get; set; }

    public string? IdNumber { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PrefferedName { get; set; }

    public string? EmailAddress { get; set; }

    public string? CellphoneNumber { get; set; }

    public DateTime? DateOfHire { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? DateOfCreation { get; set; }

    public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Password> Passwords { get; set; } = new List<Password>();

    public virtual ICollection<SystemAdministrator> SystemAdministrators { get; set; } = new List<SystemAdministrator>();

    public virtual Title? Title { get; set; }

    public virtual ICollection<UserUserRole> UserUserRoles { get; set; } = new List<UserUserRole>();
}
