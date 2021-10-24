using Kaysar_Mvc_DataBaseFirst_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kaysar_Mvc_DataBaseFirst_Project
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalFilters.Filters.Add(new AuthorizeAttribute());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            CreateInitialUserAndRole();
        }

        private void CreateInitialUserAndRole()
        {
            using (var dboj = new MvcProjectDBEntities())
            {
                int count = dboj.Employees.Count();
                if (count == 0)
                {
                    Roletb RoleObj = new Roletb();
                    RoleObj.RollName = "None";
                    dboj.Roletbs.Add(RoleObj);
                    dboj.SaveChanges();

                    RoleObj.RollName = "Admin";
                    dboj.Roletbs.Add(RoleObj);
                    dboj.SaveChanges();

                    RoleObj.RollName = "Employee";
                    dboj.Roletbs.Add(RoleObj);
                    dboj.SaveChanges();

                    var RoleRow = dboj.Roletbs.Where(model => model.RollName == "Admin").FirstOrDefault();
                    int AdminRoleId = RoleRow.RoletbId;

                    Employee user = new Employee();
                    user.EmpName = "Super Admin";
                    user.EmpUserName = "kaysar";
                    user.EmpAddress = "Dhaka";
                    user.DateOfBirth = Convert.ToDateTime("08/22/1990");
                    user.EmpEmail = "kaysar@gmail.com";
                    user.EmpPassword = "12345678";
                    //user.ImgPath = "KaysarImage";
                    user.ImgPath = "No Image";
                    user.IsActive = false;
                    user.RoletbId = AdminRoleId;
                    dboj.Employees.Add(user);
                    dboj.SaveChanges();

                    user.EmpName = "Admin";
                    user.EmpUserName = "admin";
                    user.EmpAddress = "Dhaka";
                    user.DateOfBirth = Convert.ToDateTime("08/22/1990");
                    user.EmpEmail = "admin@gmail.com";
                    user.EmpPassword = "12345678";
                    user.ImgPath = "No Image";
                    user.IsActive = true;
                    user.RoletbId = AdminRoleId;
                    dboj.Employees.Add(user);
                    dboj.SaveChanges();
                }
            }
        }
    }
}
