using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Keyless]
public partial class AllFan
{
    [Column("FAN_USERNAME")]
    [StringLength(20)]
    [Unicode(false)]
    public string FanUsername { get; set; } = null!;

    [Column("FAN_PASSWORD")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FanPassword { get; set; }

    [Column("name")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("national_id")]
    [StringLength(20)]
    [Unicode(false)]
    public string NationalId { get; set; } = null!;

    [Column("birthdate", TypeName = "datetime")]
    public DateTime? Birthdate { get; set; }

    [Column("status")]
    public bool? Status { get; set; }
}
