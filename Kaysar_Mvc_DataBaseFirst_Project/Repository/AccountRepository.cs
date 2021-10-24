using Kaysar_Mvc_DataBaseFirst_Project.Models;
using Kaysar_Mvc_DataBaseFirst_Project.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Kaysar_Mvc_DataBaseFirst_Project.Repository
{
    public class AccountRepository
    {
        MvcProjectDBEntities db = new MvcProjectDBEntities();

        public bool CheckUserName(string EmpUserName)
        {
            return !db.Employees.Any(u => u.EmpUserName == EmpUserName);
        }

        public IEnumerable<EmployeeViewModel> GetAllEmployee()
        {
            List<EmployeeViewModel> Emplist = db.Employees.Where(s => s.IsActive == true).Select(s => new EmployeeViewModel
            {
                EmployeeId = s.EmployeeId,
                EmpName = s.EmpName,
                EmpUserName = s.EmpUserName,
                EmpAddress = s.EmpAddress,
                DateOfBirth = s.DateOfBirth,
                EmpEmail = s.EmpEmail,
                EmpPassword = s.EmpPassword,
                LoginTime = s.LoginTime,
                LogoutTime = s.LogoutTime,
                RoletbId = s.RoletbId,
                RollName = s.Roletb.RollName,
                ImgPath = s.ImgPath

            }).ToList();
            return Emplist;
        }

        public IEnumerable<AttendanceViewModel> GetAllAttendance()
        {
            List<AttendanceViewModel> Attlist = db.Attendances.Select(s => new AttendanceViewModel
            {
                AttendanceId = s.AttendanceId,
                AttDate = s.AttDate,
                LoginTime = s.LoginTime,

                LogoutTime = s.LogoutTime,
                Late = s.Late,
                OverTime = s.OverTime,
                PartTime = s.PartTime,
                EmployeeId = s.EmployeeId,
                EmpName = s.Employee.EmpName,


            }).ToList();
            return Attlist;
        }

        public List<Roletb> GetRoles()
        {
            List<Roletb> list = db.Roletbs.ToList();
            return list;
        }

        public void InsertEmployee(Employee obj)
        {
            db.Employees.Add(obj);
            db.SaveChanges();
        }

        public Employee GetEmployeeId(int id)
        {
            Employee obj = db.Employees.SingleOrDefault(e => e.EmployeeId == id);
            return obj;
        }

        public void UpdateEmployee(Employee updateObj)
        {
            db.Entry(updateObj).State = EntityState.Modified;
            db.SaveChanges();
        }

        public int LoginSuccess(Employee obj)
        {
            int count = db.Employees.Where(u => u.EmpUserName == obj.EmpUserName && u.EmpPassword == obj.EmpPassword && u.IsActive == true).Count();
            return count;
        }

        public int LoginSuccessSuperAdmin(Employee obj)
        {
            int count = db.Employees.Where(u => u.EmpUserName == obj.EmpUserName && u.EmpPassword == obj.EmpPassword && u.IsActive == false && u.ImgPath == "No Image").Count();
            return count;
        }

        public Employee GetEmployeebyUserName(string EmpUserName)
        {
            Employee obj = db.Employees.SingleOrDefault(e => e.EmpUserName == EmpUserName);
            return obj;
        }

        public string FindOutRoleName(int RoletbId)
        {
            string RoleName = (from r in db.Roletbs where r.RoletbId == RoletbId select r.RollName).First();
            return RoleName;
        }

        public Attendance AttendancesCheckLoingForSameDate(int EmployeeId)
        {
            DateTime CurrentDate = DateTime.Now;
            var ord = db.Attendances.FirstOrDefault(t => (t.EmployeeId == EmployeeId && t.AttDate.Day == CurrentDate.Day && t.AttDate.Month == CurrentDate.Month && t.AttDate.Year == CurrentDate.Year));
            return ord;
        }

        public void AttendancesInsert(Attendance AttObj)
        {
            db.Attendances.Add(AttObj);
            db.SaveChanges();
        }

        public void UpdateAttendances(Attendance updateObj)
        {
            db.Entry(updateObj).State = EntityState.Modified;
            db.SaveChanges();
        }


        //============ Delete ===============
        public void DeleteEmployee(Employee updateObj)
        {
            updateObj.IsActive = false;
            db.Entry(updateObj).State = EntityState.Modified;
            db.SaveChanges();
        }

    }
}