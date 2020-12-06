using LanguageExt.Common;

namespace WorkTracker.Core.Types
{
    public record DbError(Error Error, string? Message);
}
