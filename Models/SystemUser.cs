using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

[Table("SystemUser")]
public partial class SystemUser
{
    [Key]
    [Column("username")]
    [StringLength(20)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Column("password")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Password { get; set; }

    [InverseProperty("Super")]
    public virtual ICollection<ClubRepresentative> ClubRepresentatives { get; } = new List<ClubRepresentative>();

    [InverseProperty("Super")]
    public virtual ICollection<Fan> Fans { get; } = new List<Fan>();

    [InverseProperty("Super")]
    public virtual ICollection<SportsAssociationManager> SportsAssociationManagers { get; } = new List<SportsAssociationManager>();

    [InverseProperty("Super")]
    public virtual ICollection<StadiumManager> StadiumManagers { get; } = new List<StadiumManager>();

    [InverseProperty("Super")]
    public virtual ICollection<SystemAdmin> SystemAdmins { get; } = new List<SystemAdmin>();
}
