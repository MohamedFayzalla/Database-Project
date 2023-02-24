using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Table("Stadium_Manager")]
public partial class StadiumManager
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("super_id")]
    [StringLength(20)]
    [Unicode(false)]
    public string SuperId { get; set; } = null!;

    [Column("stadium_id")]
    public int? StadiumId { get; set; }

    [InverseProperty("StadiumManager")]
    public virtual ICollection<HostRequest> HostRequests { get; } = new List<HostRequest>();

    [ForeignKey("StadiumId")]
    [InverseProperty("StadiumManagers")]
    public virtual Stadium? Stadium { get; set; }

    [ForeignKey("SuperId")]
    [InverseProperty("StadiumManagers")]
    public virtual SystemUser Super { get; set; } = null!;
}
