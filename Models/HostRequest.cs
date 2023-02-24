
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;


[Table("Host_Request")]
public partial class HostRequest
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("match_id")]
    public int? MatchId { get; set; }

    [Column("status")]
    public int? Status { get; set; }

    [Column("stadium_manager_id")]
    public int StadiumManagerId { get; set; }

    [Column("club_representative_id")]
    public int ClubRepresentativeId { get; set; }

    [ForeignKey("ClubRepresentativeId")]
    [InverseProperty("HostRequests")]
    public virtual ClubRepresentative ClubRepresentative { get; set; } = null!;

    [ForeignKey("StadiumManagerId")]
    [InverseProperty("HostRequests")]
    public virtual StadiumManager StadiumManager { get; set; } = null!;
}
