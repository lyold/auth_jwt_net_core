
using Microsoft.EntityFrameworkCore;
using AuthJWT.Domain.Model.Entities;

namespace AuthJWT.DataAccess.SqlServer.Context
{
    public class SQLServerContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options)
        {
        }
    }
}
