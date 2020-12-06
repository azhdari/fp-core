using LanguageExt;
using WorkTracker.Core.Types;
using static LanguageExt.Prelude;

namespace WorkTracker.Core.Endpoints
{
    public interface IEndpoint<TInput, TOutput>
    {
        public EitherAsync<DbError, TOutput> Invoke(TInput input);
    }
}
