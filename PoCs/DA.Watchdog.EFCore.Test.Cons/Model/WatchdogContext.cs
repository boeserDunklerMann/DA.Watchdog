using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DA.Watchdog.EFCore.Test.Cons.Model
{
    public partial class WatchdogContext : DbContext
    {
        public WatchdogContext()
        {
        }

        public WatchdogContext(DbContextOptions<WatchdogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Check> Check { get; set; }
        public virtual DbSet<Observable> Observable { get; set; }
        public virtual DbSet<ObservableMeta> ObservableMeta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Database=Watchdog; Trusted_Connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Check>(entity =>
            {
                entity.Property(e => e.CheckId)
                    .HasColumnName("CheckID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ObservableId).HasColumnName("ObservableID");

                entity.Property(e => e.TimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Observable)
                    .WithMany(p => p.Check)
                    .HasForeignKey(d => d.ObservableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Check_Observable");
            });

            modelBuilder.Entity<Observable>(entity =>
            {
                entity.Property(e => e.ObservableId)
                    .HasColumnName("ObservableID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ObservableMeta>(entity =>
            {
                entity.HasKey(e => e.ObservableId);

                entity.Property(e => e.ObservableId)
                    .HasColumnName("ObservableID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Observable)
                    .WithOne(p => p.ObservableMeta)
                    .HasForeignKey<ObservableMeta>(d => d.ObservableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObservableMeta_Observable");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
