using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Pletko.Models;

public partial class PletkoContext : DbContext
{
    public PletkoContext()
    {
    }

    public PletkoContext(DbContextOptions<PletkoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Kategorija> Kategorijas { get; set; }

    public virtual DbSet<Korisnik> Korisniks { get; set; }

    public virtual DbSet<Narudzbina> Narudzbinas { get; set; }

    public virtual DbSet<Proizvod> Proizvods { get; set; }

    public virtual DbSet<SpecijalnaPonudum> SpecijalnaPonuda { get; set; }

    public virtual DbSet<UserStatus> UserStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-V5EBUVQ\\SQLEXPRESS;Database=Pletko;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kategorija>(entity =>
        {
            entity.ToTable("Kategorija");

            entity.Property(e => e.KategorijaId).HasColumnName("Kategorija_ID");
            entity.Property(e => e.Naziv).HasMaxLength(20);
        });

        modelBuilder.Entity<Korisnik>(entity =>
        {
            entity.HasKey(e => e.KorisnikId).HasName("PK_Korisnici");

            entity.ToTable("Korisnik");

            entity.Property(e => e.KorisnikId).HasColumnName("Korisnik_ID");
            entity.Property(e => e.Adresa).HasMaxLength(50);
            entity.Property(e => e.BrojTelefona)
                .HasMaxLength(15)
                .IsFixedLength();
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Ime).HasMaxLength(35);
            entity.Property(e => e.Password).HasMaxLength(150);
            entity.Property(e => e.PonudaId).HasColumnName("Ponuda_ID");
            entity.Property(e => e.Prezime).HasMaxLength(35);
            entity.Property(e => e.StatusId)
                .HasDefaultValue(333)
                .HasColumnName("Status_ID");

            entity.HasOne(d => d.Ponuda).WithMany(p => p.Korisniks)
                .HasForeignKey(d => d.PonudaId)
                .HasConstraintName("FK_Korisnik_SpecijalnaPonuda");

            entity.HasOne(d => d.Status).WithMany(p => p.Korisniks)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Korisnik_UserStatus");
        });

        modelBuilder.Entity<Narudzbina>(entity =>
        {
            entity.ToTable("Narudzbina");

            entity.Property(e => e.NarudzbinaId).HasColumnName("Narudzbina_ID");
            entity.Property(e => e.BrojKartice)
                .HasMaxLength(19)
                .HasColumnName("Broj_Kartice");
            entity.Property(e => e.DatumNarudzbine)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");
            entity.Property(e => e.KorisnikId).HasColumnName("Korisnik_ID");
            entity.Property(e => e.ProizvodId).HasColumnName("Proizvod_ID");
            entity.Property(e => e.UkupnaCena).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Korisnik).WithMany(p => p.Narudzbinas)
                .HasForeignKey(d => d.KorisnikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Narudzbina_Korisnik");

            entity.HasOne(d => d.Proizvod).WithMany(p => p.Narudzbinas)
                .HasForeignKey(d => d.ProizvodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Narudzbina_Proizvod");
        });

        modelBuilder.Entity<Proizvod>(entity =>
        {
            entity.ToTable("Proizvod");

            entity.Property(e => e.ProizvodId).HasColumnName("Proizvod_id");
            entity.Property(e => e.Cena).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.KategorijaId).HasColumnName("Kategorija_ID");
            entity.Property(e => e.KorisnikId).HasColumnName("Korisnik_ID");
            entity.Property(e => e.Naziv).HasMaxLength(50);
            entity.Property(e => e.Opis).HasMaxLength(250);

            entity.HasOne(d => d.Kategorija).WithMany(p => p.Proizvods)
                .HasForeignKey(d => d.KategorijaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proizvod_Kategorija");
        });

        modelBuilder.Entity<SpecijalnaPonudum>(entity =>
        {
            entity.HasKey(e => e.PonudaId);

            entity.Property(e => e.PonudaId).HasColumnName("Ponuda_ID");
            entity.Property(e => e.KorisnikId).HasColumnName("Korisnik_ID");
            entity.Property(e => e.Naziv).HasMaxLength(50);
            entity.Property(e => e.Opis).HasMaxLength(250);
            entity.Property(e => e.Popust).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<UserStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId);

            entity.ToTable("UserStatus");

            entity.Property(e => e.StatusId)
                .ValueGeneratedNever()
                .HasColumnName("Status_id");
            entity.Property(e => e.Naziv).HasMaxLength(10);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
