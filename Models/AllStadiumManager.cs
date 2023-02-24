using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Keyless]
public partial class AllStadiumManager
{
    [Column("MANAGER_USERNAME")]
    [StringLength(20)]
    [Unicode(false)]
    public string ManagerUsername { get; set; } = null!;

    [Column("PASSWORD")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Password { get; set; }

    [Column("MANAGER_NAME")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ManagerName { get; set; }

    [Column("STADIUM_NAME")]
    [StringLength(20)]
    [Unicode(false)]
    public string? StadiumName { get; set; }
}
