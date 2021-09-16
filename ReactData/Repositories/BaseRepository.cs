using ReactData.Data;

namespace ReactData.Repositories
{
    public abstract class BaseRepository
    {
        protected string ConnectionString { get; }
        protected RepositoryContextFactory ContextFactory { get; }

        public BaseRepository(string connectionString, RepositoryContextFactory contextFactory)
        {
            ConnectionString = connectionString;
            ContextFactory = contextFactory;
        }
    }
}