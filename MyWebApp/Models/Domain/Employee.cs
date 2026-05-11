namespace  MyWebApp.Models.Domain;

public class Employee
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { set; get; }
    public decimal Salary { set; get; }
    public DateTime DateOfBirth { set; get; }
    public string Department { set; get; }
}