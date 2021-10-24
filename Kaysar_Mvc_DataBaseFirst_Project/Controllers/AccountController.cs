using Kaysar_Mvc_DataBaseFirst_Project.Models;
using Kaysar_Mvc_DataBaseFirst_Project.Models.ViewModels;
using Kaysar_Mvc_DataBaseFirst_Project.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Kaysar_Mvc_DataBaseFirst_Project.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        AccountRepository AccRepo = new AccountRepository();
        Attendance AttObj = new Attendance();
        Employee obj1 = new Employee();


        // GET: Account
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ActionName("Login")]
        public ActionResult PostLogin(EmployeeViewModel obj)
        {
            obj1.EmpUserName = obj.EmpUserName;
            obj1.EmpPassword = obj.EmpPassword;
            var count = AccRepo.LoginSuccess(obj1);
            var SuperAdmin = AccRepo.LoginSuccessSuperAdmin(obj1);

            if (count > 0)
            {
                Employee EmpObj = AccRepo.GetEmployeebyUserName(obj.EmpUserName);
                Session["EmployeeImage"] = EmpObj.ImgPath;
                Session["EmployeeName"] = EmpObj.EmpName;
                Session["EmployeeId"] = EmpObj.EmployeeId;
                

                string RoleName = AccRepo.FindOutRoleName(EmpObj.RoletbId);

                if (RoleName != "Admin" && RoleName != "None")
                {

                    var ord = AccRepo.AttendancesCheckLoingForSameDate(EmpObj.EmployeeId);
                    
                    if (ord != null)
                    {
                        FormsAuthentication.SetAuthCookie(obj.EmpUserName, false);
                        return RedirectToAction("PurchasePage", "Purchase");
                    }
                    else
                    {
                        int EmpId = EmpObj.EmployeeId;

                        TimeSpan LoginTime = TimeSpan.Parse(EmpObj.LoginTime.ToString());
                        TimeSpan CurrentTime = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));

                        AttObj.AttDate = DateTime.Now;

                        if (LoginTime >= CurrentTime)
                        {
                            AttObj.LoginTime = LoginTime;
                            AttObj.Late = TimeSpan.Parse("00:00:00");
                        }
                        else
                        {
                            AttObj.LoginTime = CurrentTime;
                            var LateTime = CurrentTime - LoginTime;
                            AttObj.Late = LateTime;
                        }


                        AttObj.EmployeeId = EmpId;
                        AttObj.LogoutTime = TimeSpan.Parse("00:00:00");
                        AttObj.OverTime = TimeSpan.Parse("00:00:00");
                        AttObj.PartTime = TimeSpan.Parse("00:00:00");


                        AccRepo.AttendancesInsert(AttObj);
                        FormsAuthentication.SetAuthCookie(obj.EmpUserName, false);
                        return RedirectToAction("PurchasePage", "Purchase");
                    }
                }

                else if(RoleName == "Admin")
                {
                    FormsAuthentication.SetAuthCookie(obj.EmpUserName, false);
                    return RedirectToAction("AdminPanelPage", "AdminPanel");
                }
                ViewBag.AdminPermit = "Show";
                ModelState.Clear();
                return View();
            }
            else if (SuperAdmin == 1)
            {
                FormsAuthentication.SetAuthCookie(obj.EmpUserName, false);
                return RedirectToAction("AdminPanelPage", "AdminPanel");
            }
            else
            {
                ViewBag.NotAuthenticatedUser = "Show";
                return View();
            }
        }




        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }



        [HttpPost]
        [ActionName("SignUp")]
        public ActionResult SignUp(EmployeeViewModel obj)
        {
            bool isExists = AccRepo.CheckUserName(obj.EmpUserName);

            if (isExists)
            {
                try
                {
                    obj1.EmpName = obj.EmpName;
                    obj1.EmpUserName = obj.EmpUserName;
                    obj1.EmpAddress = obj.EmpAddress;
                    obj1.DateOfBirth = obj.DateOfBirth;
                    obj1.EmpEmail = obj.EmpEmail;
                    obj1.EmpPassword = obj.EmpPassword;
                    obj1.IsActive = true;
                    obj1.EmpPassword = obj.EmpPassword;
                    obj1.RoletbId = 1;

                    string fileName = Path.GetFileName(obj.ImageFile.FileName);
                    obj1.ImgPath = "/Images/Employee/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/Employee/"), fileName);

                    obj.ImageFile.SaveAs(fileName);
                    AccRepo.InsertEmployee(obj1);
                    ViewBag.Registration = "Show";
                }
                catch (Exception)
                {

                }
                
                return View();

            }
            else
            {
                ViewBag.UserAlreadyExists = "Show";
                //ModelState.AddModelError("", "User Name Exists");
                return View();
            }

        }

        public ActionResult Logout()
        {
            int EmployeeId = Convert.ToInt32(Session["EmployeeId"]);

            Employee EmpObj = AccRepo.GetEmployeeId(EmployeeId);
            var AttendObj = AccRepo.AttendancesCheckLoingForSameDate(EmployeeId);

            if (AttendObj != null)
            {
                TimeSpan LogoutTime = TimeSpan.Parse(EmpObj.LogoutTime.ToString());
                TimeSpan CurrentTime = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));

                if (LogoutTime <= CurrentTime)
                {
                    var OverTime =  CurrentTime - LogoutTime;
                    AttendObj.OverTime = OverTime;
                    AttendObj.LogoutTime = CurrentTime;

                    AccRepo.UpdateAttendances(AttendObj);

                    FormsAuthentication.SignOut();
                    return RedirectToAction("Login", "Account");
                }

                else if (LogoutTime > CurrentTime)
                {
                    
                    var PartTime =  CurrentTime - AttendObj.LoginTime;
                    AttendObj.PartTime = PartTime;
                    AttendObj.LogoutTime = CurrentTime;

                    AccRepo.UpdateAttendances(AttendObj);

                    FormsAuthentication.SignOut();
                    return RedirectToAction("Login", "Account");
                }
                
            }

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }


    }
}