using System;
using ASM_NET104Contenxt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ASM_NET104.Models
{
    public partial class ASM_NET104Context : DbContext
    {
        //public ASM_NET104Context()
        //{
        //}

        public ASM_NET104Context(DbContextOptions<ASM_NET104Context> options)
            : base(options)
        {
        }

        public virtual DbSet<ChiTietHd> ChiTietHd { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<NhomSanPham> NhomSanPham { get; set; }
        public virtual DbSet<Sanpham> Sanpham { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietHd>(entity =>
            {
                entity.ToTable("ChiTietHD");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdhoaDon).HasColumnName("IDHoaDon");

                entity.Property(e => e.MaSp).HasColumnName("MaSP");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<NhomSanPham>(entity =>
            {
                entity.HasKey(e => e.MaNhom)
                    .HasName("PK__NHOM_San__234F91CD46FE5B5D");

                entity.ToTable("NHOM_SanPham");

                entity.Property(e => e.MaNhom).ValueGeneratedNever();

                entity.Property(e => e.TenNhom)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sanpham>(entity =>
            {
                entity.HasKey(e => e.MaSp)
                    .HasName("PK__SANPHAM__2725081C4E59260D");

                entity.ToTable("SANPHAM");

                entity.Property(e => e.MaSp)
                    .HasColumnName("MaSP")
                    .HasColumnType("INT");

                entity.Property(e => e.DonGia).HasColumnType("money");

                entity.Property(e => e.HinhAnh).IsUnicode(false);

                entity.Property(e => e.MoTa).HasMaxLength(150);

                entity.Property(e => e.TenSp)
                    .IsRequired()
                    .HasColumnName("TenSP");

                entity.HasOne(d => d.NhomNavigation)
                    .WithMany(p => p.Sanpham)
                    .HasForeignKey(d => d.Nhom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SANPHAM__Nhom__15502E78");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
