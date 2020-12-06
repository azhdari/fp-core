using System;
using LanguageExt;
using static LanguageExt.Prelude;

namespace WorkTracker.Core.Records
{
    public record CreateRecord(
        string Caption,
        DateTime StartAt,
        Option<DateTime> StopAt);

    public record Record(
        Guid Id,
        string Caption,
        DateTime StartAt,
        Option<DateTime> StopAt,
        TimeSpan Duration);
}
