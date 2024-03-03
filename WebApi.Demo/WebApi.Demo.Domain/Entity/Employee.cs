namespace WebApi.Demo.Domain
{
    public class Employee : BaseAuditEntity, IEntity<Guid>
    {
        public Guid EmployeeId { get; set; }

        public string? EmployeeCode { get; set; }

        public string? FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public Guid GetId()
        {
            return EmployeeId;
        }

        public void SetId(Guid id)
        {
            EmployeeId = id;
        }
    }
}
