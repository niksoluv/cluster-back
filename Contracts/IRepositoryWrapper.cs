using cluster_back.Repository;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IClusteringResultsRepository ClusteringResults { get; }
        Task<bool> SaveAsync();
    }
}
