using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Alumnos.Models
{
    public class AlumnoCE
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Ingrese Nombres")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Ingrese Apellidos")]
        public string Apellidos { get; set; }
        [Required]
        [Display(Name = "Ingresa Edad")]
        public Nullable<int> Edad { get; set; }
        [Required]
        [Display(Name = "Ingrese Sexo")]
        public string Sexo { get; set; }
        [Required]
        [Display(Name = "Ingrese Ciudad")]
        public string CodCiudad { get; set; }
        public string NombreCiudad { get; set; }
        public string NombreCompleto { get { return Nombre + " " + Apellidos; } }
        public System.DateTime FechaRegistro { get; set; }
    }

    [MetadataType(typeof(AlumnoCE))]
    public partial class Alumno
    {
        public string NombreCompleto { get { return Nombre + " " + Apellidos; } }

        public string NombreCiudad { get; set; }
    }
}