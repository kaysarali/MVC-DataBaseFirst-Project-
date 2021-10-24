using Kaysar_Mvc_DataBaseFirst_Project.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaysar_Mvc_DataBaseFirst_Project.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Name is Required")]
       
        public string EmpName { get; set; }



        [Required(ErrorMessage = "UserName is Required")]
        [StringLength(25, MinimumLength = 4, ErrorMessage = "5 to 15 character")]
        public string EmpUserName { get; set; }



        [Required(ErrorMessage = "Address is Required")]
        public string EmpAddress { get; set; }



        [Required(ErrorMessage = "DOB is Required")]
        [CustomValidationDate]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Invalid Email")]
        public string EmpEmail { get; set; }



        [Required(ErrorMessage = "Password is Required")]
        public string EmpPassword { get; set; }


        [Required(ErrorMessage = "C-Pass is Required")]
        [Compare("EmpPassword", ErrorMessage = "Not Matching")]
        public string RePassword { get; set; }



        public bool IsActive { get; set; }
        public TimeSpan LoginTime { get; set; }
        public TimeSpan LogoutTime { get; set; }
        public int RoletbId { get; set; }
        public string RollName { get; set; }



        public string ImgPath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}