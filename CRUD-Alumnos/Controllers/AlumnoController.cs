using CRUD_Alumnos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Alumnos.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index()
        {
            try
            {
                /*int edad = 18;
                string sql = @"select 
                a.Id as Id,
                a.Nombre as Nombre,
                a.Apellidos as Apellidos,
                a.Edad as Edad,
                a.Sexo as Sexo,
                a.FechaRegistro as FechaRegistro,
                c.Nombre as NombreCiudad
                 from Alumno a
                inner join Ciudad c on c.Id=a.Id
                where a.Edad > @edad";*/

                using (var db = new AlumnosContext())
                {
                   // return View(db.Database.SqlQuery<AlumnoCE>(sql, 
                     //   new SqlParameter("@edad",edad)).ToList());
                    var data = from a in db.Alumno
                               join c in db.Ciudad on a.CodCiudad equals c.Id
                               select new AlumnoCE()
                               {
                                   Id = a.Id,
                                   Nombre = a.Nombre,
                                   Apellidos = a.Apellidos,
                                   Edad = a.Edad,
                                   Sexo = a.Sexo,
                                   NombreCiudad = c.Nombre,
                                   FechaRegistro = a.FechaRegistro
                               };
                    return View(data.ToList());
                    //where a.Equals > 18
                    //select a
                    //List<Alumno> lista = db.Alumno.Where(a => a.Edad > 18).ToList();
                    //
                    
                }
            }
            catch (Exception)
            {

                throw;
            }

            // AlumnosContext db = new AlumnosContext();
            //List<Alumno> lista = db.Alumno.Where(a => a.Edad > 18).ToList();

        }
        public ActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Alumno a)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                try
                {
                    using (var db = new AlumnosContext())
                    {
                        a.FechaRegistro = DateTime.Now;
                        db.Alumno.Add(a);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", "Error al agregar el Alumno - " + ex.Message);
                }
            }

            return View();
        }
        public ActionResult Editar(int id)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    //Alumno al = db.Alumno.Where(a => al.Id == id).FirstOrDefault();
                    Alumno al2 = db.Alumno.Find(id);
                    return View(al2);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Alumno a)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    Alumno al = db.Alumno.Find(a.Id);
                    al.Nombre = a.Nombre;
                    al.Sexo = a.Sexo;
                    al.Apellidos = a.Apellidos;
                    al.Edad = a.Edad;
                    db.SaveChanges();
                    //return View();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
        public ActionResult Detalles(int id)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    //Alumno al = db.Alumno.Where(a => al.Id == id).FirstOrDefault();
                    Alumno al2 = db.Alumno.Find(id);
                    return View(al2);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Eliminar(int id)
        {
            try
            {
                using (var db = new AlumnosContext())
                {
                    //Alumno al = db.Alumno.Where(a => al.Id == id).FirstOrDefault();
                    Alumno al2 = db.Alumno.Find(id);
                    db.Alumno.Remove(al2);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult ListaCiudades()
        {
            using (var db = new AlumnosContext())
            {
                return PartialView(db.Ciudad.ToList());
            }
        }

        public static string NombreCiudad(int CodCiudad)
        {
            using (var db = new AlumnosContext())
            {
                return db.Ciudad.Find(CodCiudad).Nombre;
            }
        }
    }
}