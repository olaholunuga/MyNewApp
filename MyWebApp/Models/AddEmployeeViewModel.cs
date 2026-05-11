namespace MyWebApp.Models;

public class AddEmployeeViewModel
{
    public string Name { get; set; }
    public string Email { set; get; }
    public decimal Salary { set; get; }
    public DateTime DateOfBirth { set; get; }
    public string Department { set; get; }
}