using System;
using System.Collections.Generic;

namespace MoTimeApi.Models;

public partial class ClaimCapture
{
    public int ClaimId { get; set; }

    public int? ClaimItemId { get; set; }

    public int? TimecardId { get; set; }

    public decimal? Amount { get; set; }

    public string? UploadProof { get; set; }

    public virtual ClaimItem? ClaimItem { get; set; }

    public virtual Timecard? Timecard { get; set; }
}
