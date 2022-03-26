using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SherloqApp.Models;

namespace SherloqApp.Data
{
    public class SherloqDbContext : ApiAuthorizationDbContext<ApplicationUser>, ISherloqContext
    {
        public DbSet<Database> Databases { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Query> Queries { get; set; }
        public DbSet<ParsedQueriesFieldAccessLog> ParsedQueriesFieldAccessLogs { get; set; }

        public SherloqDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {

        }
    }
}