using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace sistemaeducativo.Models;

public partial class SistemaeducativoContext : IdentityDbContext
{
    public SistemaeducativoContext()
    {
    }

    public SistemaeducativoContext(DbContextOptions<SistemaeducativoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Matricula> Matriculas { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=MEDAPRCSGFSP819;Initial Catalog=sistemaeducativo;integrated security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.CursoId).HasName("PK__Curso__7E023A374BF7BDCE");

            entity.ToTable("Curso");

            entity.Property(e => e.CursoId).HasColumnName("CursoID");
            entity.Property(e => e.NombreCurso)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.EstudianteId).HasName("PK__Estudian__6F76833816D78E83");

            entity.HasIndex(e => e.Email, "UQ__Estudian__A9D10534BEFF7B63").IsUnique();

            entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.MatriculaId).HasName("PK__Matricul__908CEE2227B63E41");

            entity.ToTable("Matricula");

            entity.Property(e => e.MatriculaId).HasColumnName("MatriculaID");
            entity.Property(e => e.CursoId).HasColumnName("CursoID");
            entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");
            entity.Property(e => e.FechaMatricula).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Curso).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.CursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Matricula_Curso");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.EstudianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Matricula_Estudiante");
            base.OnModelCreating(modelBuilder);//agregando el identity
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
