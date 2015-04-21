namespace Farfetch.Buildionaire.Domain.Core
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Farfetch.Buildionaire.Domain.Model;

    using SharpRepository.Repository;

    public interface IBuildRepository : IRepository<Build, int>
    {
        Build GetLastBuild();

        Task<IList<Build>> GetHighlitedBuilds(bool top, int? take, string dashboardCode = null);

        Task<Build> GetLastFailedBuildAsync();

        Task<IList<Build>> GetMonthHighlitedBuilds(int year, int month, bool top, int? take, string dashboardCode);
    }
}
