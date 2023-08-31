namespace WebService.DataAccessLayer.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public int? CompanyId { get; set; }
        public int? DepartmentId { get; set; }
        public string? Type { get; set; }
        public string? Number { get; set; }
    }
}
