using LanguageExt;
using System;
using WorkTracker.Core.Database.Entities;
using WorkTracker.Core.Database.Repositories.RecordRepo;
using WorkTracker.Core.Records;
using WorkTracker.Core.Types;
using static LanguageExt.Prelude;

namespace WorkTracker.Core.Endpoints
{
    public class CreateRecordEndpoint : IEndpoint<CreateRecord, Record>
    {
        private readonly IRecordRepository _repo;

        public CreateRecordEndpoint(IRecordRepository repo)
        {
            _repo = repo;
        }

        public EitherAsync<DbError, Record> Invoke(CreateRecord record) => pipe(
            ToEntity(record),
            _repo.Create,
            self => bind(self, id => pipe(
                    _repo.FindById(id),
                    self => bind(self, res => res.Map(ToRecord).ToEitherAsync(() => new DbError(new(), "not found")))
                    )
                )
            );

        private static RecordEntity ToEntity(CreateRecord entity) => new RecordEntity
        {
            Caption = entity.Caption,
            StartAt = entity.StartAt,
            StopAt = entity.StopAt.ToNullable(),
            Ticks = (entity.StopAt.IfNone(() => entity.StartAt) - entity.StartAt).Ticks,
        };

        private static Record ToRecord(RecordEntity entity) => new Record(
            entity.Id,
            entity.Caption,
            entity.StartAt,
            Optional(entity.StopAt),
            TimeSpan.FromTicks(entity.Ticks)
            );
    }
}
