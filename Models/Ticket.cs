using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Table("Ticket")]
public partial class Ticket
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("status")]
    public bool? Status { get; set; }

    [Column("match_id")]
    public int MatchId { get; set; }

    [ForeignKey("MatchId")]
    [InverseProperty("Tickets")]
    public virtual Match Match { get; set; } = null!;

    [ForeignKey("TicketId")]
    [InverseProperty("Tickets")]
    public virtual ICollection<Fan> Fans { get; } = new List<Fan>();
}
