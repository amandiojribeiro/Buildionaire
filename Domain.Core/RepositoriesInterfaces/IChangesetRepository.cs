namespace Farfetch.Buildionaire.Domain.Core.RepositoriesInterfaces
{
    using Farfetch.Buildionaire.Domain.Model;
    using SharpRepository.Repository;

    public interface IChangesetRepository : IRepository<ChangeSet, int>
    {
        ChangeSet GetLast();
    }
}
