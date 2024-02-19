using System;
using System.Collections.Generic;

namespace MoTimeApi.Models;

public partial class ClaimItem
{
    public int ClaimItemId { get; set; }

    public int? ClaimTypeId { get; set; }

    public string? ClaimItemName { get; set; }

    public virtual ICollection<ClaimCapture> ClaimCaptures { get; set; } = new List<ClaimCapture>();

    public virtual ClaimType? ClaimType { get; set; }

    public virtual ICollection<ProjectAllocation> ProjectAllocations { get; set; } = new List<ProjectAllocation>();

    public virtual ICollection<ProjectClaimItem> ProjectClaimItems { get; set; } = new List<ProjectClaimItem>();
}
