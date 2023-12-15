using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Entities.Models
{
    public class ClusteringResult : BaseModel
    {
        public string FileName { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
