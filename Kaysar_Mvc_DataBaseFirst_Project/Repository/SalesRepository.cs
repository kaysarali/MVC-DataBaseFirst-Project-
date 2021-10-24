using Kaysar_Mvc_DataBaseFirst_Project.Models;
using Kaysar_Mvc_DataBaseFirst_Project.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kaysar_Mvc_DataBaseFirst_Project.Repository
{
    public class SalesRepository
    {
        MvcProjectDBEntities db = new MvcProjectDBEntities();

        public IEnumerable<SalesViewModel> GetAllSales()
        {
            List<SalesViewModel> saleslist = db.Sales.Select(s => new SalesViewModel
            {
                SalesId = s.SalesId,
                MemoNo = s.MemoNo,
                SalesDate = s.SalesDate,
                Quantity = s.Quantity,
                UnitPrice = s.UnitPrice,


                ProductModelId = s.ProductModelId,
                ModelName = s.ProductModel.ModelName,

                ProductId = s.ProductModel.ProductId,
                ProductName = s.ProductModel.Product.ProductName,


                CustomarId = s.CustomarId,
                CustomarName = s.Customar.CustomarName,
                CustAddress = s.Customar.CustAddress,
                CustMobile = s.Customar.CustMobile,
                EmployeeId = s.EmployeeId,
                EmpName = s.Employee.EmpName,

            }).ToList();
            return saleslist;
        }


        public IEnumerable<SalesViewModel> GetSalesWhereMaxMamoNo()
        {
            int MaxValue = (from p in db.Sales select p.MemoNo).Max();
            List<SalesViewModel> saleslist = db.Sales.Where(s => s.MemoNo == MaxValue).Select(s => new SalesViewModel
            {
                SalesId = s.SalesId,
                MemoNo = s.MemoNo,
                SalesDate = s.SalesDate,
                Quantity = s.Quantity,
                UnitPrice = s.UnitPrice,


                ProductModelId = s.ProductModelId,
                ModelName = s.ProductModel.ModelName,

                ProductId = s.ProductModel.ProductId,
                ProductName = s.ProductModel.Product.ProductName,


                CustomarId = s.CustomarId,
                CustomarName = s.Customar.CustomarName,
                CustAddress = s.Customar.CustAddress,
                CustMobile = s.Customar.CustMobile,
                EmployeeId = s.EmployeeId,
                EmpName = s.Employee.EmpName,

            }).ToList();
            return saleslist;
        }


        public List<ProductModel> GetProductModel()
        {
            List<ProductModel> list = db.ProductModels.ToList();
            return list;
        }

        public List<Product> GetProduct()
        {
            List<Product> list = db.Products.ToList();
            return list;
        }



        //----------- Insert Method Two Table ----------------------------
        public void InsertSales(Sale obj)
        {
            db.Sales.Add(obj);
            db.SaveChanges();
        }
        public void InsertCustomar(Customar obj)
        {
            try
            {
                db.Customars.Add(obj);
                db.SaveChanges();
            }
            catch (Exception)
            {
            }
    
        }



    }
}