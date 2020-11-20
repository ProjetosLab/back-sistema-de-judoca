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
        public virtual DbSet<TblEntidade> TblEntidade { get; set; }
        public virtual DbSet<TblCarteira> TblCarteiras { get; set; }
        internal DbQuery<Matricula> Matricula { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=judoca.cfk4trdj3utg.us-east-1.rds.amazonaws.com,1433;Database=Judoca;User ID=admin;Password=master123;");
            }
        }
        //dotnet ef dbcontext scaffold "Server=judoca.cfk4trdj3utg.us-east-1.rds.amazonaws.com,1433;Database=Judoca;User ID=admin;Password=master123;" Microsoft.EntityFrameworkCore.SqlServer -t [dbo].[TBL_ENTIDADE] -o ModelsEF
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Query<Matricula>().ToView("Associados","dbo");
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

            modelBuilder.Entity<TblEntidade>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("TBL_ENTIDADE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cnpj)
                    .HasColumnName("CNPJ")
                    .HasMaxLength(14)
                    .IsUnicode(false); ;

                entity.Property(e => e.DataCadastro)
                    .HasColumnName("DATA_CADASTRO")
                    .HasColumnType("date");

                entity.Property(e => e.Nome)
                    .HasColumnName("NOME")
                    .HasMaxLength(120)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblCarteira>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("TBL_CARTEIRA");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DataFinal)
                    .HasColumnType("date")
                    .HasColumnName("DATA_FINAL");

                entity.Property(e => e.DataInicio)
                    .HasColumnType("date")
                    .HasColumnName("DATA_INICIO");

                entity.Property(e => e.IdEntidade).HasColumnName("ID_ENTIDADE");

                entity.Property(e => e.IdFiliado).HasColumnName("ID_FILIADO");
            });
        }
    }
}
