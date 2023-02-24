using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Table("Stadium")]
public partial class Stadium
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("capacity")]
    public int? Capacity { get; set; }

    [Column("location")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Location { get; set; }

    [Column("status")]
    public bool? Status { get; set; }

    [InverseProperty("HostStadium")]
    public virtual ICollection<Match> Matches { get; } = new List<Match>();

    [InverseProperty("Stadium")]
    public virtual ICollection<StadiumManager> StadiumManagers { get; } = new List<StadiumManager>();
}
