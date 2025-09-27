namespace AspNetCoreIdentityApp.Domain.Entities.Commons
{
    public class BaseEntity : IIsActive, ICreated, IUpdated
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
