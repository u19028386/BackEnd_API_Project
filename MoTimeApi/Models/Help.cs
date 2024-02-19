using System;
using System.Collections.Generic;

namespace MoTimeApi.Models;

public partial class Help
{
    public int HelpId { get; set; }

    public int? HelpTypeId { get; set; }

    public int? AdminId { get; set; }

    public string? HelpName { get; set; }

    public string? HelpDescription { get; set; }

    public string? Material { get; set; }

    public virtual SystemAdministrator? Admin { get; set; }

    public virtual HelpType? HelpType { get; set; }
}
