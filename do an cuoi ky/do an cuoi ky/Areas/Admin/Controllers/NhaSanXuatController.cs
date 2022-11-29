using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using do_an_cuoi_ky.Models;
using System.IO;
using PagedList;

namespace do_an_cuoi_ky.Areas.Admin.Controllers
{
    public class NhaSanXuatController : Controller
    {
        // GET: Admin/NhaSanXuat
        QLBANMOHINHEntities database = new QLBANMOHINHEntities();
        public ActionResult Index(int? page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;
            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo BookID mới có thể phân trang.
            var books = database.MOHINHs.OrderBy(b => b.MaMH);
            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 10;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            return View(books.ToPagedList(pageNumber, pageSize));
            //var categories = database.MOHINHs.ToList();
            //return View(categories);
        }

        // GET: Admin/NhaSanXuat/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.MaCD = new SelectList(database.MOHINHs, "MaCD");
            ViewBag.MaNSX = new SelectList(database.MOHINHs, "MaNSX");
            var category = database.MOHINHs.Where(c => c.MaMH == id).FirstOrDefault();
            return View(category);
        }

        // GET: Admin/NhaSanXuat/Create
        public ActionResult Create()
        {
            ViewBag.MaCD = new SelectList(database.MOHINHs, "MaCD", "MaNSX");
            ViewBag.MaNSX = new SelectList(database.MOHINHs, "MaCD", "MaNSX");
            return View();
        }

        // POST: Admin/NhaSanXuat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "MaMh,TenMH, Donvitinh, MoTa, DonGia, HinhMinhHoa, MaCD, MaNSX," +
            " NgayCapNhat, SoLuongBan, SoLuotXem")] MOHINH mohinh, HttpPostedFileBase HinhMinhHoa)
        {
            if (ModelState.IsValid)
            {
                if (HinhMinhHoa != null)
                { //Lấy tên file của hình được up lên

                    var fileName = Path.GetFileName(HinhMinhHoa.FileName);
                    //Tạo đường dẫn tới file
                    var path = Path.Combine(Server.MapPath("/Asset/img"), fileName);
                    //Lưu tên
                    mohinh.HinhMinhHoa = fileName;
                    //Save vào Images Folder
                    HinhMinhHoa.SaveAs(path);
                }
                database.MOHINHs.Add(mohinh);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCD = new SelectList(database.MOHINHs, "MaCD", "MaNSX", mohinh.MaCD);
            ViewBag.MaNSX = new SelectList(database.MOHINHs, "MaCD", "MaNSX", mohinh.MaNSX);
            return View(mohinh);

        }

        // GET: Admin/NhaSanXuat/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = database.MOHINHs.Where(c => c.MaMH == id).FirstOrDefault();
            ViewBag.MaCD = new SelectList(database.MOHINHs, "MaCD");
            ViewBag.MaNSX = new SelectList(database.MOHINHs, "MaNSX");
            return View(category);
        }

        // POST: Admin/NhaSanXuat/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection, MOHINH category, [Bind(Include = "MaMh,TenMH, Donvitinh, MoTa,DonGia , HinhMinhHoa, MaCD, MaNSX," +
            " NgayCapNhat, SoLuongBan, SoLuotXem")] MOHINH mohinh, HttpPostedFileBase HinhMinhHoa)
        {
            if (ModelState.IsValid)
            {
                var mohinhDB = database.MOHINHs.FirstOrDefault(p => p.MaMH == mohinh.MaMH);
                if (mohinhDB != null)
                {
                    mohinhDB.TenMH = mohinh.TenMH;
                    mohinhDB.Donvitinh = mohinh.Donvitinh;
                    mohinhDB.MoTa = mohinh.MoTa;
                    mohinhDB.DonGia = mohinh.DonGia;

                    mohinhDB.NgayCapNhat = mohinh.NgayCapNhat;
                    mohinhDB.SoLuongBan = mohinh.SoLuongBan;
                    mohinhDB.SoLuotXem = mohinh.SoLuotXem;
                    if (HinhMinhHoa != null)
                    {
                        //Lấy tên file của hình được up lên
                        var fileName = Path.GetFileName(HinhMinhHoa.FileName);
                        //Tạo đường dẫn tới file
                        var path = Path.Combine(Server.MapPath("/Asset/img"), fileName);
                        //Lưu tên
                        mohinhDB.HinhMinhHoa = fileName;
                        //Save vào Images Folder
                        HinhMinhHoa.SaveAs(path);
                    }
                    mohinhDB.MaCD = mohinh.MaCD;
                    mohinhDB.MaNSX = mohinh.MaNSX;
                }
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCD = new SelectList(database.MOHINHs, "MaCD", "TenCD", mohinh.MaCD);
            ViewBag.MaNSX = new SelectList(database.MOHINHs, "MaNSX", "TenNSX", mohinh.MaNSX);

            return View(mohinh);
        }

        // GET: Admin/NhaSanXuat/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var category = database.MOHINHs.Where(c => c.MaMH == id).FirstOrDefault();
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/NhaSanXuat/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var category = database.MOHINHs.Where(c => c.MaMH == id).FirstOrDefault();
                database.MOHINHs.Remove(category);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Không xóa được do có liên quan đến bảng khác");
            }
        }
    }
}
