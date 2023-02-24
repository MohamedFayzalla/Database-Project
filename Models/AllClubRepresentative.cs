using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Keyless]
public partial class AllClubRepresentative
{
    [Column("CLUB REPRESENTATIVE USERNAME")]
    [StringLength(20)]
    [Unicode(false)]
    public string ClubRepresentativeUsername { get; set; } = null!;

    [Column("PASSWORD")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Password { get; set; }

    [Column("CLUB REPRESENTATIVE NAME")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ClubRepresentativeName { get; set; }

    [Column("CLUB NAME")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ClubName { get; set; }
}
