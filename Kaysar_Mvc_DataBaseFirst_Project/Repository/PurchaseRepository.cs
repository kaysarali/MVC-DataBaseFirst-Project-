using Kaysar_Mvc_DataBaseFirst_Project.Models;
using Kaysar_Mvc_DataBaseFirst_Project.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Kaysar_Mvc_DataBaseFirst_Project.Repository
{
    public class PurchaseRepository
    {
        MvcProjectDBEntities db = new MvcProjectDBEntities();


        public IEnumerable<PurchaseViewModel> GetAllPurchase()
        {
            List<PurchaseViewModel> Purchaselist = db.Purchases.Where(s => s.IsActive == true).Select(s => new PurchaseViewModel
            {
                PurchaseId = s.PurchaseId,
                VoucherNo = s.VoucherNo,
                PurchaseDate = s.PurchaseDate,

                Quantity = s.Quantity,
                TotalPrice = s.TotalPrice,
                ImgPath = s.ImgPath,

                ProductModelId = s.ProductModelId,
                ModelName = s.ProductModel.ModelName,

                ProductId = s.ProductModel.ProductId,
                ProductName = s.ProductModel.Product.ProductName,

                SupplierId = s.SupplierId,
                SupplierName = s.Supplier.SupplierName,

                EmployeeId = s.EmployeeId,
                EmpName = s.Employee.EmpName,

            }).ToList();
            return Purchaselist;
        }

        public IEnumerable<PurchaseViewModel> GetAllPurchaseForReport(string ImagePath)
        {
            List<PurchaseViewModel> Purchaselist = db.Purchases.Where(s => s.IsActive == true).Select(s => new PurchaseViewModel
            {
                PurchaseId = s.PurchaseId,
                VoucherNo = s.VoucherNo,
                PurchaseDate = s.PurchaseDate,

                Quantity = s.Quantity,
                TotalPrice = s.TotalPrice,
                ImageFullPath = ImagePath + s.ImgPath,
                ImgPath = s.ImgPath,

                ProductModelId = s.ProductModelId,
                ModelName = s.ProductModel.ModelName,

                ProductId = s.ProductModel.ProductId,
                ProductName = s.ProductModel.Product.ProductName,

                SupplierId = s.SupplierId,
                SupplierName = s.Supplier.SupplierName,

                EmployeeId = s.EmployeeId,
                EmpName = s.Employee.EmpName,

            }).ToList();
            return Purchaselist;
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

        public List<Supplier> GetSupplier()
        {
            List<Supplier> list = db.Suppliers.ToList();
            return list;
        }


        //============ Get Purchase Id ===============
        public Purchase GetPurchaseId(int id)
        {
            Purchase obj = db.Purchases.SingleOrDefault(e => e.PurchaseId == id);
            return obj;
        }


        //============ Insert ===============

        public void InsertProduct(Product obj)
        {
            db.Products.Add(obj);
            db.SaveChanges();
        }

        public void InsertProductModel(ProductModel obj)
        {
            try
            {
                db.ProductModels.Add(obj);
                db.SaveChanges();
            }
            catch (Exception)
            {

            }

        }

        public void InsertSupplier(Supplier obj)
        {
            db.Suppliers.Add(obj);
            db.SaveChanges();
        }


        public void InsertPurchase(Purchase obj)
        {
            db.Purchases.Add(obj);
            db.SaveChanges();
        }

        //============ Update ===============
        public void UpdateStudent(Purchase updateObj)
        {
            db.Entry(updateObj).State = EntityState.Modified;
            db.SaveChanges();
        }





        //============ Delete ===============
        public void DeletePurchase(Purchase updateObj)
        {
            updateObj.IsActive = false;
            db.Entry(updateObj).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}