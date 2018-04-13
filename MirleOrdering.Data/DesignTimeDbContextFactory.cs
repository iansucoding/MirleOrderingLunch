using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MirleOrdering.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MirleOrderingContext>
    {
        public MirleOrderingContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MirleOrderingContext>();
            builder.UseSqlServer("Data Source=.;Initial Catalog=MirleOrdering;Integrated Security=False;User ID=sa;Password=Pa$$w0rd;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            return new MirleOrderingContext(builder.Options);
        }
    }
}
