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
            constructorModel.Entity<Customer>( etd=>
            {
                etd.ToTable("tblCustomer");
                etd.HasKey(c => c.Id).HasName("idCustomer");
                etd.Property(c => c.Id).HasColumnName("idCustomer").ValueGeneratedOnAdd();
                etd.Property(c => c.Nome).HasColumnName("name").HasMaxLength(100);
                etd.Property(c => c.Email).HasColumnName("email").HasMaxLength(30);
            });                
        }

        private void ConfiguraPedido(ModelBuilder constructorModel)
        {
            constructorModel.Entity<Pedido>(etd =>
            {
                etd.ToTable("tblOrder");
                etd.HasKey(p => p.Id).HasName("idOrder");
                etd.Property(p => p.Id).HasColumnName("idOrder").ValueGeneratedOnAdd();                                
                etd.Property(p => p.Total).HasColumnName("total");
                etd.HasOne(c => c.Customer).WithMany(p => p.Orders);
            });
        }
    }
}