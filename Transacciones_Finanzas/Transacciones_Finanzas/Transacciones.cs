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
            MontoTransferencia_TxtBox.KeyPress += MontoTransferencia_TxtBox_KeyPress;
            No_UsuarioEmiTxtBox.KeyPress += No_UsuarioEmiTxtBox_KeyPress;
            No_UsuarioReceptorTxtBox.KeyPress += No_UsuarioReceptorTxtBox_KeyPress;
        }

        private void No_UsuarioEmiTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si el carácter ingresado no es un número o si no es el carácter de control (como backspace).
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                // Si es así, cancela el evento para que el carácter no se ingrese en el TextBox.
                e.Handled = true;
            }
        }

        private void No_UsuarioReceptorTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si el carácter ingresado no es un número o si no es el carácter de control (como backspace).
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                // Si es así, cancela el evento para que el carácter no se ingrese en el TextBox.
                e.Handled = true;
            }
        }

        private void MontoTransferencia_TxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si el carácter ingresado no es un número o si no es el carácter de control (como backspace).
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                // Si es así, cancela el evento para que el carácter no se ingrese en el TextBox.
                e.Handled = true;
            }
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
                        //Validar que los TextBoxes existen.
                        if (string.IsNullOrWhiteSpace(No_UsuarioReceptorTxtBox.Text) || string.IsNullOrWhiteSpace(No_UsuarioReceptorTxtBox.Text) || string.IsNullOrWhiteSpace(MontoTransferencia_TxtBox.Text))
                        {
                            MessageBox.Show("Por favor, llene todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Status_TransTxt.Visible = true;
                            Status_TransTxt.ForeColor = Color.Red;
                            Status_TransTxt.Text = "¡Datos incorrectos!";
                        }
                        else
                        {
                            if (DatosCorrectos_CheckBox.Checked == false)
                            {
                                MessageBox.Show("Favor de REVISAR los datos antes de continuar");
                                Status_TransTxt.Visible = true;
                                Status_TransTxt.ForeColor = Color.Red;
                                Status_TransTxt.Text = "¡Datos incorrectos!";
                            }
                            else
                            {
                                // Sustituye los parámetros con los valores reales.
                                restar_monto.Parameters.AddWithValue("@nuevoBalance", Convert.ToDouble(MontoTransferencia_TxtBox.Text)); // El nuevo valor viene del TextBox Monto_TxtBox.

                                int rowsAffected = restar_monto.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    // Mostrar el estado de la transferencia.
                                    Status_TransTxt.Visible = true;
                                    Status_TransTxt.ForeColor = Color.Green;
                                    Status_TransTxt.Text = "¡Transferencia Exitosa!";
                                }
                                else
                                {
                                    MessageBox.Show("No se encontró el registro.");
                                }
                            }
                        }

                        string adicion_bancaria = "UPDATE Usuario SET Balance = Balance + @nuevoBalance WHERE No_Usuario = @No_Usuario";

                        using (MySqlCommand sumar_trans = new MySqlCommand(adicion_bancaria, conexion))
                        {
                            //Validar que los TextBoxes existen.
                            if (string.IsNullOrWhiteSpace(No_UsuarioReceptorTxtBox.Text) || string.IsNullOrWhiteSpace(No_UsuarioReceptorTxtBox.Text) || string.IsNullOrWhiteSpace(MontoTransferencia_TxtBox.Text))
                            {
                                MessageBox.Show("Por favor, llene todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
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
            // Parámetros de la DB.
            string conexionString = "Server=127.0.0.1;Database=Control_Finanzas;Uid=root;Pwd=roxellflores772;";

            // Conexión a la DB.
            using (MySqlConnection conexion = new MySqlConnection(conexionString))
            {
                try
                {
                    conexion.Open();

                    //Validar que los TextBoxes no esten vacios.
                    if (string.IsNullOrWhiteSpace(No_UsuarioEmiTxtBox.Text) && string.IsNullOrWhiteSpace(No_UsuarioReceptorTxtBox.Text))
                    {
                        MessageBox.Show("Por favor, llene todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        // Consulta SQL para verificar si el No_Usuario emisor existe.
                        string consultaUsuario = "SELECT Nombres FROM Usuario WHERE No_Usuario = @No_Usuario";

                        using (MySqlCommand consultar_registroEmisor = new MySqlCommand(consultaUsuario, conexion))
                        {
                            // Realiza las consultas de ambos usuario.
                            consultar_registroEmisor.Parameters.AddWithValue("@No_Usuario", No_UsuarioEmiTxtBox.Text);

                            // Ejecutar la consulta y obtener el resultado del Usuario Emisor.
                            object resultadoEmisor = consultar_registroEmisor.ExecuteScalar();

                            if (resultadoEmisor == null)
                            {
                                // El registro no existe.
                                MessageBox.Show("No se encontró el registro del No.Cuenta emisora. Favor de validar el No. Cuenta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Nombre_EmiTxt.Visible = false;
                            }
                            else
                            {
                                // Mostrar nombre del Usuario Emisor.
                                Nombre_EmiTxt.Visible = true;
                                Nombre_EmiTxt.Text = resultadoEmisor.ToString();
                            }
                        }
                        using (MySqlCommand consultar_registroReceptor = new MySqlCommand(consultaUsuario, conexion))
                        {
                            // Realiza las consulta del Usuario receptor.
                            consultar_registroReceptor.Parameters.AddWithValue("@No_Usuario", No_UsuarioReceptorTxtBox.Text);

                            // Ejecutar la consulta y obtener el resultado del Usuario Receptor.
                            object resultadoReceptor = consultar_registroReceptor.ExecuteScalar();

                            if (resultadoReceptor == null)
                            {
                                // El registro no existe.
                                MessageBox.Show("No se encontró el registro del No.Cuenta receptora. Favor de validar el No. Cuenta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Nombre_Rec.Visible = false;
                            }
                            else
                            {
                                // Mostrar nombre del Usuario Emisor.
                                Nombre_Rec.Visible = true;
                                Nombre_Rec.Text = resultadoReceptor.ToString();
                            }
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
    }       
}

