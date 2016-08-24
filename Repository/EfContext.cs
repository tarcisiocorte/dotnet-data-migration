
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace tccp.Reposotiry
{
    public class EfContext : IDbContextEf<PersistenceContext>
    {
        private const string CONNECTIONSTRING= @""; 
        public PersistenceContext Create(DbContextEfOptions options)
        {
            var constructor = new DbContextEfOptionsBuilder<PersistenceContext>(); 
            construtor.UseSqlServer(CONNECTIONSTRING);

            return new PersistenceContext(constructor.Options);
        }
    }
}