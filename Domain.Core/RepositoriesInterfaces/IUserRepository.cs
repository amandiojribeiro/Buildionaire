namespace Farfetch.Buildionaire.Domain.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Farfetch.Buildionaire.Domain.Model;

    using SharpRepository.Repository;

    public interface IUserRepository : IRepository<User, int>
    {
        Task<IEnumerable<User>> GetTopRankingReviewersAsync(int take);

        Task<IEnumerable<DataPair<User, int>>> GetMonthTopRankingReviewersAsync(int year, int month, int take);

        Task<User> GetTopCheckinUserAsync();
    }
}
