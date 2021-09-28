using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace WcfFactura1
{
    //Interfaz de servicio ISERVICE1 para ser usada por el cliente 
    //Definicion de la interfaz del servicio de pago que sera consumido por el formulario Web
    //Aqui se definen todas las operaciones de servicio
    [ServiceContract]
    public interface IService1
    {
        //Metodo para obtener detalle con idFactura
        [OperationContract]
        List<tbl_Detalle> obtenerDetalles(int idFactura);
        //Metodo para obtener todos los detalles
        [OperationContract]
        List<tbl_Detalle> obtenerTodoDetalles();
        //Metodo para obtener todos las facturas
        [OperationContract]
        List<tbl_Factura> obtenerListaFacturas();
        //Metodo para obtener lista de facturas con fecha
        [OperationContract]
        List<tbl_Factura> obtenerListaFacturasConFecha(DateTime date);
        //Metodo para obtener lista facturas con el nombre del cliente
        [OperationContract]
        List<tbl_Factura> obtenerListaFacturasConCliente(string cliente);
        //Metodo para obtener factura con ID
        [OperationContract]
        tbl_Factura obtenerFactura(int id);
        //Metodo para eliminar factura con id de factura
        [OperationContract]
        void eliminarFactura(int idFactura);
        //Metodo para agregar factura
        [OperationContract]
        void agregarFactura(tbl_Factura factura);
        //MEtodo para agregar detalle
        [OperationContract]
        void agregarDetalle(tbl_Detalle detalle);
        //Metodo para actualizar factura
        [OperationContract]
        void actualizarFacturas(int ID, string ciudad, DateTime fecha, string cliente, string direccion, string CI,
                    decimal subtotal, decimal total);

    }

    // Se agrega los tipos compuestos a las operaciones de servicio
    // Se define la clase factura que se usara para guardar en la base de datos segun los datos ingresados por el cliente 

    [ServiceContract]
    [DataContract]
    [Table(Name = "tbl_Factura")]
    public class tbl_Factura
    {
        [DataMember]
        [Column(IsPrimaryKey = true, AutoSync = AutoSync.OnInsert, IsDbGenerated = true)]
        public int idFactura;
        [DataMember]
        [Column]
        public string ciudad;
        [DataMember]
        [Column]
        public DateTime fecha;
        [DataMember]
        [Column]
        public string cliente;
        [DataMember]
        [Column]
        public string direccion;
        [DataMember]
        [Column]
        public string numeroCedula;
        [DataMember]
        [Column]
        public string telefono;
        [DataMember]
        [Column]
        public decimal subtotal;
        [DataMember]
        [Column]
        public decimal total;

        public tbl_Factura()
        {
        }

        public tbl_Factura(string ciudad, DateTime fecha, string cliente, string direccion, string numeroCedula, string telefono, decimal subtotal, decimal total)
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
            return "ID: "+idFactura + " Nombre: "+cliente;
        }
    }
    // Se define la clase detalles  que se usara para cargar de la base de datos los detalles
    [ServiceContract]
    [DataContract]
    [Table(Name ="tbl_Detalle")]
    public class tbl_Detalle
    {
        [DataMember]
        [Column(IsPrimaryKey = true, AutoSync = AutoSync.OnInsert, IsDbGenerated = true)]
        public int idDetalle;
        [DataMember]
        [Column]
        public int cantidad;
        [DataMember]
        [Column]
        public string descripcion;
        [DataMember]
        [Column]
        public decimal valorU;
        [DataMember]
        [Column]
        public decimal valorT;
        [DataMember]
        [Column]
        public int idFactura;

        public tbl_Detalle()
        {
        }

        public tbl_Detalle(int cantidad, string descripcion, decimal valorU, decimal valorT, int idFactura)
        {
            this.cantidad = cantidad;
            this.descripcion = descripcion;
            this.valorU = valorU;
            this.valorT = valorT;
            this.idFactura = idFactura;
        }
        [OperationContract]
        public override string ToString()
        {
            return "Cantidad: "+cantidad +" Precio: "+valorU;
        }


    }

}
