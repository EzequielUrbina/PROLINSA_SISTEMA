using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROLINSA_SISTEMA.Models
{
    public class FacturaDetalle : Producto
    {
        [Required]
        [Display (Name ="Cantidad")]
        public int CANTIDAD { set; get; }

        [Required]
        [Display(Name = "Sub-total")]
        public Nullable<decimal> TOTAL { get { return Precio_Venta * CANTIDAD; } }
    }




}