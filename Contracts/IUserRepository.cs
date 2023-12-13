using Entities.Models;

namespace cluster_back.Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetByUsername(string username);
    }
}
