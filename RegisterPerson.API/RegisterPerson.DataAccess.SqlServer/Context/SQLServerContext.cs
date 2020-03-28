
using Microsoft.EntityFrameworkCore;
using RegisterPerson.Domain.Model.Entities;

namespace RegisterPerson.DataAccess.SqlServer.Context
{
    public class SQLServerContext : DbContext
    {
        public DbSet<Person> Person { get; set; }
        
        public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options)
        {
        }
    }
}
