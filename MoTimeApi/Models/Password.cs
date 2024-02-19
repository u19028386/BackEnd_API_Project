using System;
using System.Collections.Generic;

namespace MoTimeApi.Models;

public partial class Password
{
    public int PasswordId { get; set; }

    public int? UserId { get; set; }

    public string? Password1 { get; set; }

    public virtual User? User { get; set; }
}
