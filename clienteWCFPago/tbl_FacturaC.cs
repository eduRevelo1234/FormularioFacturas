using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clienteWCFPago
{
    class tbl_FacturaC
    {
        public int idFactura;
        public string ciudad;
        public DateTime fecha;
        public string cliente;
        public string direccion;
        public string numeroCedula; 
        public string telefono;
        public decimal subtotal;
        public decimal total;

        public tbl_FacturaC()
        {
        }

        public tbl_FacturaC(string ciudad, DateTime fecha, string cliente, string direccion, string numeroCedula, string telefono, decimal subtotal, decimal total)
        {
            this.ciudad = ciudad;
            this.fecha = fecha;
            this.cliente = cliente;
            this.direccion = direccion;
            this.numeroCedula = numeroCedula;
            this.telefono = telefono;
            this.subtotal = subtotal;
            this.total = total;
        }
        

        public override string ToString()
        {
            return "ID Factura: " + idFactura + " Cliente: " + cliente +" Fecha: "+ fecha+ " Total factura: " +total;
        }
    }
}
