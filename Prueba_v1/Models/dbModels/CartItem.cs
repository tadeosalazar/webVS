using System;
using Prueba_v1.Models.dbModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Prueba_v1.Models.dbModels
{
    public class CartItem
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdComida { get; set; }
        public int Cantidad { get; set; }
        // Otras propiedades necesarias
    }
}
