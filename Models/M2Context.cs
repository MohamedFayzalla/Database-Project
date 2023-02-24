using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace M3_V1.Models;

public partial class M2Context : DbContext
{
    public M2Context()
    {
    }

    public M2Context(DbContextOptions<M2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Stadium> Stadiums { get; set; }
    public virtual DbSet<AllAssocManager> AllAssocManagers { get; set; }

    public virtual DbSet<AllClub> AllClubs { get; set; }

    public virtual DbSet<AllClubRepresentative> AllClubRepresentatives { get; set; }

    public virtual DbSet<AllFan> AllFans { get; set; }

    public virtual DbSet<AllMatch> AllMatches { get; set; }

    public virtual DbSet<AllRequest> AllRequests { get; set; }

    public virtual DbSet<AllStadium> AllStadiums { get; set; }

    public virtual DbSet<AllStadiumManager> AllStadiumManagers { get; set; }

    public virtual DbSet<AllTicket> AllTickets { get; set; }

    public virtual DbSet<Club> Clubs { get; set; }

    public virtual DbSet<ClubRepresentative> ClubRepresentatives { get; set; }

    public virtual DbSet<ClubsNeverMatched> ClubsNeverMatcheds { get; set; }

    public virtual DbSet<ClubsWithNoMatch> ClubsWithNoMatches { get; set; }

    public virtual DbSet<Fan> Fans { get; set; }

    public virtual DbSet<HostRequest> HostRequests { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<MatchesPerTeam> MatchesPerTeams { get; set; }

    public virtual DbSet<SportsAssociationManager> SportsAssociationManagers { get; set; }

    public virtual DbSet<Stadium> Stadia { get; set; }

    public virtual DbSet<StadiumManager> StadiumManagers { get; set; }

    public virtual DbSet<SystemAdmin> SystemAdmins { get; set; }

    public virtual DbSet<SystemUser> SystemUsers { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Data Source=MSI;Initial Catalog=M2_;Integrated Security=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AllAssocManager>(entity =>
        {
            entity.ToView("allAssocManagers");
        });

        modelBuilder.Entity<AllClub>(entity =>
        {
            entity.ToView("allCLubs");
        });

        modelBuilder.Entity<AllClubRepresentative>(entity =>
        {
            entity.ToView("allClubRepresentatives");
        });

        modelBuilder.Entity<AllFan>(entity =>
        {
            entity.ToView("allFans");
        });

        modelBuilder.Entity<AllMatch>(entity =>
        {
            entity.ToView("allMatches");
        });

        modelBuilder.Entity<AllRequest>(entity =>
        {
            entity.ToView("allRequests");
        });

        modelBuilder.Entity<AllStadium>(entity =>
        {
            entity.ToView("allStadiums");
        });

        modelBuilder.Entity<AllStadiumManager>(entity =>
        {
            entity.ToView("allStadiumManagers");
        });

        modelBuilder.Entity<AllTicket>(entity =>
        {
            entity.ToView("allTickets");
        });

        modelBuilder.Entity<Club>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Club__3213E83F247B5D5C");
        });

        modelBuilder.Entity<ClubRepresentative>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Club_Rep__3213E83F21965305");

            entity.HasOne(d => d.Club).WithMany(p => p.ClubRepresentatives)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("CR_FK2");

            entity.HasOne(d => d.Super).WithMany(p => p.ClubRepresentatives).HasConstraintName("CR_FK1");
        });

        modelBuilder.Entity<ClubsNeverMatched>(entity =>
        {
            entity.ToView("clubsNeverMatched");
        });

        modelBuilder.Entity<ClubsWithNoMatch>(entity =>
        {
            entity.ToView("clubsWithNoMatches");
        });

        modelBuilder.Entity<Fan>(entity =>
        {
            entity.HasKey(e => e.NationalId).HasName("PK__Fan__9560E95CFCA33E99");

            entity.Property(e => e.Status).HasDefaultValueSql("('1')");

            entity.HasOne(d => d.Super).WithMany(p => p.Fans).HasConstraintName("F_FK");

            entity.HasMany(d => d.Tickets).WithMany(p => p.Fans)
                .UsingEntity<Dictionary<string, object>>(
                    "TicketsBought",
                    r => r.HasOne<Ticket>().WithMany()
                        .HasForeignKey("TicketId")
                        .HasConstraintName("TB_FK2"),
                    l => l.HasOne<Fan>().WithMany()
                        .HasForeignKey("FanId")
                        .HasConstraintName("TB_FK1"),
                    j =>
                    {
                        j.HasKey("FanId", "TicketId").HasName("TB_PK");
                        j.ToTable("Tickets_Bought");
                    });
        });

        modelBuilder.Entity<HostRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Host_Req__3213E83FE393EFF3");

            entity.Property(e => e.Status).HasDefaultValueSql("((-1))");

            entity.HasOne(d => d.ClubRepresentative).WithMany(p => p.HostRequests).HasConstraintName("HR_FK2");

            entity.HasOne(d => d.StadiumManager).WithMany(p => p.HostRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("HR_FK1");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Match__3213E83F917F565F");

            entity.HasOne(d => d.Guest).WithMany(p => p.MatchGuests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("M_FK2");

            entity.HasOne(d => d.Host).WithMany(p => p.MatchHosts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("M_FK1");

            entity.HasOne(d => d.HostStadium).WithMany(p => p.Matches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("M_FK3");
        });

        modelBuilder.Entity<MatchesPerTeam>(entity =>
        {
            entity.ToView("matchesPerTeam");
        });

        modelBuilder.Entity<SportsAssociationManager>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sports_A__3213E83FFC0007A0");

            entity.HasOne(d => d.Super).WithMany(p => p.SportsAssociationManagers).HasConstraintName("SAM_FK");
        });

        modelBuilder.Entity<Stadium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stadium__3213E83F68D9A398");

            entity.Property(e => e.Status).HasDefaultValueSql("('1')");
        });

        modelBuilder.Entity<StadiumManager>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stadium___3213E83F46C9F572");

            entity.HasOne(d => d.Stadium).WithMany(p => p.StadiumManagers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SM_FK2");

            entity.HasOne(d => d.Super).WithMany(p => p.StadiumManagers).HasConstraintName("SM_FK1");
        });

        modelBuilder.Entity<SystemAdmin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__System_A__3213E83F78E43DC4");

            entity.HasOne(d => d.Super).WithMany(p => p.SystemAdmins).HasConstraintName("SA_FK");
        });

        modelBuilder.Entity<SystemUser>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__SystemUs__F3DBC57335E6EBF1");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ticket__3213E83F6C67A4A9");

            entity.HasOne(d => d.Match).WithMany(p => p.Tickets).HasConstraintName("T_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
