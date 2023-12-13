namespace Entities.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
