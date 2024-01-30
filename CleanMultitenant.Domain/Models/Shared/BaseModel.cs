namespace CleanMultitenant.Domain.Models.Shared
{
    public class BaseModel
    {
        public BaseModel()
        {
            Id = new Guid();
        }

        public Guid Id { get; set; }
    }
}
