using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Transacciones_Finanzas
{
    public partial class Transacciones : Form
    {
        public Transacciones()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Regresar_Btn_Click(object sender, EventArgs e)
        {
            Balance_Cuenta balance_Cuentaform = new Balance_Cuenta();
            balance_Cuentaform.Show();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Banco_Txt.Visible = true;
            Banco_Txt.Text = "BBVA";
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Banco_Txt.Visible = true;
            Banco_Txt.Text = "HSBC";
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Banco_Txt.Visible = true;
            Banco_Txt.Text = "Santander";
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Banco_Txt.Visible = true;
            Banco_Txt.Text = "CitiBanamex";
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Banco_Txt.Visible = true;
            Banco_Txt.Text = "Invex";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Banco_Txt.Visible = true;
            Banco_Txt.Text = "Inbursa";
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Banco_Txt.Visible = true;
            Banco_Txt.Text = "NU";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Banco_Txt.Visible = true;
            Banco_Txt.Text = "Banco Azteca";
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Banco_Txt.Visible = true;
            Banco_Txt.Text = "Afirme";
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Banco_Txt.Visible = true;
            Banco_Txt.Text = "OTROS";
        }

        private void Transferir_Btn_Click(object sender, EventArgs e)
        {
            // Parametros de la DB.
            string conexionString = "Server=127.0.0.1;Database=Control_Finanzas;Uid=root;Pwd=roxellflores772;";

            // Conexion DB.
            using (MySqlConnection conexion = new MySqlConnection(conexionString))
            {

                try
                {
                    conexion.Open();
                    string transferencia_bancaria = "UPDATE Usuario SET Balance = Balance - @nuevoBalance WHERE No_Usuario = 1";

                    using (MySqlCommand restar_monto = new MySqlCommand(transferencia_bancaria, conexion))
                    {
                        // Sustituye los parámetros con los valores reales.
                        restar_monto.Parameters.AddWithValue("@nuevoBalance", Convert.ToDouble(MontoTransferencia_TxtBox.Text)); // El nuevo valor viene del TextBox Monto_TxtBox.

                        int rowsAffected = restar_monto.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Mostrar el estado de la transferencia.
                            Status_TransTxt.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el registro.");
                        }

                    }

                    string adicion_bancaria = "UPDATE Usuario SET Balance = Balance + @nuevoBalance WHERE No_Usuario = @No_Usuario";

                    using (MySqlCommand sumar_trans = new MySqlCommand(adicion_bancaria, conexion))
                    {
                        sumar_trans.Parameters.AddWithValue("@nuevoBalance", Convert.ToDouble(MontoTransferencia_TxtBox.Text)); // El nuevo valor viene del TextBox Monto_TxtBox.
                        sumar_trans.Parameters.AddWithValue("@No_Usuario", No_UsuarioReceptorTxtBox.Text); // El ID a actualizar también puede venir de un TextBox.

                        // Ejecuta el comando.
                        int rowsAffected = sumar_trans.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Mostrar el estado de la transferencia.
                            Status_TransTxt.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el registro.");
                        }
                    }
                }

                catch (MySqlException ex)
                {
                    MessageBox.Show("Error al conectar con la Base de Datos: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }
        }

        private void BuscarUsuario_Btn_Click(object sender, EventArgs e)
        {
            // Parametros de la DB.
            string conexionString = "Server=127.0.0.1;Database=Control_Finanzas;Uid=root;Pwd=roxellflores772;";

            // Conexion DB.
            using (MySqlConnection conexion = new MySqlConnection(conexionString))
            {

                try
                {   // Consultar cuentas.
                    conexion.Open();
                    string consulta_cuentaEMISORA = "SELECT Nombres FROM Usuario WHERE No_Usuario = @No_Usuario";
                    string consulta_cuentaRECEPTORA = "SELECT Nombres FROM Usuario WHERE No_Usuario = @No_Usuario";

                    using (MySqlCommand consultarcuentaemi = new MySqlCommand(consulta_cuentaEMISORA, conexion))
                    {

                        consultarcuentaemi.Parameters.AddWithValue("@No_Usuario", No_UsuarioEmiTxtBox.Text);

                        object consultar_nombre = consultarcuentaemi.ExecuteScalar();

                        if (string.IsNullOrWhiteSpace(No_UsuarioEmiTxtBox.Text))
                        {
                            MessageBox.Show("Por favor, llene todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        //Mostrar nombre del Usuario Emisor.
                        Nombre_EmiTxt.Visible = true;
                        Nombre_EmiTxt.Text = consultar_nombre.ToString();

                    }

                    using (MySqlCommand consultarcuentarec = new MySqlCommand(consulta_cuentaRECEPTORA,conexion))
                    {
                        consultarcuentarec.Parameters.AddWithValue("@No_Usuario", No_UsuarioReceptorTxtBox.Text);
                        object consultar_nombre = consultarcuentarec.ExecuteScalar();

                        if (string.IsNullOrWhiteSpace(No_UsuarioReceptorTxtBox.Text))
                        {
                            MessageBox.Show("Por favor, llene todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        //Mostrar nombre del Usuario Receptor.
                        Nombre_Rec.Visible = true;
                        Nombre_Rec.Text = consultar_nombre.ToString();
                    }
                }

                catch (MySqlException ex)
                {
                    MessageBox.Show("Error al conectar con la Base de Datos: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }
        }

        private void DatosCorrectos_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DatosCorrectos_CheckBox.Checked == false)
            {
                MessageBox.Show("Favor de REVISAR los datos antes de continuar");
                Status_TransTxt.Visible = true;
                Status_TransTxt.ForeColor = Color.Red;
                Status_TransTxt.Text = "Datos incorrectos.";
            }
        }
    }
}
