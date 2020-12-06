using FluentMigrator;
using System;
using WorkTracker.Core.Database.Entities;

namespace WorkTracker.Core.Database.Migrations
{
    [Migration(202012061500)]
    public class M001__Initialiaze : AutoReversingMigration
    {
        public override void Up()
        {
            CreateDb();
        }

        private void CreateDb()
        {
            Create.Schema(DbContext.SCHEMA);
            Create.Table("Record")
                  .InSchema(DbContext.SCHEMA)
                  .WithColumn(nameof(RecordEntity.Id)).AsGuid().NotNullable().PrimaryKey().WithDefaultValue(SystemMethods.NewSequentialId)
                  .WithColumn(nameof(RecordEntity.Caption)).AsString().NotNullable()
                  .WithColumn(nameof(RecordEntity.Ticks)).AsInt64().NotNullable().WithDefaultValue(0)
                  .WithColumn(nameof(RecordEntity.StartAt)).AsDateTime2().NotNullable().WithDefaultValue(SystemMethods.CurrentUTCDateTime)
                  .WithColumn(nameof(RecordEntity.StopAt)).AsDateTime2().Nullable()
                  .WithColumn(nameof(RecordEntity.CreatedAt)).AsDateTime2().NotNullable().WithDefaultValue(SystemMethods.CurrentUTCDateTime)
                  .WithColumn(nameof(RecordEntity.UpdatedAt)).AsDateTime2().NotNullable().WithDefaultValue(SystemMethods.CurrentUTCDateTime);
        }
    }
}
