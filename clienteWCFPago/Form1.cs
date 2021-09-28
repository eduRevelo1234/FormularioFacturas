using clienteWCFPago.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clienteWCFPago
{
    public partial class Form1 : Form
    {
        int estado;
        decimal subtotal = 0;
        tbl_Factura factura = new tbl_Factura();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (ServiceReference1.Service1Client client = new ServiceReference1.Service1Client())
            {
                txtValorT.Enabled = false;
                btnBorrar.Enabled = false;
                actualizarListaFacturas();
                var factura = client.obtenerFactura(1);
                anadirCMB();
                txtID.Enabled = false;
                dtpBuscar.Enabled = false;
                txtBuscar.Enabled = false;
                btnActualizar.Enabled = false;
                btnGuardar1.Enabled = false;
                txtIDdetalle.Enabled = false;
                txtCantidad.Enabled = false;
                txtDescripcion.Enabled = false;
                txtValorT.Enabled = false;
                txtValorU.Enabled = false;
                txtSubtotal.Enabled = false;
                txtTotal.Enabled = false;

            }
        }

        public void anadirCMB()
        {
            comboBox1.Items.Add("Fecha");
            comboBox1.Items.Add("Empresa");
        }
        public void actualizarListaFacturas()
        {

            using (ServiceReference1.Service1Client client = new ServiceReference1.Service1Client())
            {
                lstFacturas.Items.Clear();
                var facturas = client.obtenerListaFacturas();
                
                foreach (tbl_Factura i in facturas)
                {
                    tbl_Factura f = (tbl_Factura)i;
                    tbl_FacturaC fac = new tbl_FacturaC(f.ciudad, f.fecha, f.cliente, f.direccion, f.numeroCedula, 
                        f.telefono, f.subtotal, f.total);
                    fac.idFactura = f.idFactura;
                    fac.subtotal = Math.Round(fac.subtotal, 2);
                    fac.total = Math.Round(fac.total, 2);
                  
                    lstFacturas.Items.Add(fac);
                }
            }

        }

        private void lstFacturas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                actualizarListaDetalle();
                vaciarTxt();
                btnActualizar.Enabled = true;
                btnBorrar.Enabled = true;
                var v = (tbl_FacturaC)lstFacturas.SelectedItem;
                //factura.ciudad = v.ciudad;
                // factura.cliente = v.cliente;

                txtID.Text = (Convert.ToString(v.idFactura));
                txtCiudad.Text = v.ciudad;
                txtFecha.Text = Convert.ToString(v.fecha);
                dtpFecha.Value = v.fecha;
                txtCliente.Text = v.cliente;
                txtDireccion.Text = v.direccion;
                txtCI.Text = v.numeroCedula;
                txtTelefono.Text = v.telefono;

                Math.Round(v.subtotal, 2);
                Math.Round(v.total, 2);
                txtSubtotal.Text = Convert.ToString(v.subtotal);
                txtTotal.Text = Convert.ToString(v.total);
                subtotal = v.subtotal;

            } catch(Exception ex)
            {
                MessageBox.Show("Seleccione algun elemento de la lista");
            }
     }
        public void vaciarTxt()
        {
            txtID.Clear();
            txtCiudad.Clear();
            txtFecha.Clear();
            txtCliente.Clear();
            txtDireccion.Clear();
            txtCI.Clear();
            txtTelefono.Clear();
            txtSubtotal.Clear();
            txtTotal.Clear();
            txtIDdetalle.Clear();
            txtCantidad.Clear();
            txtDescripcion.Clear();
            txtValorU.Clear();
            txtValorT.Clear();
        }

        public void actualizarListaDetalle()
        {
            using (ServiceReference1.Service1Client client = new ServiceReference1.Service1Client())
            {
                try
                {
                    lstDetalle.Items.Clear();

                    var v = (tbl_FacturaC)lstFacturas.SelectedItem;
                    var detalles = client.obtenerDetalles(Convert.ToInt32(v.idFactura));


                    foreach (var i in detalles)
                    {
                        tbl_Detalle f = (tbl_Detalle)i;
                        
                        tbl_DetalleC detalle = new tbl_DetalleC(f.cantidad, f.descripcion, f.valorU, f.valorT, f.idFactura);
                        detalle.valorT = Math.Round(detalle.valorT, 2);
                        detalle.valorU = Math.Round(detalle.valorU, 2);
                        detalle.idDetalle = f.idDetalle;
                        lstDetalle.Items.Add(detalle);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error cargar lista detalle");
                }
            }

        }

        private void lstDetalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var v = (tbl_DetalleC)lstDetalle.SelectedItem;
                txtIDdetalle.Text = Convert.ToString(v.idDetalle);
                txtCantidad.Text = Convert.ToString(v.cantidad);
                txtDescripcion.Text = v.descripcion;
                txtValorU.Text = Convert.ToString(v.valorU);
                txtValorT.Text = Convert.ToString(v.valorT);
                btnGuardar1.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Seleccione un elemento de la lista");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedText == "Empresa")
            {
                estado = 1;
                dtpBuscar.Enabled = false;
                txtBuscar.Enabled = true;
            }
            else if (comboBox1.SelectedText == "Fecha")
            {
                estado = 0;
                txtBuscar.Enabled = false;
                dtpBuscar.Enabled = true;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (ServiceReference1.Service1Client client = new ServiceReference1.Service1Client())
            {
                if (estado == 1)
                {
                    cargarListaXempresa();

                }
                else
                {
                    cargarListaXfecha();
                }
            }
        }
        public void cargarListaXfecha()
        {
            using (ServiceReference1.Service1Client client = new ServiceReference1.Service1Client())
            {
                lstFacturas.Items.Clear();
                var facturas = client.obtenerListaFacturasConFecha(Convert.ToDateTime(dtpBuscar.Value));
                foreach (tbl_Factura i in facturas)
                {
                    tbl_Factura f = (tbl_Factura)i;
                    tbl_FacturaC fac = new tbl_FacturaC(f.ciudad, f.fecha, f.cliente, f.direccion, f.numeroCedula,
                       f.telefono, f.subtotal, f.total);
                    fac.idFactura = f.idFactura;
                    fac.subtotal = Math.Round(fac.subtotal, 2);
                    fac.total = Math.Round(fac.total, 2);
                    lstFacturas.Items.Add(fac);
                }
            }
        }

        public void cargarListaXempresa()
        {
            using (ServiceReference1.Service1Client client = new ServiceReference1.Service1Client())
            {
                lstFacturas.Items.Clear();
                var facturas = client.obtenerListaFacturasConCliente(txtBuscar.Text);
                foreach (tbl_Factura i in facturas)
                {
                    tbl_Factura f = (tbl_Factura)i;
                    tbl_FacturaC fac = new tbl_FacturaC(f.ciudad, f.fecha, f.cliente, f.direccion, f.numeroCedula,
                       f.telefono, f.subtotal, f.total);
                    fac.idFactura = f.idFactura;
                    fac.subtotal = Math.Round(fac.subtotal, 2);
                    fac.total = Math.Round(fac.total, 2);
                    lstFacturas.Items.Add(fac);
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using(ServiceReference1.Service1Client client = new ServiceReference1.Service1Client())
            {
                vaciarTxt();
                subtotal = 0;
                
                lstDetalle.Items.Clear();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (ServiceReference1.Service1Client client = new ServiceReference1.Service1Client())
            {
                try
                {
                    if (txtCI.Text.Trim() == "" || txtCiudad.Text.Trim() == "" || txtCliente.Text.Trim() == "" || txtDireccion.Text.Trim() == "" || txtTelefono.Text.Trim() == "")
                    {
                        MessageBox.Show("Ingrese todos los campos");
                    }
                    else
                    {
                        factura.ciudad = txtCiudad.Text;
                        factura.cliente = txtCliente.Text;
                        factura.direccion = txtDireccion.Text;
                        factura.fecha = dtpFecha.Value;
                        factura.numeroCedula = txtCI.Text;
                        factura.telefono = txtTelefono.Text;
                        factura.subtotal = 0;
                        factura.total = 0;
                        client.agregarFactura(factura);
                        actualizarListaFacturas();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR");
                }
            }
        }

        private void validarTextBoxFac()
        {
            
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            using (ServiceReference1.Service1Client client = new ServiceReference1.Service1Client())
            {
                try
                {
                    if (txtCI.Text.Trim() == "" || txtCiudad.Text.Trim() == "" || txtCliente.Text.Trim() == "" || txtDireccion.Text.Trim() == "" || txtTelefono.Text.Trim() == "")
                    {
                        MessageBox.Show("Ingrese todos los campos");
                    }
                    else
                    {
                        var v = (tbl_FacturaC)lstFacturas.SelectedItem;
                        factura = client.obtenerFactura(v.idFactura);
                        txtID.Text = (Convert.ToString(factura.idFactura));

                        factura.ciudad = txtCiudad.Text;
                        factura.fecha = dtpFecha.Value;
                        factura.cliente = txtCliente.Text;
                        factura.direccion = txtDireccion.Text;
                        factura.numeroCedula = txtCI.Text;
                        factura.subtotal = Convert.ToDecimal(txtSubtotal.Text);
                        factura.total = Convert.ToDecimal(txtTotal.Text);
                        client.actualizarFacturas(factura.idFactura, factura.ciudad, dtpFecha.Value, txtCliente.Text, txtDireccion.Text, txtCI.Text,
                            Convert.ToDecimal(txtSubtotal.Text), Convert.ToDecimal(txtTotal.Text));
                        actualizarListaFacturas();
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ërror");
                }

            }
        }
        private void btnGuardar1_Click(object sender, EventArgs e)
        {
            using (ServiceReference1.Service1Client client = new ServiceReference1.Service1Client())
            {
               try
                {
                    if (txtCantidad.Text.Trim() == "" || txtValorU.Text.Trim() == "" || txtDescripcion.Text.Trim() == "" )
                    {
                        MessageBox.Show("Ingrese todos los campos");
                    }
                    var v = (tbl_FacturaC)lstFacturas.SelectedItem;

                    decimal valorTotal = Convert.ToInt32(txtCantidad.Text) * Convert.ToDecimal(txtValorU.Text);
                    
                    tbl_Detalle detalle = new tbl_Detalle();
                    detalle.cantidad = Convert.ToInt32(txtCantidad.Text);
                    detalle.descripcion = txtDescripcion.Text;
                    detalle.valorU = Convert.ToDecimal(txtValorU.Text);
                    detalle.valorT = valorTotal;
                    detalle.idFactura = v.idFactura;

                    client.agregarDetalle(detalle);
                    actualizarListaDetalle();
                    
                    v.subtotal = v.subtotal + detalle.valorT;
                    decimal aux = Convert.ToDecimal(0.12);
                    decimal iva = v.subtotal * aux;
                    
                    tbl_Factura f = client.obtenerFactura(v.idFactura);
                    f.total = v.subtotal + (iva);
                    MessageBox.Show(Convert.ToString(f.idFactura));
                client.actualizarFacturas(f.idFactura, f.ciudad, f.fecha, f.cliente, f.direccion, f.numeroCedula,
                    v.subtotal, f.total);
                    actualizarListaFacturas();
                
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ërror");
                }
            }
        }

        private void btnNuevo1_Click(object sender, EventArgs e)
        {
            txtCantidad.Clear();
            txtIDdetalle.Clear();
            txtDescripcion.Clear();
            txtValorU.Clear();
            txtValorT.Clear();
            txtCantidad.Enabled = true;
            txtDescripcion.Enabled = true;
            txtValorU.Enabled = true;
            btnGuardar1.Enabled = true;

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            using (ServiceReference1.Service1Client client = new ServiceReference1.Service1Client())
            {
                tbl_FacturaC v = (tbl_FacturaC)lstFacturas.SelectedItem;
                client.eliminarFactura(v.idFactura);
                actualizarListaFacturas();

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            actualizarListaFacturas();
            txtBuscar.Clear();
        }

        private void txtCI_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtValorU_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtValorU_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.soloNumerosDecimal(e);
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.soloNumeros(e);
        }

        private void txtCI_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.soloNumeros(e);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.soloNumeros(e);
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtCiudad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.soloLetras(e);
        }
    }
}
