using Microsoft.EntityFrameworkCore;

namespace ReactData.Data
{
    public class RepositoryContextFactory
    {
        public RepositoryContext CreateDbContext(string connectioString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseNpgsql(connectioString);

            return new RepositoryContext(optionsBuilder.Options);
        }
    }
}