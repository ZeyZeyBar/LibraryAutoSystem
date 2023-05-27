using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryAutoSystem.Models;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GecmisOduncler> GecmisOdunclers { get; set; }

    public virtual DbSet<IadeKitaplar> IadeKitaplars { get; set; }

    public virtual DbSet<Kitaplar> Kitaplars { get; set; }

    public virtual DbSet<OduncAlinanKitaplar> OduncAlinanKitaplars { get; set; }

    public virtual DbSet<Uyeler> Uyelers { get; set; }

    public virtual DbSet<Yazar> Yazars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Library;Username=postgres;Password=123456");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GecmisOduncler>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("gecmis_oduncler");

            entity.Property(e => e.IadeEdenId).HasColumnName("iade_eden_id");
            entity.Property(e => e.IadeKitapId).HasColumnName("iade_kitap_id");
            entity.Property(e => e.OduncAlmaTarihi).HasColumnName("odunc_alma_tarihi");
        });

        modelBuilder.Entity<IadeKitaplar>(entity =>
        {
            entity.HasKey(e => new { e.IadeKitapId, e.IadeEdenId }).HasName("iade_kitaplar_pkey");

            entity.ToTable("iade_kitaplar");

            entity.Property(e => e.IadeKitapId).HasColumnName("iade_kitap_id");
            entity.Property(e => e.IadeEdenId).HasColumnName("iade_eden_id");
            entity.Property(e => e.IadeTarihi).HasColumnName("iade_tarihi");

            entity.HasOne(d => d.IadeEden).WithMany(p => p.IadeKitaplars)
                .HasForeignKey(d => d.IadeEdenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("iade_kitaplar_iade_eden_id_fkey");

            entity.HasOne(d => d.IadeKitap).WithMany(p => p.IadeKitaplars)
                .HasForeignKey(d => d.IadeKitapId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("iade_kitaplar_iade_kitap_id_fkey");
        });

        modelBuilder.Entity<Kitaplar>(entity =>
        {
            entity.HasKey(e => e.KitapId).HasName("kitaplar_pkey");

            entity.ToTable("kitaplar");

            entity.Property(e => e.KitapId).HasColumnName("kitap_id");
            entity.Property(e => e.IsbnNo)
                .HasMaxLength(20)
                .HasColumnName("isbn_no");
            entity.Property(e => e.KitapAdi)
                .HasMaxLength(255)
                .HasColumnName("kitap_adi");
            entity.Property(e => e.Tur)
                .HasMaxLength(100)
                .HasColumnName("tur");
            entity.Property(e => e.YayinEvi)
                .HasMaxLength(255)
                .HasColumnName("yayin_evi");
            entity.Property(e => e.YayinTarihi).HasColumnName("yayin_tarihi");
            entity.Property(e => e.YazarId).HasColumnName("yazar_id");

            entity.HasOne(d => d.Yazar).WithMany(p => p.Kitaplars)
                .HasForeignKey(d => d.YazarId)
                .HasConstraintName("kitaplar_yazar_id_fkey");
        });

        modelBuilder.Entity<OduncAlinanKitaplar>(entity =>
        {
            entity.HasKey(e => new { e.KitapId, e.UyeId, e.OduncAlmaTarihi }).HasName("odunc_alinan_kitaplar_pkey");

            entity.ToTable("odunc_alinan_kitaplar");

            entity.Property(e => e.KitapId).HasColumnName("kitap_id");
            entity.Property(e => e.UyeId).HasColumnName("uye_id");
            entity.Property(e => e.OduncAlmaTarihi).HasColumnName("odunc_alma_tarihi");

            entity.HasOne(d => d.Kitap).WithMany(p => p.OduncAlinanKitaplars)
                .HasForeignKey(d => d.KitapId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("odunc_alinan_kitaplar_kitap_id_fkey");

            entity.HasOne(d => d.Uye).WithMany(p => p.OduncAlinanKitaplars)
                .HasForeignKey(d => d.UyeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("odunc_alinan_kitaplar_uye_id_fkey");
        });

        modelBuilder.Entity<Uyeler>(entity =>
        {
            entity.HasKey(e => e.UyeId).HasName("uyeler_pkey");

            entity.ToTable("uyeler");

            entity.Property(e => e.UyeId).HasColumnName("uye_id");
            entity.Property(e => e.EPosta)
                .HasMaxLength(255)
                .HasColumnName("e_posta");
            entity.Property(e => e.KayitTarihi).HasColumnName("kayit_tarihi");
            entity.Property(e => e.Sifre)
                .HasMaxLength(255)
                .HasColumnName("sifre");
            entity.Property(e => e.Telefon)
                .HasMaxLength(20)
                .HasColumnName("telefon");
            entity.Property(e => e.UyeAdi)
                .HasMaxLength(255)
                .HasColumnName("uye_adi");
            entity.Property(e => e.UyeSoyadi)
                .HasMaxLength(255)
                .HasColumnName("uye_soyadi");
        });

        modelBuilder.Entity<Yazar>(entity =>
        {
            entity.HasKey(e => e.YazarId).HasName("yazar_pkey");

            entity.ToTable("yazar");

            entity.Property(e => e.YazarId).HasColumnName("yazar_id");
            entity.Property(e => e.DogumTarihi).HasColumnName("dogum_tarihi");
            entity.Property(e => e.YazarAdi)
                .HasMaxLength(50)
                .HasColumnName("yazar_adi");
            entity.Property(e => e.YazarSoyadi)
                .HasMaxLength(100)
                .HasColumnName("yazar_soyadi");
            entity.Property(e => e.YazarUlke)
                .HasMaxLength(60)
                .HasColumnName("yazar_ulke");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
