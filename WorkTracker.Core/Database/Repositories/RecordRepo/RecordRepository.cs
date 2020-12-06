using LanguageExt;
using LinqToDB;
using System;
using WorkTracker.Core.Database.Entities;
using WorkTracker.Core.Types;
using static LanguageExt.Prelude;

namespace WorkTracker.Core.Database.Repositories.RecordRepo
{
    public class RecordRepository : IRecordRepository
    {
        private readonly DbContext _db;

        public RecordRepository(DbContext dbContext)
        {
            _db = dbContext;
        }

        public EitherAsync<DbError, Guid> Create(RecordEntity entity) =>
            TryAsync(_db.InsertWithIdentityAsync(entity))
                .Map(ToGuid)
                .ToEither()
                .BindLeft(error => EitherAsync<DbError, Guid>.Left(new DbError(error, $"Error while creating record: {error.Message}")));

        public EitherAsync<DbError, Option<RecordEntity>> FindById(Guid id) =>
            TryAsync(_db.Record.FirstOrDefaultAsync(x => x.Id == id))
                .Map(Optional)
                .ToEither()
                .BindLeft(error => EitherAsync<DbError, Option<RecordEntity>>.Left(new DbError(error, $"Error while creating record: {error.Message}")));

        private Guid ToGuid(object idObj) => idObj is Guid id ? id : Guid.Empty;
    }
}
