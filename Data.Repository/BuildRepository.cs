namespace Farfetch.Buildionaire.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Farfetch.Buildionaire.Domain.Core;
    using Farfetch.Buildionaire.Domain.Model;

    using SharpRepository.EfRepository;

    public class BuildRepository : EfRepository<Build, int>, IBuildRepository
    {
        public BuildRepository(DbContext context)
            : base(context)
        {
        }

        public Build GetLastBuild()
        {
            return this.DbSet.OrderByDescending(x => x.CreatedAt).FirstOrDefault();
        }


        public async Task<IList<Build>> GetHighlitedBuilds(bool top, int? take, string dashboardCode = null)
        {
            IQueryable<Build> ret = this.DbSet;

            if (!string.IsNullOrEmpty(dashboardCode))
            {
                ret =
                    ret.Where(
                        a =>
                            a.Project.Dashboards.Any(
                                c => c.Code.Equals(dashboardCode, StringComparison.InvariantCultureIgnoreCase)));
            }

            ret = ret.GroupBy(x => x.Project.Name).SelectMany(x => x.OrderByDescending(y => y.CompletedAt).Take(1));

            ret = top ? ret.OrderByDescending(ob => ob.TotalScore) : ret.OrderBy(ob => ob.TotalScore);

            if (take.HasValue)
            {
                ret = ret.Take(take.Value);
            }

            return await Task.FromResult(ret.ToList());
        }


        public async Task<Build> GetLastFailedBuildAsync()
        {
            return await Task.FromResult(this.DbSet.OrderByDescending(y => y.CompletedAt).FirstOrDefault(y => y.Status == BuildStatus.Failed));
        }

        public async Task<IList<Build>> GetMonthHighlitedBuilds(int year, int month, bool top, int? take, string dashboardCode)
        {
            var initDate = new DateTime(year, month, 1);
            var endDate = initDate.AddMonths(1);

            var ret = this.DbSet.Where(x => x.CompletedAt >= initDate && x.CompletedAt < endDate);

            if (!string.IsNullOrEmpty(dashboardCode))
            {
                ret =
                    ret.Where(
                        x =>
                        x.Project.Dashboards.Any(
                            c => c.Code.Equals(dashboardCode, StringComparison.InvariantCultureIgnoreCase)));
            }

            ret = top ?
                        ret.GroupBy(x => x.Project.Name).SelectMany(x => x.OrderByDescending(y => y.TotalScore).Take(1)).OrderByDescending(y => y.TotalScore)
                      : ret.GroupBy(x => x.Project.Name).SelectMany(x => x.OrderBy(y => y.TotalScore).Take(1)).OrderBy(y => y.TotalScore);

            if (take.HasValue)
            {
                ret = ret.Take(take.Value);
            }

            return await Task.FromResult(ret.ToList());
        }


    }
}
