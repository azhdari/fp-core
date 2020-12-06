using LanguageExt;
using System;
using WorkTracker.Core.Database.Entities;
using WorkTracker.Core.Types;

namespace WorkTracker.Core.Database.Repositories.RecordRepo
{
    public interface IRecordRepository
    {
        EitherAsync<DbError, Guid> Create(RecordEntity entity);

        EitherAsync<DbError, Option<RecordEntity>> FindById(Guid id);
    }
}
