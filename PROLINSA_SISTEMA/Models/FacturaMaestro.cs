using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROLINSA_SISTEMA.Models
{
    public class FacturaMaestro:Facturas
    {
        public  int IDEMPLEADO { set; get; }
        public  int IDCLIENTE { set; get; }
        public  int IDTIPOFACTURA { set; get; }

        [Display(Name ="Fecha")]
        public Nullable<System.DateTime> FECHAAA { set; get; }
        
    }
}   