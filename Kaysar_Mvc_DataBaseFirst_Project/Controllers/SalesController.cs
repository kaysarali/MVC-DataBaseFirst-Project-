using CrystalDecisions.CrystalReports.Engine;
using Kaysar_Mvc_DataBaseFirst_Project.Models;
using Kaysar_Mvc_DataBaseFirst_Project.Models.ViewModels;
using Kaysar_Mvc_DataBaseFirst_Project.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kaysar_Mvc_DataBaseFirst_Project.Controllers
{
    [AllowAnonymous]
    //[Authorize]
    public class SalesController : Controller
    {
        //int EmpId = 1001;
        
        MvcProjectDBEntities db = new MvcProjectDBEntities();
        SalesRepository Salesrepo = new SalesRepository();
        SalesTempRepository SalesTempRepo = new SalesTempRepository();


        // GET: Sales
        public ActionResult SalesPage()
        {
            List<Product> ProductList = Salesrepo.GetProduct();
            ViewBag.ProductList = ProductList;
            return View();
        }

        public PartialViewResult InsertSale(SalesViewModel obj)
        {
            int EmpId = Convert.ToInt32(Session["EmployeeId"]);
            Customar Objcus = new Customar();
            SalesTempViewModel ObjSalesTemp = new SalesTempViewModel();

            var CustomarRow = db.Customars.Where(model => model.CustMobile == obj.CustMobile).FirstOrDefault();
            if (CustomarRow == null)
            {
                Objcus.CustomarName = obj.CustomarName;
                Objcus.CustMobile = obj.CustMobile;
                Objcus.CustAddress = obj.CustAddress;
                Salesrepo.InsertCustomar(Objcus);
            }


            var Customarinfo = db.Customars.Where(model => model.CustMobile == obj.CustMobile).FirstOrDefault();

            var ProductInfo = db.Products.Where(model => model.ProductId == obj.ProductId).FirstOrDefault();
            var ProductModelInfo = db.ProductModels.Where(model => model.ProductModelId == obj.ProductModelId).FirstOrDefault();
            var EmployeeInfo = db.Employees.Where(model => model.EmployeeId == EmpId).FirstOrDefault();


            int MakeSaleid = SalesTempRepo.CreateSalesTempId();

            try
            {

                ObjSalesTemp.SalesId = MakeSaleid;
                ObjSalesTemp.MemoNo = obj.MemoNo;
                ObjSalesTemp.SalesDate = obj.SalesDate;

                ObjSalesTemp.CustomarId = Customarinfo.CustomarId;
                ObjSalesTemp.CustomarName = Customarinfo.CustomarName;
                ObjSalesTemp.CustMobile = Customarinfo.CustMobile;
                ObjSalesTemp.CustAddress = Customarinfo.CustAddress;

                ObjSalesTemp.ProductModelId = obj.ProductModelId;
                ObjSalesTemp.ProductName = ProductInfo.ProductName;
                ObjSalesTemp.ModelName = ProductModelInfo.ModelName;


                ObjSalesTemp.Quantity = obj.Quantity;
                ObjSalesTemp.UnitPrice = obj.UnitPrice;
                ObjSalesTemp.EmployeeId = EmpId;
                ObjSalesTemp.EmpName = EmployeeInfo.EmpName;

                SalesTempRepo.InsertSaleTemp(ObjSalesTemp);

                TempData["textboxblank"] = "<script>$('#btnShowTable').trigger('click'); $('#ProductId').val(''); $('#Quantity').val(''); $('#UnitPrice').val(''); </script>";

                TempData["Insert"] = "<script> insertMessageShow(); </script>";
            }
            catch (Exception)
            {

            }


            IEnumerable<SalesTempViewModel> list = SalesTempRepo.GetAllSaleTemp();
            return PartialView("_SalesTempView", list);
        }

        public PartialViewResult ShowSaleTempTable()
        {
            IEnumerable<SalesTempViewModel> list = SalesTempRepo.GetAllSaleTemp();
            return PartialView("_SalesTempView", list);
        }



        //#################### Edit Section ####################
        public PartialViewResult Edits(int id)
        {
            SalesTempViewModel obj = SalesTempRepo.SelectSalesTempById(id);

            List<Product> ProductList = Salesrepo.GetProduct();
            ViewBag.ProductList = ProductList;

            ViewBag.EditSectionShow = "Show";
            return PartialView("_SalesTempEdit", obj);
        }

        public ActionResult EditAction(SalesTempViewModel obj)
        {
            int SalesId = obj.SalesId;
            int ProductModelId = obj.ProductModelId;

            if (ProductModelId == 0)
            {
                SalesTempRepo.UpdateSalesTemp(obj);
            }
            else if (ProductModelId >= 0)
            {
                var ProductInfo = db.Products.Where(model => model.ProductId == obj.ProductId).FirstOrDefault();
                var ProductModelInfo = db.ProductModels.Where(model => model.ProductModelId == obj.ProductModelId).FirstOrDefault();

                obj.ProductName = ProductInfo.ProductName;
                obj.ModelName = ProductModelInfo.ModelName;
                SalesTempRepo.UpdateSalesTempWithProduct(obj);
            }

            TempData["Update"] = "<script> updateMessageShow(); </script>";

            IEnumerable<SalesTempViewModel> list = SalesTempRepo.GetAllSaleTemp();
            return PartialView("_SalesTempView", list);
        }
        //#################### Edit Section ####################





        //#################### Delete Section ####################
        public PartialViewResult Delete(int id)
        {
            SalesTempViewModel obj = SalesTempRepo.SelectSalesTempById(id);

            ViewBag.DeleteSectionShow = "Show";
            return PartialView("_SalesTempDelete", obj);
        }

        public ActionResult DeleteAction(SalesTempViewModel obj)
        {
            SalesTempRepo.DeleteSalesTemp(obj);

            TempData["DeleteAction"] = "<script> </script>";

            IEnumerable<SalesTempViewModel> list = SalesTempRepo.GetAllSaleTemp();
            return PartialView("_SalesTempView", list);
        }
        //#################### Delete Section ####################



        public ActionResult InsertTotalSales()
        {
            Sale DObj = new Sale();
            IEnumerable<SalesTempViewModel> list = SalesTempRepo.GetAllSaleTemp();

            foreach (var item in list)
            {
                DObj.MemoNo = item.MemoNo;
                DObj.SalesDate = item.SalesDate;
                DObj.CustomarId = item.CustomarId;
                DObj.ProductModelId = item.ProductModelId;
                DObj.Quantity = item.Quantity;
                DObj.UnitPrice = item.UnitPrice;
                DObj.EmployeeId = item.EmployeeId;
                Salesrepo.InsertSales(DObj);
            }
            int memo = SalesTempRepo.GetMaxValueMemo();
            SalesTempRepo.DeleteAllRowSalesTemp(memo);

            IEnumerable<SalesViewModel> list1 = Salesrepo.GetAllSales();
            return PartialView("_SalesTotalView", list1);
        }

        public ActionResult PageLoadTotalSalesView()
        {
            IEnumerable<SalesViewModel> list1 = Salesrepo.GetAllSales();
            return PartialView("_SalesTotalView", list1);
        }




        // GET: Sales
        public ActionResult SalesView()
        {
            IEnumerable<SalesViewModel> list = Salesrepo.GetAllSales();
            return View(list);
        }

        public JsonResult GetProductModelMethod(int ProductId)
        {
            List<ProductModel> list = Salesrepo.GetProductModel();

            var data = db.ProductModels.Where(c => c.ProductId == ProductId).Select(x => new { ProductModelId = x.ProductModelId, ModelName = x.ModelName }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNewMemoNo(string name)
        {
            SalesViewModel obj = new SalesViewModel();
            try
            {
                int CurrentMemoNo = (from p in db.Sales select p.MemoNo).Max();
                int newMemoNo = CurrentMemoNo + 1;
                SalesTempRepo.DeleteAllRowSalesTemp(newMemoNo);

                return Json(newMemoNo);
            }
            catch (Exception)
            {
                int newMemoNo = 101;
                return Json(newMemoNo);
            }
        }
        public JsonResult GetCustomarinfo(string GetCustMobNum)
        {
            Customar Objcus = new Customar();
            var CustomarRow = db.Customars.Where(model => model.CustMobile == GetCustMobNum).FirstOrDefault();

            if (CustomarRow != null)
            {
                string CustName = CustomarRow.CustomarName;
                string CustAddress = CustomarRow.CustAddress;

                var result = new { CustName, CustAddress };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {

            }
            return Json(null);
        }



        public ActionResult CustomarBillReport()
        {
            ReportDocument rd = new ReportDocument();
            string path = Server.MapPath("~") + "Reports//" + "CustomarBillReport.rpt";

            rd.Load(path);
            IEnumerable<SalesViewModel> SalesList = Salesrepo.GetSalesWhereMaxMamoNo();
            rd.SetDataSource(SalesList);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf", "SalesBill.pdf");
        }
    }
}