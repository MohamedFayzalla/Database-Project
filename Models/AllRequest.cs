using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Keyless]
public partial class AllRequest
{
    [Column("CLUB REPRESENTATIVE USER NAME")]
    [StringLength(20)]
    [Unicode(false)]
    public string ClubRepresentativeUserName { get; set; } = null!;

    [Column("STADIUM MANAGER USER NAME")]
    [StringLength(20)]
    [Unicode(false)]
    public string StadiumManagerUserName { get; set; } = null!;

    [Column("status")]
    public int? Status { get; set; }
}
