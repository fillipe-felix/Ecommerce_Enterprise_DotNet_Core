using System.Linq;
using System.Threading.Tasks;
using EE.Cliente.API.Models;
using EE.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace EE.Cliente.API.Data
{
    /// <summary>
    /// Classe de contexto de Clientes
    /// </summary>
    public class ClienteContext : DbContext, IUnitOfWork
    {
        /// <summary>
        /// Construtor de Clientes
        /// </summary>
        /// <param name="options"></param>
        public ClienteContext(DbContextOptions<ClienteContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Models.Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        /// <summary>
        /// Meotodo de criação de EF
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100");
            }

            //Desativa a deleção em casacata
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClienteContext).Assembly);
        }


        /// <summary>
        /// Método para persistir os dados
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Commit()
        {
            var sucesso = await base.SaveChangesAsync() > 0;
            return sucesso;
        }
    }
}