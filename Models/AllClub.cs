using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Keyless]
public partial class AllClub
{
    [Column("CLUB_NAME")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ClubName { get; set; }

    [Column("CLUB_LOCATION")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ClubLocation { get; set; }
}
