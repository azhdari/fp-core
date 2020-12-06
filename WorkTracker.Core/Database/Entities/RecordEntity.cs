using LinqToDB.Mapping;
using System;
using WorkTracker.Core.Database.Infrastructure;

namespace WorkTracker.Core.Database.Entities
{
    [Table("Record", Schema = DbContext.SCHEMA)]
    public class RecordEntity : IEntity
    {
        [Column(SkipOnInsert = true), PrimaryKey]
        public Guid Id { get; set; }

        [Column, NotNull]
        public string Caption { get; set; } = string.Empty;
        
        [Column]
        public long Ticks { get; set; }

        [Column, NotNull]
        public DateTime StartAt { get; set; }

        [Column, Nullable]
        public DateTime? StopAt { get; set; }

        [Column(SkipOnUpdate = true)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
