using System;
using System.Collections.Generic;

namespace Doan.Models;

public partial class Slider
{
    public int Sliderid { get; set; }

    public string? ImageUrl { get; set; }

    public string? Title { get; set; }

    public string? Subtitle { get; set; }

    public string? Description { get; set; }

    public string? Link { get; set; }

    public bool? IsActive { get; set; }
}
