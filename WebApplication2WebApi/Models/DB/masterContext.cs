using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2WebApi.Models.DB;

public partial class masterContext : DbContext
{
    public masterContext()
    {
    }

    public masterContext(DbContextOptions<masterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comune> Comune { get; set; }

    public virtual DbSet<Persona> Persona { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=AC-HDEKANIS\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comune>(entity =>
        {
            entity.Property(e => e.Descrizione).HasMaxLength(50);
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasOne(d => d.Id_ComuneNascitaNavigation).WithMany(p => p.Persona)
                .HasForeignKey(d => d.Id_ComuneNascita)
                .HasConstraintName("FK_Persona_Comune");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
