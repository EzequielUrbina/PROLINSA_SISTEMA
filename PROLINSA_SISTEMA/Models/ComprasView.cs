using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROLINSA_SISTEMA.Models
{
    public class ComprasView
    {

        [Required]
        [Range(1, 90000)]
        public List<Proveedores> IDPROVEEDOR { set; get; }


        [Required]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime FechaA { get; set; }

        public List<detallecompraas> IdProducto { set; get; }
        public int CANTIDAD { set; get; }
        public FacturaDetalle Subtotal { set; get; }
        public FacturaDetalle Total { set; get; }


        public Producto Titulos { set; get; }
    }
}