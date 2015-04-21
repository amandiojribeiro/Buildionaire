namespace Farfetch.Buildionaire.Data.Repository
{
    using System.Data.Entity;

    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Domain.Model;

    using SharpRepository.EfRepository;

    public class ProjectRepository : EfRepository<Project, int>, IProjectRepository
    {
        public ProjectRepository(DbContext context)
            : base(context)
        {
        }
    }
}
