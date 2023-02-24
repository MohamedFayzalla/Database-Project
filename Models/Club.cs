using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Table("Club")]
public partial class Club
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("location")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Location { get; set; }

    [InverseProperty("Club")]
    public virtual ICollection<ClubRepresentative> ClubRepresentatives { get; } = new List<ClubRepresentative>();

    [InverseProperty("Guest")]
    public virtual ICollection<Match> MatchGuests { get; } = new List<Match>();

    [InverseProperty("Host")]
    public virtual ICollection<Match> MatchHosts { get; } = new List<Match>();
}
