using Microsoft.EntityFrameworkCore;
using web.students.Models;

namespace web.students.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        // TODO: propriedade para manipular a entidade de representante
        public DbSet<RepresentanteModel> Representantes { get; set; }

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
        }
        public DatabaseContext(DbContextOptions options) : base(options) { }
        protected DatabaseContext(){ }
    }
}
