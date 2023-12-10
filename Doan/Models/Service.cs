using System;
using System.Collections.Generic;

namespace Doan.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public decimal? Price { get; set; }

    public int? Duration { get; set; }

    public string? Location { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsActive { get; set; }
}
