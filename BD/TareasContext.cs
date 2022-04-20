using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BD
{
    public partial class TareasContext : DbContext
    {
        public TareasContext()
        {
        }

        public TareasContext(DbContextOptions<TareasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tarea> Tareas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=Tareas;Username=alejo688;Password=900330");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.ToTable("Tarea");

                entity.Property(e => e.Descripcion).HasMaxLength(4000);

                entity.Property(e => e.FechaFinal)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("Fecha_Final");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("Fecha_Inicio");

                entity.Property(e => e.Nombre).HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
