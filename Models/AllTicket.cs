using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Keyless]
public partial class AllTicket
{
    [Column("HOST_CLUB")]
    [StringLength(20)]
    [Unicode(false)]
    public string? HostClub { get; set; }

    [Column("GUEST_CLUB")]
    [StringLength(20)]
    [Unicode(false)]
    public string? GuestClub { get; set; }

    [Column("STADIUM_NAME")]
    [StringLength(20)]
    [Unicode(false)]
    public string? StadiumName { get; set; }

    [Column("start_time", TypeName = "datetime")]
    public DateTime? StartTime { get; set; }
}
