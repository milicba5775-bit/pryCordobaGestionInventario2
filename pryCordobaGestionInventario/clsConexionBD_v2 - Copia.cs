using System;
using System.Collections.Generic;
using System.Data;
//para conexion de ACcess
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public SqlDataReader BuscarProducto(string codigo)
        {
            comandoBaseDatos = new SqlCommand();
            comandoBaseDatos.Connection = coneccionBaseDatos;
            comandoBaseDatos.CommandType = System.Data.CommandType.Text;
            comandoBaseDatos.CommandText = $"SELECT codigo, nombre, categoria, precio, stock, descripcion FROM Productos WHERE codigo = '{codigo}'";
            lectorDataReader = comandoBaseDatos.ExecuteReader();
            return lectorDataReader;
        }

        public void EliminarProducto(string codigo)
        {
            comandoBaseDatos = new SqlCommand();
            comandoBaseDatos.Connection = coneccionBaseDatos;
            comandoBaseDatos.CommandType = System.Data.CommandType.Text;
            comandoBaseDatos.CommandText = $"DELETE FROM Productos WHERE codigo = '{codigo}'";
            comandoBaseDatos.ExecuteNonQuery();
        }

        public void ModificarProducto(string codigo, string nombre, string categoria, decimal precio, Int32 stock, string descripcion)
        {
            comandoBaseDatos = new SqlCommand();
            comandoBaseDatos.Connection = coneccionBaseDatos;
            comandoBaseDatos.CommandType = System.Data.CommandType.Text;
            comandoBaseDatos.CommandText = "UPDATE Productos SET nombre = @nombre, categoria = @categoria, precio = @precio, stock = @stock, descripcion = @descripcion WHERE codigo = @codigo";

            comandoBaseDatos.Parameters.AddWithValue("@codigo", codigo);
            comandoBaseDatos.Parameters.AddWithValue("@nombre", nombre);
            comandoBaseDatos.Parameters.AddWithValue("@categoria", categoria);
            comandoBaseDatos.Parameters.AddWithValue("@precio", precio);
            comandoBaseDatos.Parameters.AddWithValue("@stock", stock);
            comandoBaseDatos.Parameters.AddWithValue("@descripcion", descripcion);

            comandoBaseDatos.ExecuteNonQuery();
        }

        public DataTable BuscarProductos(string consultaSQL)
        {
            DataTable dt = new DataTable();
            SqlCommand comandoLocal = new SqlCommand(consultaSQL, coneccionBaseDatos);
            SqlDataAdapter da = new SqlDataAdapter(comandoLocal);

            try
            {
                da.Fill(dt);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al ejecutar la consulta SQL: " + error.Message);
            }
            return dt;
        }
    }
}
