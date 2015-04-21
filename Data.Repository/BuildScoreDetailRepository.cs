namespace Farfetch.Buildionaire.Data.Repository
{
    using System.Data.Entity;

    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Domain.Model;

    using SharpRepository.EfRepository;

    public class BuildScoreDetailRepository : EfRepository<BuildScoreDetail, int>, IBuildScoreDetailRepository
    {
        public BuildScoreDetailRepository(DbContext context)
            : base(context)
        {
        }
    }
}