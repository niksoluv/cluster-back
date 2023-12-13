using Microsoft.Extensions.Logging;
using Entities.Models;
using cluster_back.Repository;
using Contracts;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationContext _context;
        private ILogger _logger;
        private IUserRepository _user;
        private IClusteringResultsRepository _clusteringResultsRepository;

        public IUserRepository User
        {
            get
            {
                _user ??= new UserRepository(_context, _logger);
                return _user;
            }
        }

        public IClusteringResultsRepository ClusteringResults
        {
            get
            {
                _clusteringResultsRepository??=new ClusteringResultsRepository(_context, _logger);
                return _clusteringResultsRepository;
            }
        }
        public RepositoryWrapper(
            ApplicationContext context,
            ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
        }
        public async Task<bool> SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} RemoveFromList method error", typeof(RepositoryWrapper));
                return false;
            }
        }
    }
}
