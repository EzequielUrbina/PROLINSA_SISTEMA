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
        public int CANTIDAD { set; get; }
        [Required]
        public Nullable<decimal> TOTAL { get { return Precio_Venta * CANTIDAD; } }
    }




}