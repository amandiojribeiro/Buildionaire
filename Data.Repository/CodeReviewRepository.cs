namespace Farfetch.Buildionaire.Data.Repository
{
    using System.Data.Entity;
    using System.Linq;

    using Farfetch.Buildionaire.Domain.Core.RepositoriesInterfaces;
    using Farfetch.Buildionaire.Domain.Model;

    using SharpRepository.EfRepository;

    public class CodeReviewRepository : EfRepository<CodeReview, int>, ICodeReviewRepository
    {
        public CodeReviewRepository(DbContext context)
            : base(context)
        {
        }

        public CodeReview GetLast()
        {
            return this.DbSet.OrderByDescending(x => x.CreatedAt).FirstOrDefault();
        }
    }
}
