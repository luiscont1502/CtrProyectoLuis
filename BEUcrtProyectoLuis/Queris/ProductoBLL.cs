using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BEUcrtProyectoLuis.Queris
{
    public static class ProductoBLL
    {
        public static void Create(Producto a)
        {
            using (Entities db = new Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        a.prd_dateOfCreated = DateTime.Now;
                        db.Producto.Add(a);
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
        public static Producto Get(int? id)
        {
            Entities db = new Entities();
            return db.Producto.Find(id);
        }
        public static void Update(Producto Producto)
        {
            using (Entities db = new Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Producto.Attach(Producto);
                        db.Entry(Producto).State = System.Data.Entity.EntityState.Modified;
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
                        Producto Producto = db.Producto.Find(id);
                        db.Entry(Producto).State = System.Data.Entity.EntityState.Deleted;
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
        public static List<Producto> List()
        {
            Entities db = new Entities();
            return db.Producto.Include(c => c.Categoria).Include(p=>p.Promocion).ToList();
        }
        public static List<Producto> List(int id)
        {
            Entities db = new Entities();
            return db.Producto.Where(x => x.cat_id.Equals(id)).ToList();
        }
        public static List<Producto> GetProductsByPromocion(int id) {
            Entities db = new Entities();
            return db.Producto.Where(x => x.prm_id.Equals(id)).ToList();
        }
    }
}
