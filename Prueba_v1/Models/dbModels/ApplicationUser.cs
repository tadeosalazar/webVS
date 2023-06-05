using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Prueba_v1.Models.dbModels
{
    public class ApplicationUser : IdentityUser<int>
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public ApplicationUser()
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        {
            Carritos = new HashSet<Carrito>();
            Reseñas = new HashSet<Reseña>();
        }

        




        [InverseProperty("IdPedidoNavigation")]
        public virtual Pedido? Pedido { get; set; }
        [InverseProperty("IdUsusario1")]
        public virtual ICollection<Carrito> Carritos { get; set; }
        [InverseProperty("IdUsuario1")]
        public virtual ICollection<Reseña> Reseñas { get; set; }


    }
}
