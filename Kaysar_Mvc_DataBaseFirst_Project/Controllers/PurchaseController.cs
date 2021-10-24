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
    [Authorize]
    public class PurchaseController : Controller
    {
        //int EmpId = 1001;
        

        private readonly MvcProjectDBEntities db = new MvcProjectDBEntities();

        PurchaseRepository PurchRepo = new PurchaseRepository();

        // GET: Purchase
        public ActionResult PurchasePage()
        {
            
            return View();
        }

        public PartialViewResult SearchCustomers(string txtboxSearch, string PurchaseCategory, string CurrentFilter, string SortOrder, int? page)
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


            IEnumerable<PurchaseViewModel> Purchlist = PurchRepo.GetAllPurchase();

            if (!string.IsNullOrEmpty(PurchaseCategory))
            {
                if (!string.IsNullOrEmpty(txtboxSearch))
                {
                    if (PurchaseCategory == "Purchase ID")
                    {
                        try
                        {
                            Purchlist = Purchlist.Where(e => e.PurchaseId.Equals(Convert.ToInt32(txtboxSearch))).ToList();
                        }
                        catch (Exception)
                        {
                        }
                    }

                    else if (PurchaseCategory == "Voucher No")
                    {
                        try
                        {
                            Purchlist = Purchlist.Where(e => e.VoucherNo.Equals(Convert.ToInt32(txtboxSearch))).ToList();
                        }
                        catch (Exception)
                        {
                        }
                    }

                    else if (PurchaseCategory == "Product Name")
                    {
                        Purchlist = Purchlist.Where(e => e.ProductName.ToLower().Contains(txtboxSearch.ToLower())).ToList();
                    }
                }

            }

            switch (SortOrder)
            {
                case "name_desc":
                    Purchlist = Purchlist.OrderBy(e => e.PurchaseId).ToList();
                    break;
                default:
                    Purchlist = Purchlist.OrderByDescending(e => e.PurchaseId).ToList();
                    break;
            }

            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return PartialView("_PurchaseTableView", Purchlist.ToPagedList(pageNumber, pageSize));
        }

        // Get Section Method Open =====================================================

        public JsonResult GetPurchaseRawMethod(int PurchaseId)
        {
            Purchase obj1 = PurchRepo.GetPurchaseId(PurchaseId);
            PurchaseViewModel obj = new PurchaseViewModel();

            obj.PurchaseId = obj1.PurchaseId;
            obj.VoucherNo = obj1.VoucherNo;
            obj.PurchaseDate = obj1.PurchaseDate;
            obj.ConvertDate = obj.PurchaseDate.ToShortDateString();

            obj.ProductId = obj1.ProductModel.ProductId;
            obj.ProductName = obj1.ProductModel.Product.ProductName;

            obj.ProductModelId = obj1.ProductModelId;
            TempData["ProductModelId"] = obj1.ProductModelId;
            obj.ModelName = obj1.ProductModel.ModelName;

            obj.SupplierId = obj1.SupplierId;
            TempData["SupplierId"] = obj1.SupplierId;
            obj.SupplierName = obj1.Supplier.SupplierName;

            obj.Quantity = obj1.Quantity;
            obj.TotalPrice = obj1.TotalPrice;
            obj.ImgPath = obj1.ImgPath;
            TempData["Image"] = obj1.ImgPath;
            obj.EmployeeId = obj1.EmployeeId;
            obj.EmpName = obj1.Employee.EmpName;


            string value = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });


            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPurchaseTableData()
        {
            IEnumerable<PurchaseViewModel> Purchlist = PurchRepo.GetAllPurchase();
            return Json(Purchlist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductModelMethod(int ProductId)
        {
            var data = db.ProductModels.Where(c => c.ProductId == ProductId).Select(x => new { ProductModelId = x.ProductModelId, ModelName = x.ModelName }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductList()
        {
            var data = db.Products.Select(x => new { ProductId = x.ProductId, ProductName = x.ProductName }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSupplierList()
        {
            var data = db.Suppliers.Select(x => new { SupplierId = x.SupplierId, SupplierName = x.SupplierName }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        // Get Section Method End ======================================================



        // Insert Section Method Open =====================================================
        public JsonResult InsertProduct(string name)
        {
            Product obj = new Product();
            obj.ProductName = name;
            PurchRepo.InsertProduct(obj);
            var data = db.Products.Select(x => new { ProductId = x.ProductId, ProductName = x.ProductName }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertProductModel(string para1, string para2)
        {
            ProductModel obj = new ProductModel();
            obj.ProductId = Convert.ToInt32(para1);
            obj.ModelName = para2;
            PurchRepo.InsertProductModel(obj);
            var data = db.ProductModels.Where(c => c.ProductId == obj.ProductId).Select(x => new { ProductModelId = x.ProductModelId, ModelName = x.ModelName }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertSupplier(string name)
        {
            Supplier obj = new Supplier();
            obj.SupplierName = name;
            PurchRepo.InsertSupplier(obj);
            var data = db.Suppliers.Select(x => new { SupplierId = x.SupplierId, SupplierName = x.SupplierName }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }



        public JsonResult InsertPurchase(PurchaseViewModel obj)
        {
            Purchase obj1 = new Purchase();
            if (obj.PurchaseId == 0)
            {

                if (obj.VoucherNo != 0 && obj.ProductModelId != 0 && obj.SupplierId != 0 && obj.Quantity != 0 && obj.TotalPrice != 0 && obj.ImageFile != null)
                {
                    obj1.VoucherNo = obj.VoucherNo;
                    obj1.PurchaseDate = obj.PurchaseDate;
                    obj1.ProductModelId = obj.ProductModelId;
                    obj1.SupplierId = obj.SupplierId;
                    obj1.Quantity = obj.Quantity;
                    obj1.TotalPrice = obj.TotalPrice;
                    obj1.EmployeeId = Convert.ToInt32(Session["EmployeeId"]);
                    obj1.IsActive = true;

                    string fileName = Path.GetFileName(obj.ImageFile.FileName);
                    obj1.ImgPath = "/Images/Product/" + fileName;

                    fileName = Path.Combine(Server.MapPath("~/Images/Product/"), fileName);
                    obj.ImageFile.SaveAs(fileName);
                    

                    PurchRepo.InsertPurchase(obj1);

                    return new JsonResult()
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = new { result = "Insert" }
                    };
                }



            }
            /// Update ================
            else if (obj.PurchaseId >= 0)
            {


                obj1.PurchaseId = obj.PurchaseId;
                obj1.VoucherNo = obj.VoucherNo;
                obj1.PurchaseDate = obj.PurchaseDate;

                if (obj.ProductModelId == 0)
                {
                    obj1.ProductModelId = Convert.ToInt16(TempData["ProductModelId"]);
                }
                else
                {
                    obj1.ProductModelId = obj.ProductModelId;
                }
                if (obj.SupplierId == 0)
                {
                    obj1.SupplierId = Convert.ToInt16(TempData["SupplierId"]);
                }
                else
                {
                    obj1.SupplierId = obj.SupplierId;
                }

                obj1.Quantity = obj.Quantity;
                obj1.TotalPrice = obj.TotalPrice;
                obj1.EmployeeId = obj.EmployeeId;
                obj1.IsActive = true;


                if (obj.ImageFile == null)
                {
                    obj1.ImgPath = TempData["Image"].ToString();
                }
                else
                {
                    string fileName = Path.GetFileName(obj.ImageFile.FileName);
                    obj1.ImgPath = "/Images/Product/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/Product/"), fileName);
                    obj.ImageFile.SaveAs(fileName);
                    string ImagePath = Request.MapPath(TempData["Image"].ToString());
                    if (System.IO.File.Exists(ImagePath))
                    {
                        System.IO.File.Delete(ImagePath);
                    }
                }

                PurchRepo.UpdateStudent(obj1);

                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { result = "Update" }
                };
            }
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { result = "NotInsert" }
            };
        }
        // Insert Section Method End =====================================================


        // Delete Section Method Open =====================================================
        public JsonResult DeletePurchase(int PurchaseId)
        {
            bool result = true;
            Purchase obj2 = PurchRepo.GetPurchaseId(PurchaseId);
            PurchRepo.DeletePurchase(obj2);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // Delete Section Method End =====================================================


        public ActionResult ExportReport()
        {
            ReportDocument rd = new ReportDocument();
            string path = Server.MapPath("~") + "Reports//" + "PurchaseTotalReport.rpt";
            string ImagePath = Server.MapPath("~") + "//";
            rd.Load(path);
            IEnumerable<PurchaseViewModel> Purchlist = PurchRepo.GetAllPurchaseForReport(ImagePath);
            rd.SetDataSource(Purchlist);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "Purchase.pdf");
           
        }



    }
}