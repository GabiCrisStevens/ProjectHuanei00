using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProyectHuanei0
{
    public partial class Form1 : Form
    {
        public int IDCliente;
        public Form1()
        {
            IDCliente = 0;
            InitializeComponent();
            reestablecer();
        }

        private class Cliente
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Fecha_Nac { get; set; }
            public string Direccion { get; set; } 
        }

        SqlConnection conectar = new SqlConnection(" Fuente de datos GABYPC\\GABYPC; Catálogo inicial = Taller; Seguridad integrada = verdadero; ");

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtApellido_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDNI_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string strNombre, strApellido , strNacimiento , strDireccion;
            strNombre = txtNombre.Text;
            strApellido = txtApellido.Text;
            strNacimiento = dtmNacimiento.Value.Year.ToString() + '/' + dtmNacimiento.Value.Month.ToString() + '/' + dtmNacimiento.Value.Day.ToString();
            strDireccion = txtDireccion.Text;


            if (IDCliente.Equals(0))
            {

                List<Cliente> lista = new List<Cliente>();

                string consulta = string.Format("Insert into clientes (Nombre, Apellido, Nacimiento, Direccion) values ('{0}','{1}','{2}','{3}')",
                   strNombre, strApellido, strNacimiento, strDireccion);

                SqlCommand comando = new SqlCommand(consulta, conectar);

                conectar.Open();
                comando.ExecuteNonQuery();
                conectar.Close();

                MessageBox.Show("Cliente " + txtNombre.Text + " " + txtApellido.Text + " registrado con exito", "Registro Exitoso");

            }


            else
            {

                string consulta = string.Format("Update clientes set Nombre = '{0}', Apellido =  '{1}' , Nacimiento =  '{2}', Direccion = '{3}' where IDCliente =  '{4}'",
               strNombre, strApellido, strNacimiento, strDireccion, IDCliente);

                SqlCommand comando = new SqlCommand(consulta, conectar);

                conectar.Open();
                comando.ExecuteNonQuery();
                conectar.Close();

                MessageBox.Show("Cliente " + txtNombre.Text + " " + txtApellido.Text + " modificado con exito", "Modificación Exitosa");

            }

            reestablecer();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            reestablecer();
        }

        private void btEliminar_Click(object sender, EventArgs e)
        {
            if (IDCliente.Equals(0))
            {
                MessageBox.Show("Seleccione un cliente", "Atención");
            }
            else
            {
                if (MessageBox.Show("¿Está seguro que desea eliminar el cliente?", "Eliminar Cliente", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    String consulta = "DELETE from clientes where IDCliente= '" + IDCliente + "'";
                    SqlCommand comando = new SqlCommand(consulta, conectar);

                    conectar.Open();
                    comando.ExecuteNonQuery();
                    conectar.Close();

                    reestablecer();
                }
            }
        }

        private void dtvTabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtvTabla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IDCliente = Convert.ToInt32(dtvTabla.CurrentRow.Cells[0].Value);
            txtNombre.Text = Convert.ToString(dtvTabla.CurrentRow.Cells[1].Value);
            txtApellido.Text = Convert.ToString(dtvTabla.CurrentRow.Cells[2].Value);

            dtmNacimiento.Value = Convert.ToDateTime(dtvTabla.CurrentRow.Cells[3].Value);
            txtDireccion.Text = Convert.ToString(dtvTabla.CurrentRow.Cells[4].Value);

            btnGuardar.Text = "Guardar";
        }

        public void reestablecer()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            dtmNacimiento.Value = System.DateTime.Today;
            txtDireccion.Text = "";
            IDCliente = 0;
            btnGuardar.Text = "Nuevo";

            mostrarRegistro();
        }

        private void mostrarRegistro()
        {
            List<Cliente> lista = new List<Cliente>();

            
            String consulta = "SELECT IDCliente, Nombre, Apellido, Nacimiento, Direccion FROM clientes";
            SqlCommand comando = new SqlCommand(consulta, conectar);

            conectar.Open();
            SqlDataReader reader = comando.ExecuteReader();

            while (reader.Read()) 
            {
                Cliente pCliente = new Cliente();
                pCliente.Id = reader.GetInt32(0);
                pCliente.Nombre = reader.GetString(1);
                pCliente.Apellido = reader.GetString(2);
                pCliente.Fecha_Nac = reader.GetString(3);
                pCliente.Direccion = reader.GetString(4);


                lista.Add(pCliente);
            }

            conectar.Close();
            btnGuardar.DataSource = lista;

        }
    }
}
