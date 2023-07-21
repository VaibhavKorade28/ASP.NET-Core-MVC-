using Microsoft.AspNetCore.Mvc;
using BL;
using DBConnection;
using System.Data.Common;

namespace HRApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private ILogger<EmployeeController> _logger;
        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }
        
       
        public IActionResult Register() {
            return View();
        }
        [HttpPost]
        public IActionResult Register(string name,double sal,string email,string dept,string password)
        {
            Employee newEmp = new Employee();
            newEmp.Name = name;
            newEmp.Salary = sal;
            newEmp.Email = email;
            newEmp.Dept = dept;
            newEmp.Password = password;
            bool status = DBManager.AddEmployee(newEmp);
            if(status == true )
            {
                return RedirectToAction("login");
            }
            else
            {
                return RedirectToAction("register");
            }
        }
        public IActionResult List()
        {
            List<Employee> employees = DBManager.GetAll();
            this.ViewData["emps"] = employees;
            return View();
        }
        public IActionResult Login() {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email,string password)
        {
            Employee emp = DBManager.GetEmployee(email, password);
            if(emp!=null)
            {
                return RedirectToAction("list");
            }
            return RedirectToAction("login");
        }
        public IActionResult Delete(int id)
        {
            DBManager.DeleteById(id);
            return RedirectToAction("list");
            
        }
        public IActionResult Update(int id) 
        { 
            Employee emp=DBManager.GetById(id);
            Console.WriteLine(emp.Name);
            this.ViewBag.newEmp = emp;
            return View();
            
        }
        [HttpPost]
        public IActionResult Update(int id,string name, double sal, string email, string dept, string password)
        {
            Console.WriteLine(password);
            bool status = DBManager.UpdateEmpDetails(id,name,sal,email,dept,password);
            Console.WriteLine(status);
            if(status == true )
            {
                return RedirectToAction("list");
            }
            else
            {
                return RedirectToAction("login");
            }
        }
    }
}
