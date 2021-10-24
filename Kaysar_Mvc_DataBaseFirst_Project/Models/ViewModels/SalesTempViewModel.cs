using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kaysar_Mvc_DataBaseFirst_Project.Models.ViewModels
{
    public class SalesTempViewModel
    {
        public int SalesId { get; set; }
        public int MemoNo { get; set; }
        public DateTime SalesDate { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }


        public int ProductModelId { get; set; }
        public string ModelName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }


        public int CustomarId { get; set; }
        public string CustomarName { get; set; }
        public string CustAddress { get; set; }
        public string CustMobile { get; set; }


        public int EmployeeId { get; set; }
        public string EmpName { get; set; }
    }
}