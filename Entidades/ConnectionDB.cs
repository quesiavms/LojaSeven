using Microsoft.EntityFrameworkCore;

namespace LojaSeven.Entidades
{
    public class ConnectionDB : DbContext
    {
        public ConnectionDB(DbContextOptions<ConnectionDB> options)
            : base(options)
        {
        }

        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<TipoPagamento> TipoPagamento { get; set; }
        public DbSet<Compra> Compra { get; set; }
    }
}
