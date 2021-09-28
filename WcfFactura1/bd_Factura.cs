using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

namespace WcfFactura1
{ 
    //Creacion de la clase base de datos usando linq 
    public class bd_Factura : DataContext
    {
        public bd_Factura(): base(@"Data Source=DESKTOP-V65BFOG\SQLEXPRESS;Initial Catalog=BD_Factura1;Integrated Security=True") { }
        public Table<tbl_Factura> facturas;
        public Table<tbl_Detalle> detalles;
    }
}