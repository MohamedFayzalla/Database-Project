using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Table("Sports_Association_Manager")]
public partial class SportsAssociationManager
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

    [ForeignKey("SuperId")]
    [InverseProperty("SportsAssociationManagers")]
    public virtual SystemUser Super { get; set; } = null!;
}
