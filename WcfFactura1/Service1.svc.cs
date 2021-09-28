using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;

namespace WcfFactura1
{
    //Se define la clase service que define la implementacion de las operaciones de la interfaz
    public class Service1 : IService1
    {
        bd_Factura bd_Factura = new bd_Factura();

        //Metodo para obtener detalles con el id de factura en la base de datos ulsando una consulta sql mediante LINQ
        public List<tbl_Detalle> obtenerDetalles(int idFactura)
        {
            bd_Factura bd_Facturax = new bd_Factura();
            List<tbl_Detalle> detalles = new List<tbl_Detalle>();
            var lstDetalle = from iter in bd_Facturax.detalles
                             where iter.idFactura == idFactura
                             select iter;
            try
            {
                foreach (tbl_Detalle item in lstDetalle)
                {
                    detalles.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return detalles;
        }
        //Metodo para obtener todas las facturas en la base de datos ulsando una consulta sql mediante LINQ
        public List<tbl_Factura> obtenerListaFacturas()
        {
            List<tbl_Factura> facturas = new List<tbl_Factura>();
            var lstFacturas = from iter in this.bd_Factura.facturas
                              select iter;
            try
            {
                foreach (tbl_Factura item in lstFacturas)
                {
                    facturas.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return facturas;
        }
        //Metodo para obtener todas los detalles en la base de datos ulsando una consulta sql mediante LINQ
        public List<tbl_Detalle> obtenerTodoDetalles()
        {
            List<tbl_Detalle> detalles = new List<tbl_Detalle>();
            var lstDetalles = from iter in this.bd_Factura.detalles
                              select iter;
            try
            {
                foreach (tbl_Detalle item in lstDetalles)
                {
                    detalles.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return detalles;
        }
        //Metodo para obtener facturas con las fechas en la base de datos ulsando una consulta sql mediante LINQ
        public List<tbl_Factura> obtenerListaFacturasConFecha(DateTime date)
        {
            List<tbl_Factura> facturas = new List<tbl_Factura>();
            var lstFacturas = from iter in this.bd_Factura.facturas
                              where iter.fecha == date
                              select iter;
            try
            {
                foreach (tbl_Factura item in lstFacturas)
                {
                    facturas.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return facturas;
        }
        //Metodo para obtener facturas con el nombre de cliente en la base de datos ussando una consulta sql mediante LINQ
        public List<tbl_Factura> obtenerListaFacturasConCliente(string cliente)
        {
            List<tbl_Factura> facturas = new List<tbl_Factura>();
            var lstFacturas = from iter in this.bd_Factura.facturas
                              where iter.cliente == cliente
                              select iter;
            try
            {
                foreach (tbl_Factura item in lstFacturas)
                {
                    facturas.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return facturas;
        }



        //Metodo para obtener factura con el id en la base de datos ulsando una consulta sql mediante LINQ
        public tbl_Factura obtenerFactura(int id)
        {
            var lstFacturas = (from iter in bd_Factura.facturas
                               where iter.idFactura == id
                               select iter).FirstOrDefault();
            return lstFacturas;
        }
        //Metodo para actualziar facturas en la base de datos ulsando una consulta sql mediante LINQ
        public void actualizarFacturas(int ID, string ciudad, DateTime fecha, string cliente, string direccion, string CI,
                    decimal subtotal, decimal total)
        {
            var f = (from iter in bd_Factura.facturas
                               where iter.idFactura == ID
                               select iter).FirstOrDefault();
            f.ciudad = ciudad;
            f.fecha = fecha;
            f.cliente = cliente;
            f.direccion = direccion;
            f.numeroCedula = CI;
            f.subtotal = subtotal;
            f.total = total;
            bd_Factura.SubmitChanges();
        }
        //Metodo para eliminar factura con el id en la base de datos ulsando una consulta sql mediante LINQ
        public void eliminarFactura(int idFactura)
        {
            List<tbl_Detalle> detalles = new List<tbl_Detalle>();
            var lstDetalle = from iter in this.bd_Factura.detalles
                             where iter.idFactura == idFactura
                             select iter;
            try
            {
                foreach (tbl_Detalle item in lstDetalle)
                {
                    bd_Factura.detalles.DeleteOnSubmit(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            bd_Factura.SubmitChanges();

            var lstFacturas = (from iter in this.bd_Factura.facturas
                               where iter.idFactura == idFactura
                               select iter).FirstOrDefault();
            bd_Factura.facturas.DeleteOnSubmit(lstFacturas);
            bd_Factura.SubmitChanges();
        }
        //Metodo para agregar factura en la base de datos ulsando una consulta sql mediante LINQ
        public void agregarFactura(tbl_Factura factura)
        {
            bd_Factura.facturas.InsertOnSubmit(factura);
            bd_Factura.SubmitChanges();
        }
        //Metodo para agregar detalle en la base de datos ulsando una consulta sql mediante LINQ
        public void agregarDetalle(tbl_Detalle detalle)
        {
            try
            {
                bd_Factura.detalles.InsertOnSubmit(detalle);
                bd_Factura.SubmitChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error");
                    
            }
        }       
    }
}
