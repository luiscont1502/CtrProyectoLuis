using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace BEUcrtProyectoLuis.Queris
{
    public class ClienteBLL
    {
        public static void Create(Cliente a)
        {
            using (Entities db = new Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        a.cln_tipo = "Nuevo";
                        a.cln_dateOfCreated = DateTime.Now;
                        db.Cliente.Add(a);
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
        public static Cliente Get(int? id)
        {
            Entities db = new Entities();
            return db.Cliente.Find(id);
        }
        public static void Update(Cliente cliente)
        {
            using (Entities db = new Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Cliente.Attach(cliente);
                        db.Entry(cliente).State = System.Data.Entity.EntityState.Modified;
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
                        Cliente Cliente = db.Cliente.Find(id);
                        db.Entry(Cliente).State = System.Data.Entity.EntityState.Deleted;
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
        public static List<Cliente> List()
        {
            Entities db = new Entities();
            return db.Cliente.Include(c => c.Usuario).ToList();
        }
        public static List<Cliente> List(int uso_usu)
        {
            Entities db = new Entities();
            return db.Cliente.Where(x => x.uso_id.Equals(uso_usu)).ToList();
        }
    }
}
