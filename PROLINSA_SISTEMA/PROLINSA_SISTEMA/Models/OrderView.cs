using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROLINSA_SISTEMA.Models
{
    public class OrderView
    {
        [Display(Name = "Empleado")]
        public List <FacturaMaestro> IDEMPLEADO { set; get; }
        [Display(Name = "Cliente")]
        public List <FacturaMaestro> IDCLIENTE { set; get; }
        [Display(Name = "Facturta / Cotización")]
        public List <FacturaMaestro> IDTIPOFACTURA { set; get; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public DateTime FechaA { get; set; }

        public List <FacturaDetalle> idproductos { set; get; }
        public FacturaDetalle Cantidad { set; get; }
        public FacturaDetalle Subtotal { set; get; }
        public FacturaDetalle Total { set; get; }

        public Producto Titulos { set; get; }

       

    }
}