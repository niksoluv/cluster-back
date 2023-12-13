using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Entities.Models;
using cluster_back.Repository;

namespace Repository
{
    internal class ClusteringResultsRepository : RepositoryBase<ClusteringResult>, IClusteringResultsRepository
    {
        public ClusteringResultsRepository(ApplicationContext context, ILogger logger)
            : base(context, logger)
        {

        }
        public async Task<User> GetByUsername(string username)
        {
            var result = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}
