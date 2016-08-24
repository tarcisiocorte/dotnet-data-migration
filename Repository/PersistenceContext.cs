using Microsoft.EntityFrameworkCore;
using Projeto.Dominio;

namespace tccp.Reposotiry
{
    public class PersistenceContext : DbContext
    {
        public PersistenceContext()
        { }

        public PersistenceContext(DbContextOptions<PersistenceContext> opt )
            :base(opt)
        {             
            opt = new DbContextOptions<PersistenceContext>();            
        }

        public DbSet<Custumer> Custumers { get; set; }
        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(ModelBuilder constructorModel)
        {
            constructorModel .ForSqlServerUseIdentityColumns();
            constructorModel.HasDefaultSchema("OrderControl");
            SetCustomer(constructorModel);
            SetOrder(constructorModel);
        }

        private void SetCustomer(ModelBuilder constructorModel)
        {
            constructorModel.Entity<Cliente>( etd=>
            {
                etd.ToTable("tbCliente");
                etd.HasKey(c => c.Id).HasName("idCliente");
                etd.Property(c => c.Id).HasColumnName("idCliente").ValueGeneratedOnAdd();
                etd.Property(c => c.Nome).HasColumnName("nome").HasMaxLength(100);
                etd.Property(c => c.Email).HasColumnName("email").HasMaxLength(30);
            });                
        }

        private void ConfiguraPedido(ModelBuilder constructorModel)
        {
            constructorModel.Entity<Pedido>(etd =>
            {
                etd.ToTable("tbPedido");
                etd.HasKey(p => p.Id).HasName("idPedido");
                etd.Property(p => p.Id).HasColumnName("idPedido").ValueGeneratedOnAdd();                                
                etd.Property(p => p.Total).HasColumnName("total");
                etd.HasOne(c => c.Cliente).WithMany(p => p.Pedidos);
            });
        }
    }
}