using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CsvPnl.Database
{
    public partial class PnldbContext : DbContext
    {
        public PnldbContext()
        {
        }

        public PnldbContext(DbContextOptions<PnldbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Capital> Capitals { get; set; }
        public virtual DbSet<Pnl> Pnls { get; set; }
        public virtual DbSet<Strategy> Strategies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("data source=LAPTOP-98B3EA6L\\SQLEXPRESS;integrated security=True;initial catalog=Pnldb;MultipleActiveResultSets=True;App=EntityFramework");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Capital>(entity =>
            {
                entity.ToTable("Capital");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CapitalDate).HasColumnType("date");

                entity.HasOne(d => d.Strategy)
                    .WithMany(p => p.Capitals)
                    .HasForeignKey(d => d.StrategyId)
                    .HasConstraintName("FK__Capital__Strateg__3E52440B");
            });

            modelBuilder.Entity<Pnl>(entity =>
            {
                entity.ToTable("Pnl");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PnlDate).HasColumnType("date");

                entity.HasOne(d => d.Strategy)
                    .WithMany(p => p.Pnls)
                    .HasForeignKey(d => d.StrategyId)
                    .HasConstraintName("FK__Pnl__StrategyId__412EB0B6");
            });

            modelBuilder.Entity<Strategy>(entity =>
            {
                entity.ToTable("Strategy");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Region);

                entity.Property(e => e.StrategyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
