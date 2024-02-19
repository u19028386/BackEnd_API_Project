using System;
using System.Collections.Generic;

namespace MoTimeApi.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public string? Question1 { get; set; }

    public string? Answer { get; set; }

    public bool? IsAnswered { get; set; }
}
