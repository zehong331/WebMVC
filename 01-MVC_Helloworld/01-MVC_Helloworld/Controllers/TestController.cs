using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _01_MVC_Helloworld.ViewModels;
using _01_MVC_Helloworld.Models;

namespace _01_MVC_Helloworld.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public string GetString()
        {
            return "Hello World is old now. It’s time for wassup bro ;)";
        }
        //创建新的Action方法  
        //注：若将View放在Shared文件夹中则所有的Controller都可以使用
        //ActionResult是抽象类，而ViewResult是ActionResult的多级孩子节点，多级是因为ViewResult是ViewResultBase的子类，而ViewResultBase是ActionResult的孩子节点。
        public ActionResult GetView()
        {
            //此处作为测试直接编写数据，实际应从数据库或web服务器获取
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
            List<Employee> employees = empBal.GetEmployees();
            List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();
            foreach (Employee emp in employees)
	        {
		        EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
                empViewModel.Salary = emp.Salary.ToString("C");
                if (emp.Salary > 15000)
                {
                    empViewModel.SalaryColor = "yellow";
               }
                else
                {
                    empViewModel.SalaryColor = "green";
                }
                empViewModels.Add(empViewModel);
	        }
            employeeListViewModel.Employees = empViewModels;
            employeeListViewModel.UserName = "Admin";
            //传递数据的三种方法：
            //ViewData["Employee"] = emp;
            //ViewBag.Employee = emp;
            //以上两种需要在传递时转换类型

            //EmployeeViewModel vmEmp = new EmployeeViewModel();
            //vmEmp.EmployeeName = emp.FirstName + "  " + emp.LastName;
            //vmEmp.Salary = emp.Salary.ToString("C");
            //if (emp.Salary > 15000)
            //{
            //    vmEmp.SalaryColor = "yellow";
            //}
            //else
            //{
            //    vmEmp.SalaryColor = "green";
            //}
            return View("Myview",employeeListViewModel);
            //View函数的功能是什么？
            //1.创建 ViewResult 对象将会渲染成视图来给用户反馈
            //2.ViewResult 创建了ViewPageActivator 对象
            //3.ViewResult 选择了正确的ViewEngine，并且会给ViewEngine的构造函数传ViewPageActivator对象的参数
            //4.ViewEngine 创建View类的对象
            //5.ViewEngine 调用View的RenderView 方法。

        }
        
    }
}