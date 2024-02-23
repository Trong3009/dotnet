namespace WebApi.Demo.Domain
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }

        public string? EmployeeCode { get; set; }

        public string? FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }
        
        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
