using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BEUcrtProyectoLuis.Queris
{
    public static class PagoBLL
    {
        public static void Create(Pago a)
        {
            using (Entities db = new Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                      

                        db.Pago.Add(a);
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
        public static Pago Get(int? id)
        {
            Entities db = new Entities();
            return db.Pago.Find(id);
        }
        public static void Update(Pago Pago)
        {
            using (Entities db = new Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Pago.Attach(Pago);
                        db.Entry(Pago).State = System.Data.Entity.EntityState.Modified;
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
                        Pago Pago = db.Pago.Find(id);
                        db.Entry(Pago).State = System.Data.Entity.EntityState.Deleted;
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
        public static List<Pago> List()
        {
            Entities db = new Entities();
            return db.Pago.Include(c => c.Cliente).ToList();
        }
        public static List<Pago> List(int id)
        {
            Entities db = new Entities();
            return db.Pago.Where(x => x.cln_id .Equals(id)).ToList();
        }
    }
}
