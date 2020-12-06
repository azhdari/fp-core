using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;
using WorkTracker.Core.Database.Entities;

namespace WorkTracker.Core.Database
{
    public class DbContext : DataConnection
    {
        public const string SCHEMA = "main";

        public DbContext(LinqToDbConnectionOptions<DbContext> options) : base(options)
        {
        }

        public ITable<RecordEntity> Record => GetTable<RecordEntity>();
    }
}
