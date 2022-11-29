using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using do_an_cuoi_ky.Models;
using System.IO;

namespace do_an_cuoi_ky.Areas.Admin.Controllers
{
  
    
    public class SanPhamController : Controller
    {
        QLBANMOHINHEntities db = new QLBANMOHINHEntities();
        // GET: Admin/SanPham
        public ActionResult Index([Bind(Include = "MaDH,MaKH, NgayDatHang, TriGia, DaGiao, NgayGiaoHang, TenNguoiNhan, DiaChiNhan," +
            " SDTNhan, HTThanhToan, HTGiaoHang")] DONDATHANG dh, HttpPostedFileBase HinhMinhHoa)
        {
            ViewBag.MaKH = new SelectList(db.DONDATHANGs, "MaKH","TenKH", dh.MaKH);
            var cate = db.DONDATHANGs.ToList();
            return View(cate);
        }

        // GET: Admin/SanPham/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/SanPham/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SanPham/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/SanPham/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/SanPham/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/SanPham/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/SanPham/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
