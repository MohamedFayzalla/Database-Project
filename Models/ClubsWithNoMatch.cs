using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Keyless]
public partial class ClubsWithNoMatch
{
    [Column("CLUB_NAME_NO_MATCHES")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ClubNameNoMatches { get; set; }
}
