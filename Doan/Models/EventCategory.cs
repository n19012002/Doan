using System;
using System.Collections.Generic;

namespace Doan.Models;

public partial class EventCategory
{
    public int EventCategoryId { get; set; }

    public string? Title { get; set; }

    public string? Alias { get; set; }

    public string? Description { get; set; }

    public int? Position { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<TbEvent> TbEvents { get; set; } = new List<TbEvent>();
}
