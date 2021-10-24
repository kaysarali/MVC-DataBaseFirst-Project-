using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kaysar_Mvc_DataBaseFirst_Project.Models.ViewModels
{
    public class PurchaseViewModel
    {
        public int PurchaseId { get; set; }
        public int VoucherNo { get; set; }

        public DateTime PurchaseDate { get; set; }
        public string ConvertDate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public string ImageFullPath { get; set; }


        public string ImgPath { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }


        public bool IsActive { get; set; }


        public int ProductModelId { get; set; }
        public string ModelName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }


        public int SupplierId { get; set; }
        public string SupplierName { get; set; }


        public int EmployeeId { get; set; }
        public string EmpName { get; set; }
    }
}