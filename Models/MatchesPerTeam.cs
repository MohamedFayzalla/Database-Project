using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Keyless]
public partial class MatchesPerTeam
{
    [Column("CLUB NAME ")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ClubName { get; set; }

    [Column("NUMBER OF MATCHES PLAYED")]
    public int? NumberOfMatchesPlayed { get; set; }
}
