using MySql.Data.MySqlClient;

namespace Transacciones_Finanzas

{

    public partial class Balance_Cuenta : Form
    {
        public Balance_Cuenta()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void transferirAOtraCuentaDeBancoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Transacciones transacciones = new Transacciones();
            transacciones.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Parametros de la DB.
            string conexionString = "Server=127.0.0.1;Database=Control_Finanzas;Uid=root;Pwd=roxellflores772;";

            // Conexion DB.
            using (MySqlConnection conexion = new MySqlConnection(conexionString))
            {

                try
                {
                    conexion.Open();

                    string update_monto = "UPDATE Usuario SET Balance = Balance + @nuevoBalance WHERE No_Usuario = 1";

                    using (MySqlCommand incrementar_monto = new MySqlCommand(update_monto, conexion))
                    {
                        // Sustituye los parámetros con los valores reales
                        incrementar_monto.Parameters.AddWithValue("@nuevoBalance", Convert.ToDouble(Monto_TxtBox.Text)); // El nuevo valor viene del TextBox Monto_TxtBox.

                        // Ejecuta el comando
                        int rowsAffected = incrementar_monto.ExecuteNonQuery();


                        if (rowsAffected > 0)
                        {
                            string nuevo_balance = "SELECT Balance FROM Usuario WHERE No_Usuario = 1";

                            using (MySqlCommand consultar_balanceact = new MySqlCommand(nuevo_balance, conexion))
                            {
                                object balance_actualizado = consultar_balanceact.ExecuteScalar();

                                // Mostrar el valor actualizado en el Label
                                BalanceTxt.Visible = true;
                                BalanceTxt.Text = "$" + balance_actualizado.ToString();
                            }
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

        private void Salir_Btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Consultar_BalanceBtn_Click(object sender, EventArgs e)
        {
            // Parametros de la DB.
            string conexionString = "Server=127.0.0.1;Database=Control_Finanzas;Uid=root;Pwd=roxellflores772;";

            // Conexion DB.
            using (MySqlConnection conexion = new MySqlConnection(conexionString))
            {

                try
                {
                    conexion.Open();
                    string consulta_nombre = "SELECT Nombres FROM Usuario WHERE No_Usuario = 1";
                    string consulta_balance = "SELECT Balance FROM Usuario WHERE No_Usuario = 1";

                    using (MySqlCommand consultardatos = new MySqlCommand(consulta_nombre, conexion))
                    {
                        object nombre = consultardatos.ExecuteScalar();

                        // Mostrar nombre en la etiqueta.
                        Nombre_Txt.Visible = true;
                        Nombre_Txt.Text = nombre.ToString();
                    }

                    using (MySqlCommand consultar_balance = new MySqlCommand(consulta_balance, conexion))
                    {
                         object balance = consultar_balance.ExecuteScalar();

                         // Mostrar el valor del balance en la etiqueta.
                         BalanceTxt.Visible = true;
                         BalanceTxt.Text = "$" + balance.ToString();
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
