using System.Linq;
using System.Threading.Tasks;
using EE.Cliente.API.Models;
using EE.Core.Data;
using EE.Core.DomainObjects;
using EE.Core.Mediator;
using Microsoft.EntityFrameworkCore;

namespace EE.Cliente.API.Data
{
    /// <summary>
    /// Classe de contexto de Clientes
    /// </summary>
    public class ClienteContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        
        /// <summary>
        /// Construtor de Clientes
        /// </summary>
        /// <param name="options"></param>
        public ClienteContext(DbContextOptions<ClienteContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
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
            if (sucesso)
            {
                await _mediatorHandler.PublicarEventos(this);
            }
            return sucesso;
        }
    }

    public static class MediatorExtension
    {
        public static async Task PublicarEventos<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notificacoes)
                .ToList();
            
            domainEntities.ToList()
                .ForEach(entity => entity.Entity.LimparEventos());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.PublicarEvento(domainEvent);
                });
            await Task.WhenAll(tasks);
        }
    }
}