using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Table("Club_Representative")]
public partial class ClubRepresentative
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

    [Column("club_id")]
    public int? ClubId { get; set; }

    [ForeignKey("ClubId")]
    [InverseProperty("ClubRepresentatives")]
    public virtual Club? Club { get; set; }

    [InverseProperty("ClubRepresentative")]
    public virtual ICollection<HostRequest> HostRequests { get; } = new List<HostRequest>();

    [ForeignKey("SuperId")]
    [InverseProperty("ClubRepresentatives")]
    public virtual SystemUser Super { get; set; } = null!;
}
