using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace RhyoliteERP.EntityFrameworkCore
{
    public static class RhyoliteERPDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<RhyoliteERPDbContext> builder, string connectionString)
        {
            builder.UseNpgsql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<RhyoliteERPDbContext> builder, DbConnection connection)
        {
            builder.UseNpgsql(connection);
        }
    }
}
