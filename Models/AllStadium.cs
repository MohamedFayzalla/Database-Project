using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Keyless]
public partial class AllStadium
{
    [Column("STADIUM_NAME")]
    [StringLength(20)]
    [Unicode(false)]
    public string? StadiumName { get; set; }

    [Column("STADIUM_LOCATION")]
    [StringLength(20)]
    [Unicode(false)]
    public string? StadiumLocation { get; set; }

    [Column("STADIUM_CAPACITY")]
    public int? StadiumCapacity { get; set; }

    [Column("STADIUM_STATUS")]
    public bool? StadiumStatus { get; set; }
}
