using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Judoca.ModelsEF
{
    public partial class JudocaContext : DbContext
    {
        public JudocaContext()
        {
        }

        public JudocaContext(DbContextOptions<JudocaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblFiliado> TblFiliado { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=judoca.cfk4trdj3utg.us-east-1.rds.amazonaws.com,1433;Database=Judoca;User ID=admin;Password=master123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblFiliado>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("TBL_FILIADO");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Aniversario)
                    .HasColumnName("ANIVERSARIO")
                    .HasColumnType("date");

                entity.Property(e => e.Cpf)
                    .HasColumnName("CPF")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.DataCadastro)
                    .HasColumnName("DATA_CADASTRO")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasColumnName("NOME")
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Observacao)
                    .HasColumnName("OBSERVACAO")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OrgaoExp)
                    .HasColumnName("ORGAO_EXP")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.RegistroCbj)
                    .HasColumnName("REGISTRO_CBJ")
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Rg)
                    .HasColumnName("RG")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone1)
                    .HasColumnName("TELEFONE1")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone2)
                    .HasColumnName("TELEFONE2")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasColumnName("TIPO")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });
        }
    }
}
