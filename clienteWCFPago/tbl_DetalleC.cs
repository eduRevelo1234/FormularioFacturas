using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clienteWCFPago
{
    class tbl_DetalleC
    {

        public int idDetalle;

        public int cantidad;

        public string descripcion;

        public decimal valorU;

        public decimal valorT;

        public int idFactura;

        public tbl_DetalleC()
        {
        }

        public tbl_DetalleC(int cantidad, string descripcion, decimal valorU, decimal valorT, int idFactura)
        {
            this.cantidad = cantidad;
            this.descripcion = descripcion;
            this.valorU = valorU;
            this.valorT = valorT;
            this.idFactura = idFactura;
        }

        public override string ToString()
        {
            return  "ID factura: "+ idFactura +" Descripcion: "+descripcion + " Cantidad: " + cantidad + " Valor unitario: " + valorU + " Valor total: " +valorT;
        }
    }
}
