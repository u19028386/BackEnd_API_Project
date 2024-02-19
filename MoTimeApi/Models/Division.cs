using System;
using System.Collections.Generic;

namespace MoTimeApi.Models;

public partial class Division
{
    public int DivisionId { get; set; }

    public string? DivisionName { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
