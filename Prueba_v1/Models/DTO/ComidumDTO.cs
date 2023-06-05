using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Prueba_v1.Models.dbModels;

namespace Prueba_v1.Models.DTO
{
    public class ComidumDTO : Controller
    {
        [Key]
        [Column("id_comida")]
        public int IdComida { get; set; }
        [Column("nombre")]
        [StringLength(50)]
        [Unicode(false)]
        public string Nombre { get; set; } = null!;
        [Column("descripcion")]
        [StringLength(300)]
        [Unicode(false)]
        public string Descripcion { get; set; } = null!;
        [Column("precio", TypeName = "numeric(18, 0)")]
        public decimal Precio { get; set; }
        [Column("id_categoria")]
        public int IdCategoria { get; set; }

        public IActionResult Index()
        {
            return View();
        }
    }
}
