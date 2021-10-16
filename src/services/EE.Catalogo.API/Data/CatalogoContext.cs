using System.Linq;
using System.Threading.Tasks;
using EE.Catalogo.API.Models;
using EE.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace EE.Catalogo.API.Data
{
    public class CatalogoContext : DbContext, IUnitOfWork
    {
        public DbSet<Produto> Produtos { get; set; }

        public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //todas as propriedades que eu esqueci de mapear ele vai vim por padrão com varchar(100)
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}