using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Keyless]
public partial class ClubsNeverMatched
{
    [Column("CLUB1")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Club1 { get; set; }

    [Column("CLUB2")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Club2 { get; set; }
}
