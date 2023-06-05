using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Prueba_v1.Models.dbModels;


namespace Prueba_v1.Models.dbModels
{
    [Table("Carrito")]
    public partial class Carrito
    {
        [Key]
        public int IdCarrito { get; set; }


        [Column("Id_usuario")]
        public int IdUsusario { get; set; }
        
        [Column("Id_comida")]
        public int IdComida { get; set; }
        [Column("cantidad")]
        public int? Cantidad { get; set; }

        [ForeignKey("IdComida")]
        [InverseProperty("Carritos")]
        public virtual Comidum IdComidaNavigation { get; set; } = null!;
        [ForeignKey("IdUsusario")]
        [InverseProperty("Carritos")]
        public virtual ApplicationUser IdUsusario1 { get; set; } = null!;
        [ForeignKey("IdUsusario")]
        [InverseProperty("Carritos")]
        public virtual Pedido IdUsusarioNavigation { get; set; } = null!;

 
    }
}
