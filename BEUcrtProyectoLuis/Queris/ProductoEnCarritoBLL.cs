using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace BEUcrtProyectoLuis.Queris
{
    public static class ProductoEnCarritoBLL
    {
        public static void Create(ProductoEnCarrito a)
        {
            using (Entities db = new Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        a.pcr_est = "Pendiente";
                        a.pcr_dateOfCreated = DateTime.Now;
                        db.ProductoEnCarrito.Add(a);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public static ProductoEnCarrito Get(int? id)
        {
            Entities db = new Entities();
            return db.ProductoEnCarrito.Find(id);
        }
        public static void Update(ProductoEnCarrito ProductoEnCarrito)
        {
            using (Entities db = new Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.ProductoEnCarrito.Attach(ProductoEnCarrito);
                        db.Entry(ProductoEnCarrito).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public static void Delete(int? id)
        {
            using (Entities db = new Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        ProductoEnCarrito ProductoEnCarrito = db.ProductoEnCarrito.Find(id);
                        db.Entry(ProductoEnCarrito).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }

        }
        public static List<ProductoEnCarrito> List()
        {
            Entities db = new Entities();
            return db.ProductoEnCarrito.Include(c => c.Carrito).ToList();
        }
        public static List<ProductoEnCarrito> List(int id)
        {
            Entities db = new Entities();
            return db.ProductoEnCarrito.Where(x => x.car_id.Equals(id)).ToList();
        }
        public static List<ProductoEnCarrito> GetProductsInCarByState(string pcr_est) {
            Entities db = new Entities();
            return db.ProductoEnCarrito.Where(x => x.pcr_est.Equals(pcr_est)).ToList();
        
        }
    }
}
