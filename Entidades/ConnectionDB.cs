using Microsoft.EntityFrameworkCore;

namespace LojaSeven.Entidades
{
    public class ConnectionDB : DbContext
    {
        private readonly IConfiguration _configuration;

        public ConnectionDB(IConfiguration configuration, DbContextOptions<ConnectionDB> options)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Produtos> Produtos { get; set; }

        public DbSet<TipoPagamento> TipoPagamento { get; set; }
        public DbSet<Compra> Compra { get;set; }
    }
}
