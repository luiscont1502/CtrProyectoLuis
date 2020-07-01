using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BEUcrtProyectoLuis;
using BEUcrtProyectoLuis.Queris;

namespace CtrProyectoLuis.Controllers
{
    public class ProductosController : Controller
    {
        private Entities db = new Entities();

        // GET: Productos
        public ActionResult Index()
        {
            var producto = db.Producto.Include(p => p.Categoria).Include(p => p.Promocion);
            return View(producto.ToList());
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.cat_id = new SelectList(db.Categoria, "cat_id", "cat_nom");
            ViewBag.prm_id = new SelectList(db.Promocion, "prm_id", "prm_nom");
            return View();
        }

        // POST: Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "prd_id,cat_id,prm_id,prd_nom,prd_img,prd_tal,prd_crt,prd_cnt,prd_dateOfCreated,prd_precio")] Producto producto)
        {
            HttpPostedFileBase fileBase = Request.Files[0];
            string path = SubirImagen(fileBase);
            if (path != "")
            {
                producto.prd_img = path;
            }
            else
            {
                producto.prd_img = "default";
            }
            if (ModelState.IsValid)
            {
                db.Producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cat_id = new SelectList(db.Categoria, "cat_id", "cat_nom", producto.cat_id);
            ViewBag.prm_id = new SelectList(db.Promocion, "prm_id", "prm_nom", producto.prm_id);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.cat_id = new SelectList(db.Categoria, "cat_id", "cat_nom", producto.cat_id);
            ViewBag.prm_id = new SelectList(db.Promocion, "prm_id", "prm_nom", producto.prm_id);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "prd_id,cat_id,prm_id,prd_nom,prd_img,prd_tal,prd_crt,prd_cnt,prd_dateOfCreated,prd_precio")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cat_id = new SelectList(db.Categoria, "cat_id", "cat_nom", producto.cat_id);
            ViewBag.prm_id = new SelectList(db.Promocion, "prm_id", "prm_nom", producto.prm_id);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private string SubirImagen(HttpPostedFileBase file)
        {
            string nombre = "";
            if (file != null && file.ContentLength > 0)
                try
                {
                    ArchivoBLL modelo = new ArchivoBLL();
                    nombre = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
                    string path = Server.MapPath("~/Content/Imagenes/") + nombre;
                    modelo.SubirArchivo(path, file);
                    ViewBag.Message = modelo.confirmacion;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            return nombre;
        }
    }
}
