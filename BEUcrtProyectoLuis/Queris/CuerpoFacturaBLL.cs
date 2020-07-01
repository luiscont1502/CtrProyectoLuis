using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BEUcrtProyectoLuis.Queris
{
    public static class CuerpoFacturaBLL
    {
        public static void Create(CuerpoFactura a)
        {
            using (Entities db = new Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                       
                        db.CuerpoFactura.Add(a);
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
        public static CuerpoFactura Get(int? id)
        {
            Entities db = new Entities();
            return db.CuerpoFactura.Find(id);
        }
        public static void Update(CuerpoFactura cuerpoFactura)
        {
            using (Entities db = new Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.CuerpoFactura.Attach(cuerpoFactura);
                        db.Entry(cuerpoFactura).State = System.Data.Entity.EntityState.Modified;
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
                        CuerpoFactura CuerpoFactura = db.CuerpoFactura.Find(id);
                        db.Entry(CuerpoFactura).State = System.Data.Entity.EntityState.Deleted;
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
        public static List<CuerpoFactura> List()
        {
            Entities db = new Entities();
            return db.CuerpoFactura.Include(c => c.CabezaFactura).ToList();
        }
     
    }
}
