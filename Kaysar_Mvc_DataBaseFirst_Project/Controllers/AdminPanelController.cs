using CrystalDecisions.CrystalReports.Engine;
using Kaysar_Mvc_DataBaseFirst_Project.Models;
using Kaysar_Mvc_DataBaseFirst_Project.Models.ViewModels;
using Kaysar_Mvc_DataBaseFirst_Project.Repository;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kaysar_Mvc_DataBaseFirst_Project.Controllers
{

    //[AllowAnonymous]
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        AccountRepository AccRepo = new AccountRepository();

        // GET: AdminPanel
        public ActionResult AdminPanelPage()
        {
            List<Roletb> Rolelist = AccRepo.GetRoles();
            ViewBag.DeptList = new SelectList(Rolelist, "RoletbId", "RollName");

            return View();
        }


        // Employee Table =====================================================
        public PartialViewResult EmployeeTable(string txtboxSearch, string EmployeeCategory, string CurrentFilter, string SortOrder, int? page)
        {
            if (txtboxSearch != null)
            {
                page = 1;
            }
            else
            {
                txtboxSearch = CurrentFilter;
            }

            ViewBag.CurrentFilter = txtboxSearch;
            ViewBag.SortNameParam = string.IsNullOrEmpty(SortOrder) ? "name_desc" : "";


            IEnumerable<EmployeeViewModel> Emplist = AccRepo.GetAllEmployee();

            if (!string.IsNullOrEmpty(EmployeeCategory))
            {
                if (!string.IsNullOrEmpty(txtboxSearch))
                {
                    if (EmployeeCategory == "Employee ID")
                    {
                        try
                        {
                            Emplist = Emplist.Where(e => e.EmployeeId.Equals(Convert.ToInt32(txtboxSearch))).ToList();
                        }
                        catch (Exception)
                        {
                        }
                    }

                    else if (EmployeeCategory == "Employee Name")
                    {
                        try
                        {
                            Emplist = Emplist.Where(e => e.EmpName.ToLower().Contains(txtboxSearch.ToLower())).ToList();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

            }

            switch (SortOrder)
            {
                case "name_desc":
                    Emplist = Emplist.OrderBy(e => e.EmployeeId).ToList();
                    break;
                default:
                    Emplist = Emplist.OrderByDescending(e => e.EmployeeId).ToList();
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return PartialView("_EmployeeTableView", Emplist.ToPagedList(pageNumber, pageSize));
        }


        // Attendance Table =====================================================
        public PartialViewResult AttendanceTable(string FirstDate, string SecondDate, string CurrentFilter, string SortOrder, int? page)
        {
            if (FirstDate == null)
            {
                FirstDate = "";
            }

            if (SecondDate == null)
            {
                SecondDate = "";
            }

            if (FirstDate != "")
            {
                page = 1;
            }
            else
            {
                FirstDate = CurrentFilter;
            }

            ViewBag.CurrentFilterAttend = FirstDate;
            ViewBag.SortNameParamAttend = string.IsNullOrEmpty(SortOrder) ? "name_desc" : "";


            IEnumerable<AttendanceViewModel> Attlist = AccRepo.GetAllAttendance();

            if (FirstDate != "" && SecondDate != "")
            {
                DateTime FDate = Convert.ToDateTime(FirstDate);
                DateTime SDate = Convert.ToDateTime(SecondDate);
                Attlist = Attlist.Where(e => Convert.ToDateTime(e.AttDate) >= FDate && Convert.ToDateTime(e.AttDate) <= SDate).ToList();
            }

            switch (SortOrder)
            {
                case "name_desc":
                    Attlist = Attlist.OrderBy(e => e.AttendanceId).ToList();
                    break;
                default:
                    Attlist = Attlist.OrderByDescending(e => e.AttendanceId).ToList();
                    break;
            }

            TempData["listAttEmp"] = Attlist;

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return PartialView("_AttendanceTableView", Attlist.ToPagedList(pageNumber, pageSize));
        }


        public JsonResult GetEmployeeById(int EmployeeId)
        {
            Employee Emp = AccRepo.GetEmployeeId(EmployeeId);

            EmployeeViewModel obj = new EmployeeViewModel();

            obj.EmployeeId = Emp.EmployeeId;
            obj.EmpName = Emp.EmpName;
            obj.EmpUserName = Emp.EmpUserName;
            obj.EmpAddress = Emp.EmpAddress;
            obj.DateOfBirth = Emp.DateOfBirth;

            TempData["DateOfBirth"] = Emp.DateOfBirth;
            obj.EmpEmail = Emp.EmpEmail;
            obj.EmpPassword = Emp.EmpPassword;
            obj.ImgPath = Emp.ImgPath;
            TempData["Image"] = Emp.ImgPath;
            obj.LoginTime = Emp.LoginTime;
            obj.LogoutTime = Emp.LogoutTime;
            obj.RoletbId = Emp.RoletbId;
            obj.RollName = Emp.Roletb.RollName;
            string value = "";

            value = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }



        // Insert and update Method Open =====================================================
        public JsonResult InsertEmployee(EmployeeViewModel obj)
        {
            Employee obj1 = new Employee();
            if (obj.EmployeeId == 0)
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
                    obj1.RoletbId = obj.RoletbId;
                    obj1.LoginTime = obj.LoginTime;
                    obj1.LogoutTime = obj.LogoutTime;

                    string fileName = Path.GetFileName(obj.ImageFile.FileName);
                    obj1.ImgPath = "/Images/Employee/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/Employee/"), fileName);

                    obj.ImageFile.SaveAs(fileName);
                    AccRepo.InsertEmployee(obj1);
                }
                catch (Exception)
                {

                }

            }
            /// Update ================

            else if (obj.EmployeeId >= 0)
            {


                obj1.EmployeeId = obj.EmployeeId;
                obj1.EmpName = obj.EmpName;
                obj1.EmpUserName = obj.EmpUserName;
                obj1.EmpAddress = obj.EmpAddress;
                
                obj1.EmpEmail = obj.EmpEmail;
                obj1.EmpPassword = obj.EmpPassword;
                obj1.IsActive = true;
                obj1.EmpPassword = obj.EmpPassword;
                obj1.RoletbId = obj.RoletbId;
                obj1.LoginTime = obj.LoginTime;
                obj1.LogoutTime = obj.LogoutTime;
                obj1.DateOfBirth = obj.DateOfBirth;

                if (obj.ImageFile == null)
                {
                    obj1.ImgPath = TempData["Image"].ToString();
                }
                else
                {
                    string fileName = Path.GetFileName(obj.ImageFile.FileName);
                    obj1.ImgPath = "/Images/Employee/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/Employee/"), fileName);
                    obj.ImageFile.SaveAs(fileName);
                    string ImagePath = Request.MapPath(TempData["Image"].ToString());
                    if (System.IO.File.Exists(ImagePath))
                    {
                        System.IO.File.Delete(ImagePath);
                    }
                }

                AccRepo.UpdateEmployee(obj1);
            }

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { result = "Purchase Insert Successfully!!!" }
            };
        }


        // Delete Section Method Open =====================================================
        public JsonResult DeleteEmployee(int EmployeeId)
        {
            bool result = true;
            Employee Emp = AccRepo.GetEmployeeId(EmployeeId);
            AccRepo.DeleteEmployee(Emp);
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public ActionResult ExportReport()
        {
            ReportDocument rd = new ReportDocument();
            string path = Server.MapPath("~") + "Reports//" + "EmployeeAttendanceReport.rpt";
            rd.Load(path);

            var list = TempData["listAttEmp"];

            rd.SetDataSource(list);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Purchase.pdf");
        }

    }
}