using System;
using System.Collections.Generic;

namespace CampusRecruitment.Entities.Entities;

public partial class Conversation
{
    public int ConversationId { get; set; }

    public int OrganizationFromId { get; set; }

    public int OrganizationToId { get; set; }

    public string? Message { get; set; }

    public DateTime ConversationDate { get; set; }

    public virtual Organization OrganizationFrom { get; set; } = null!;

    public virtual Organization OrganizationTo { get; set; } = null!;
}
