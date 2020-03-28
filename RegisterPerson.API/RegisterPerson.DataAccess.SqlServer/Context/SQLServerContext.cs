
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using RegisterPerson.Domain.Model.Entities;

namespace RegisterPerson.DataAccess.SqlServer.Context
{
    public class SQLServerContext : DbContext
    {
        public DbSet<Person> Person { get; set; }
=======
using AuthJWT.Domain.Model.Entities;

namespace AuthJWT.DataAccess.SqlServer.Context
{
    public class SQLServerContext : DbContext
    {
        public DbSet<User> User { get; set; }
>>>>>>> master
        
        public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options)
        {
        }
    }
}
