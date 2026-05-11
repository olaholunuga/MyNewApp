using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Data;
using MyWebApp.Models;
using MyWebApp.Models.Domain;

namespace MyWebApp.Controllers;

public class EmployeesController : Controller
{
    public readonly MvcDemoDbContext mvcDemoDbContext;
    public EmployeesController(MvcDemoDbContext mvcDemoDbContext)
    {
        this.mvcDemoDbContext = mvcDemoDbContext;
    }

    [HttpGet]
    async public Task<IActionResult> Index()
    {
        var employees = await mvcDemoDbContext.Employees.ToListAsync();
        return View(employees);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    async public Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
    {
        var employee = new Employee()
        {
            Id = Guid.NewGuid(),
            Name = addEmployeeRequest.Name,
            Email = addEmployeeRequest.Email,
            Salary = addEmployeeRequest.Salary,
            DateOfBirth = addEmployeeRequest.DateOfBirth.ToUniversalTime(),
            Department = addEmployeeRequest.Department
        };
        await mvcDemoDbContext.Employees.AddAsync(employee);
        await mvcDemoDbContext.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    async public Task<IActionResult> View(Guid id)
    {
        var employee = await mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

        if (employee != null)
        {
            var viewModel = new UpdateEmployeeViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth,
                Department = employee.Department
            };
            return await Task.Run(() => View("View", viewModel));
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    async public Task<IActionResult> View(UpdateEmployeeViewModel model)
    {
        var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);

        if (employee != null)
        {
            employee.Email = model.Email;
            employee.DateOfBirth = model.DateOfBirth.ToUniversalTime();
            employee.Name = model.Name;
            employee.Salary = model.Salary;
            employee.Department = model.Department;

            await mvcDemoDbContext.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }
    [HttpPost]
    async public Task<IActionResult> Delete(UpdateEmployeeViewModel model)
    {
        var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);

        if (employee != null)
        {
            mvcDemoDbContext.Employees.Remove(employee);
            await mvcDemoDbContext.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }
}