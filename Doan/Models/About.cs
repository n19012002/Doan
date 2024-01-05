using System;
using System.Collections.Generic;

namespace Doan.Models;

public partial class About
{
    public int AboutId { get; set; }

    public string Title { get; set; } = null!;

    public string? Contents { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public bool? IsActive { get; set; }

    public string? Images { get; set; }

    public string? Links { get; set; }
}
