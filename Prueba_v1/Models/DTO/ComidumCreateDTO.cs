using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Prueba_v1.Models.DTO
{
    public class ComidumCreateDTO
    {
        public int IdComida { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string? Imagen { get; set; }
        public decimal Precio { get; set; }
        public int IdCategoria { get; set; }

       
        public SelectList? Categorias { get; set; }
    }
}
