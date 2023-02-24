using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Table("Match")]
public partial class Match
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("start_time", TypeName = "datetime")]
    public DateTime? StartTime { get; set; }

    [Column("end_time", TypeName = "datetime")]
    public DateTime? EndTime { get; set; }

    [Column("host_id")]
    public int HostId { get; set; }

    [Column("guest_id")]
    public int GuestId { get; set; }

    [Column("host_stadium_id")]
    public int HostStadiumId { get; set; }

    [ForeignKey("GuestId")]
    [InverseProperty("MatchGuests")]
    public virtual Club Guest { get; set; } = null!;

    [ForeignKey("HostId")]
    [InverseProperty("MatchHosts")]
    public virtual Club Host { get; set; } = null!;

    [ForeignKey("HostStadiumId")]
    [InverseProperty("Matches")]
    public virtual Stadium HostStadium { get; set; } = null!;

    [InverseProperty("Match")]
    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();
}
