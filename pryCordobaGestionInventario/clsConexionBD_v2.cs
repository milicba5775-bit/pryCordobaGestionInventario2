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
    internal class clsConexionBD
    {
        //cadena de conexion
        //sql - string cadenaConexion = "Server=localhost;Database=Ventas2;Trusted_Connection=True;";
        string cadenaConexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Alumno\\source\\repos\\pryCordobaGestionInventario2\\pryCordobaGestionInventario\\Base de Datos\\producto.accdb";
        //conector
        //SqlConnection coneccionBaseDatos;
        OleDbConnection coneccionBaseDatos;
        //comando
        //SqlCommand comandoBaseDatos;
        OleDbCommand comandoBaseDatos;

        OleDbDataReader lectorDataReader;

        public string nombreBaseDeDatos;

        public void ConectarBD()
        {
            try
            {
                //coneccionBaseDatos = new SqlConnection(cadenaConexion);
                coneccionBaseDatos = new OleDbConnection(cadenaConexion);

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
            comandoBaseDatos = new OleDbCommand();
            comandoBaseDatos.Connection = coneccionBaseDatos;
            comandoBaseDatos.CommandType = System.Data.CommandType.Text;

            comandoBaseDatos.CommandText = "SELECT marca_nombre FROM Productos";

            lectorDataReader = comandoBaseDatos.ExecuteReader();
            while (lectorDataReader.Read())
            {
                ListaCategoria.Items.Add(lectorDataReader[0]);
            
            }
        }

        public void AgregarProductos(Int32 id,Int32 categoria, String nombre, string descripcion)
        {
            comandoBaseDatos = new OleDbCommand();
            comandoBaseDatos.Connection = coneccionBaseDatos;
            comandoBaseDatos.CommandType = System.Data.CommandType.Text;
            comandoBaseDatos.CommandText = $"INSERT INTO Productos (id,categoria_de_producto, marca_nombre, observaciones) VALUES ({id},{categoria}, {nombre},{descripcion})";
            lectorDataReader = comandoBaseDatos.ExecuteReader();
        }


    }
}
