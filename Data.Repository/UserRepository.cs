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

    public class UserRepository : EfRepository<User, int>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetTopRankingReviewersAsync(int take)
        {
            return await Task.FromResult(this.DbSet.Distinct().OrderByDescending(x => x.CodeReviews.Select(e => new { e.ExternalId }).Distinct().Count()).Take(take).ToList());
        }

        public async Task<IEnumerable<DataPair<User, int>>> GetMonthTopRankingReviewersAsync(int year, int month, int take)
        {
            var initDate = new DateTime(year, month, 1);
            var endDate = initDate.AddMonths(1);

            var ret =
                this.DbSet.SelectMany(x => x.CodeReviews)
                    .Where(x => x.CreatedAt >= initDate && x.CreatedAt < endDate)
                    .GroupBy(x => x.RequestedBy, y => y.Id)
                    .OrderByDescending(x => x.Count())
                    .Take(take)
                    .Select(x => new DataPair<User, int> { Item1 = x.Key, Item2 = x.Count() })
                    .ToList();
            return await Task.FromResult(ret);
        }

        public async Task<User> GetTopCheckinUserAsync()
        {
            return await Task.FromResult(this.DbSet.OrderByDescending(x => x.ChangeSets.Count()).Include(c => c.ChangeSets).FirstOrDefault());
        }
    }
}
