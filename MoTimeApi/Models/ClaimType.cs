using System;
using System.Collections.Generic;

namespace MoTimeApi.Models;

public partial class ClaimType
{
    public int ClaimTypeId { get; set; }

    public string? ClaimTypeName { get; set; }

    public string? ClaimTypeDescription { get; set; }

    public virtual ICollection<ClaimItem> ClaimItems { get; set; } = new List<ClaimItem>();
}
