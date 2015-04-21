namespace Farfetch.Buildionaire.Data.Repository
{
    using System.Data.Entity;
    using System.Linq;

    using Farfetch.Buildionaire.Domain.Core.RepositoriesInterfaces;
    using Farfetch.Buildionaire.Domain.Model;

    using SharpRepository.EfRepository;

    public class ChangesetRepository : EfRepository<ChangeSet, int>, IChangesetRepository
    {
        public ChangesetRepository(DbContext context)
            : base(context)
        {
        }
        
        public ChangeSet GetLast()
        {
            return this.DbSet.OrderByDescending(x => x.CreatedAt).FirstOrDefault();
        }
    }
}
