using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kelloggs.Models
{
    public class ContactoData
    {
        [Required(ErrorMessage = "Es necesario asignar un usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Es necesario asignar un correo")]
        [EmailAddress(ErrorMessage = "Es necesario asignar un correo")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Es necesario seleccionar un país")]
        public string Contry { get; set; }
    }
}