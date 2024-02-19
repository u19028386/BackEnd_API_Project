using System;
using System.Collections.Generic;

namespace MoTimeApi.Models;

public partial class ProjectClaimItem
{
    public int ProjectClaimId { get; set; }

    public int? ClaimItemId { get; set; }

    public int? ProjectAllocationId { get; set; }

    public virtual ClaimItem? ClaimItem { get; set; }

    public virtual ProjectAllocation? ProjectAllocation { get; set; }
}
