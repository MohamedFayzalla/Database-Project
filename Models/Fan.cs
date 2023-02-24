using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Table("Fan")]
public partial class Fan
{
    [Key]
    [Column("national_id")]
    [StringLength(20)]
    [Unicode(false)]
    public string NationalId { get; set; } = null!;

    [Column("phone_num")]
    public int? PhoneNum { get; set; }

    [Column("name")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("address")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Address { get; set; }

    [Column("status")]
    public bool? Status { get; set; }

    [Column("birthdate", TypeName = "datetime")]
    public DateTime? Birthdate { get; set; }

    [Column("super_id")]
    [StringLength(20)]
    [Unicode(false)]
    public string SuperId { get; set; } = null!;

    [ForeignKey("SuperId")]
    [InverseProperty("Fans")]
    public virtual SystemUser Super { get; set; } = null!;

    [ForeignKey("FanId")]
    [InverseProperty("Fans")]
    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();
}
