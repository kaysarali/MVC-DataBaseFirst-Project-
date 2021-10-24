//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kaysar_Mvc_DataBaseFirst_Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Purchase
    {
        public int PurchaseId { get; set; }
        public int VoucherNo { get; set; }
        public System.DateTime PurchaseDate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string ImgPath { get; set; }
        public bool IsActive { get; set; }
        public int ProductModelId { get; set; }
        public int SupplierId { get; set; }
        public int EmployeeId { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual ProductModel ProductModel { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
