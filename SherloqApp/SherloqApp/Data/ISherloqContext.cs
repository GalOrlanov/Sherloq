using Microsoft.EntityFrameworkCore;

namespace SherloqApp.Data
{
    public interface ISherloqContext
    {
        DbSet<Database> Databases { get; set; }
        DbSet<Table> Tables { get; set; }
        DbSet<Field> Fields { get; set; }
        DbSet<Query> Queries { get; set; }
        DbSet<ParsedQueriesFieldAccessLog> ParsedQueriesFieldAccessLogs { get; set; }
    }
}
