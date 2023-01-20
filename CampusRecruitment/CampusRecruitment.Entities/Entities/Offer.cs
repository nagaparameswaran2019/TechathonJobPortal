using System;
using System.Collections.Generic;

namespace CampusRecruitment.Entities.Entities;

public partial class Offer
{
    public int OfferId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime JoiningDate { get; set; }

    public string FilePath { get; set; } = null!;

    public int InterviewId { get; set; }

    public virtual Interview Interview { get; set; } = null!;
}
