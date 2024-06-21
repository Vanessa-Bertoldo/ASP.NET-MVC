using Microsoft.EntityFrameworkCore;
using web.students.Models;

namespace web.students.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        // TODO: propriedade para manipular as entidades
        public DbSet<RepresentanteModel> Representantes { get; set; }
        public DbSet<ClienteModel> Cliente { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RepresentanteModel>(entity =>
            {
                //Nome para a tabela
                entity.ToTable("Representantes");
                //Chave primária
                entity.HasKey(e => e.RepresentanteId);
                //Tornando o nome obrigatório
                entity.Property(e => e.NomeRepresentante).IsRequired();
                //Adicionando indice unico para CPF
                entity.HasIndex(e => e.Cpf).IsUnique();
            });

            modelBuilder.Entity<ClienteModel>(entity =>
            {
            entity.ToTable("Clientes");
            entity.HasKey(e => e.ClienteId);
            entity.Property(e => e.Nome).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.DataNascimento).HasColumnType("date");
            entity.Property(e => e.Observacao).HasMaxLength(500);
            //Define a relação de um pra um com RepresentanteModel
            entity.HasOne(e => e.Representante)
                //Indica que um representante pode ter muitos clientes
                .WithMany()
                //Define a chave estrangeira
                .HasForeignKey(e => e.RepresentanteId)
                // Torna a chave estrangeira obrigatória
                .IsRequired();
            });
        }
        public DatabaseContext(DbContextOptions options) : base(options) { }
        protected DatabaseContext(){ }
    }
}
