using Kaysar_Mvc_DataBaseFirst_Project.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kaysar_Mvc_DataBaseFirst_Project.Repository
{
    public class SalesTempRepository
    {
        static List<SalesTempViewModel> objlist = new List<SalesTempViewModel>();

        public List<SalesTempViewModel> GetAllSaleTemp()
        {
            return objlist;
        }

        public int GetMaxValueMemo()
        {
            try
            {
                int MemoNo = (from p in objlist select p.MemoNo).Max();
                return MemoNo;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void InsertSaleTemp(SalesTempViewModel obj)
        {
            objlist.Add(obj);
        }

        public SalesTempViewModel SelectSalesTempById(int id)
        {
            SalesTempViewModel obj = objlist.Find(x => x.SalesId == id);
            return obj;
        }


        public int CreateSalesTempId()
        {
            try
            {
                int MakeSalesId = 1 + (from p in objlist select p.SalesId).Max();
                return MakeSalesId;
            }
            catch (Exception)
            {
                int MakeSalesId = 1;
                return MakeSalesId;
            }

        }

        public void UpdateSalesTemp(SalesTempViewModel obj)
        {
            SalesTempViewModel ObjUpdate = objlist.Find(x => x.SalesId == obj.SalesId);
            ObjUpdate.SalesId = obj.SalesId;
            ObjUpdate.Quantity = obj.Quantity;
            ObjUpdate.UnitPrice = obj.UnitPrice;
        }


        public void UpdateSalesTempWithProduct(SalesTempViewModel obj)
        {
            SalesTempViewModel ObjUpdate = objlist.Find(x => x.SalesId == obj.SalesId);

            ObjUpdate.SalesId = obj.SalesId;
            ObjUpdate.Quantity = obj.Quantity;
            ObjUpdate.UnitPrice = obj.UnitPrice;
            ObjUpdate.ProductName = obj.ProductName;
            ObjUpdate.ProductModelId = obj.ProductModelId;
            ObjUpdate.ModelName = obj.ModelName;
        }

        public void DeleteSalesTemp(SalesTempViewModel obj)
        {
            SalesTempViewModel ObjDelete = objlist.Find(x => x.SalesId == obj.SalesId);
            objlist.Remove(ObjDelete);
        }



        public void DeleteAllRowSalesTemp(int MemoNo)
        {
            objlist.RemoveAll(r => r.MemoNo == MemoNo);
        }


    }
}