using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace BEUcrtProyectoLuis.Queris
{
    public class CabezaFacturaBLL
    {
        public static void Create(CabezaFactura a) {
            using (Entities db=new Entities()) {
                using (var transaction=db.Database.BeginTransaction()) {
                    try {
                        db.CabezaFactura.Add(a);
                        db.SaveChanges();
                        transaction.Commit();
                    } catch (Exception ex) {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public static CabezaFactura Get(int? id) {
            Entities db = new Entities();
            return db.CabezaFactura.Find(id);
        }
        public static void Update(CabezaFactura cabezafactura) {
            using (Entities db=new Entities()) {
                using (var transaction =db.Database.BeginTransaction()) {
                    try {
                        db.CabezaFactura.Attach(cabezafactura);
                        db.Entry(cabezafactura).State = EntityState.Modified;                       db.SaveChanges();
                        transaction.Commit();
                    } catch (Exception  ex) {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public static void Delete(int? id) {
            using (Entities db=new Entities()) {
                using (var transaction = db.Database.BeginTransaction()) {
                    try {
                        CabezaFactura cabezafactura = db.CabezaFactura.Find(id);
                        db.Entry(cabezafactura).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex) {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        
        }
        public static List<CabezaFactura> List() 
        {
            Entities db = new Entities();
            return db.CabezaFactura.Include(c =>c.Cliente).ToList();
        }
        public static List<CabezaFactura> List(int cln_id) {
            Entities db = new Entities();
            return db.CabezaFactura.Where(x => x.cln_id.Equals(cln_id)).ToList();
        }




    }


}
