using System;
using System.Collections.Generic;

namespace Doan.Models;

public partial class Profile
{
    public int ProfileId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? Gender { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? ModifiedBy { get; set; }

    public int AccountId { get; set; }

    public virtual TbAccount Account { get; set; } = null!;
}
