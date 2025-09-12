using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

//para conexion de ACcess
using System.Data.OleDb;

using System.Windows.Forms;

namespace pryCordobaGestionInventario
{
    internal class clsConexionBDcopia
    {
        //cadena de conexion
         string cadenaConexion = "Server=localhost;Database=gestion;Trusted_Connection=True;";
        //string cadenaConexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Alumno\\source\\repos\\pryCordobaGestionInventario2\\pryCordobaGestionInventario\\Base de Datos\\producto.accdb";
        //conector
        SqlConnection coneccionBaseDatos;
        //OleDbConnection coneccionBaseDatos;
        //comando
        SqlCommand comandoBaseDatos;
        //OleDbCommand comandoBaseDatos;

        SqlDataReader lectorDataReader;

        public string nombreBaseDeDatos;

        public void ConectarBD()
        {
            try
            {
                coneccionBaseDatos = new SqlConnection(cadenaConexion);
                //coneccionBaseDatos = new OleDbConnection(cadenaConexion);

                nombreBaseDeDatos = coneccionBaseDatos.Database;

                coneccionBaseDatos.Open();
                
               // MessageBox.Show("Conectado a " + nombreBaseDeDatos);
            }
            catch (Exception error)
            {
                MessageBox.Show("Tiene un errorcito - " + error.Message);
            }     

        }

        public void CargarCategoria(ComboBox ListaCategoria)
        {
            comandoBaseDatos = new SqlCommand();
            comandoBaseDatos.Connection = coneccionBaseDatos;
            comandoBaseDatos.CommandType = System.Data.CommandType.Text;

            comandoBaseDatos.CommandText = "SELECT nombre FROM productos";

            lectorDataReader = comandoBaseDatos.ExecuteReader();
            while (lectorDataReader.Read())
            {
                ListaCategoria.Items.Add(lectorDataReader[0]);
            
            }
        }

        public void AgregarProductos(string codigo, string nombre, string categoria, decimal precio, Int32 stock, string descripcion)
        {
            comandoBaseDatos = new SqlCommand();
            comandoBaseDatos.Connection = coneccionBaseDatos;
            comandoBaseDatos.CommandType = System.Data.CommandType.Text;
            comandoBaseDatos.CommandText = $"INSERT INTO Productos (codigo,nombre,categoria,precio,stock,descripcion) VALUES ('{codigo}','{nombre}','{categoria}', {precio}, {stock},'{descripcion}')";
            lectorDataReader = comandoBaseDatos.ExecuteReader();
        }


    }
}
