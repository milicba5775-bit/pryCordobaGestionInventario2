using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace pryCordobaGestionInventario
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            clsConexionBDcopia clsConexionBD = new clsConexionBDcopia();
            clsConexionBD.ConectarBD();

            clsConexionBD.CargarCategoria(cboCategoria);
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;

            optCodigo.Checked = true;
            optNombre.Checked = true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try 
            {
                clsConexionBDcopia clsConexionBD = new clsConexionBDcopia();
                clsConexionBD.ConectarBD();

                string codigo = txtCodigo.Text;
                string nombre = txtNombre.Text;
                string categoria = cboCategoria.Text;
                decimal precio = Convert.ToDecimal(txtPrecio.Text);
                Int32 stock = Convert.ToInt32(txtStock.Text);    
                string descripcion = txtDescripcion.Text;

                clsConexionBD.AgregarProductos(codigo, nombre, categoria, precio, stock, descripcion);

                MessageBox.Show("Producto Agregado exitosamente.");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                clsConexionBDcopia clsConexionBD = new clsConexionBDcopia();
                clsConexionBD.ConectarBD();

                string codigo = txtCodigo.Text;

                clsConexionBD.EliminarProducto(codigo);

                MessageBox.Show("Producto eliminado exitosamente.");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar eliminar el producto: " + ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                clsConexionBDcopia clsConexionBD = new clsConexionBDcopia();
                clsConexionBD.ConectarBD();

                string codigo = txtCodigo.Text;

                // Llama al método de búsqueda y almacena los resultados
                SqlDataReader lector = clsConexionBD.BuscarProducto(codigo);

                // Si se encuentra un producto, llena los campos
                if (lector.Read())
                {
                    txtCodigo.Text = lector["codigo"].ToString();
                    txtNombre.Text = lector["nombre"].ToString();
                    cboCategoria.Text = lector["categoria"].ToString();
                    txtPrecio.Text = lector["precio"].ToString();
                    txtStock.Text = lector["stock"].ToString();
                    txtDescripcion.Text = lector["descripcion"].ToString();

                    txtCodigo.Enabled = false;

                    btnModificar.Enabled = true;
                    btnEliminar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Producto no encontrado.");
                    LimpiarCampos();
                }

                lector.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el producto: " + ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            cboCategoria.SelectedIndex = -1;
            txtPrecio.Text = "";
            txtStock.Text = "";
            txtDescripcion.Text = "";
            txtCodigo.Enabled = true; 
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                clsConexionBDcopia clsConexionBD = new clsConexionBDcopia();
                clsConexionBD.ConectarBD();

                string codigo = txtCodigo.Text;
                string nombre = txtNombre.Text;
                string categoria = cboCategoria.Text;
                decimal precio = Convert.ToDecimal(txtPrecio.Text);
                Int32 stock = Convert.ToInt32(txtStock.Text);
                string descripcion = txtDescripcion.Text;

                clsConexionBD.ModificarProducto(codigo, nombre, categoria, precio, stock, descripcion);

                MessageBox.Show("Producto modificado exitosamente.");
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar modificar el producto: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string valor = txtValor.Text.Trim();
            string campo = ObtenerCampoBusqueda();
            string consultaSQL = "";

            if (string.IsNullOrEmpty(campo))
            {
                MessageBox.Show("Seleccione un criterio de búsqueda (Código, Nombre o Categoría).", "Advertencia");
                return;
            }

            if (string.IsNullOrEmpty(valor))
            {
                MessageBox.Show("Ingrese un valor para el campo de búsqueda.", "Advertencia");
                return;
            }

            if (campo == "codigo")
            {
                consultaSQL = $"SELECT codigo, nombre, categoria, precio, stock, descripcion FROM Productos WHERE codigo = '{valor}'";
            }
            else 
            {
                consultaSQL = $"SELECT codigo, nombre, categoria, precio, stock, descripcion FROM Productos WHERE {campo} LIKE '%{valor}%'";
            }

            try
            {
                clsConexionBDcopia clsConexionBD = new clsConexionBDcopia();
                clsConexionBD.ConectarBD();

                DataTable resultados = clsConexionBD.BuscarProductos(consultaSQL);

                dgvProducto.DataSource = null;
                dgvProducto.DataSource = resultados;
                dgvProducto.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                if (resultados.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron productos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar la búsqueda: " + ex.Message, "Error");
            }
        }
        private string ObtenerCampoBusqueda()
        {
            if (optCodigo.Checked)
            {
                return "codigo";
            }
            else if (optNombre.Checked)
            {
                return "nombre";
            }
            else if (optCategoria.Checked)
            {
                return "categoria";
            }
            return string.Empty; 
        }


        private void GenerarReporteInventario()
        {
            clsConexionBDcopia clsConexionBD = new clsConexionBDcopia();
            clsConexionBD.ConectarBD();

            string consultaSQL = "SELECT nombre, SUM(stock) AS TotalStock FROM Productos GROUP BY nombre";
            DataTable dt = clsConexionBD.BuscarProductos(consultaSQL);

            chartInventario.Series.Clear();
            chartInventario.Titles.Clear();

            chartInventario.Titles.Add("Reporte de Inventario");

            Series serie = new Series("Stock");
            serie.ChartType = SeriesChartType.Column; 
            serie.IsValueShownAsLabel = true;

            foreach (DataRow fila in dt.Rows)
            {
                serie.Points.AddXY(fila["nombre"].ToString(), Convert.ToInt32(fila["TotalStock"]));
            }

            chartInventario.Series.Add(serie);

            chartInventario.ChartAreas[0].AxisX.Interval = 1; 
            chartInventario.ChartAreas[0].AxisX.LabelStyle.Angle = -45; 
            chartInventario.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            chartInventario.ChartAreas[0].AxisX.MajorGrid.Enabled = false; 

        }

        private void btnGenerar_Click_1(object sender, EventArgs e)
        {
            GenerarReporteInventario();
        }

        private void dgvProducto_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (dgvProducto.Columns[e.ColumnIndex].Name == "stock" && e.Value != null)
            {
                int stock = Convert.ToInt32(e.Value);

                if (stock < 3)
                {
                    e.CellStyle.BackColor = Color.Red;
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.Font = new Font(dgvProducto.Font, FontStyle.Bold);
                }
                else if (stock > 10)
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                    e.CellStyle.ForeColor = Color.Black;
                }
                else
                {
                    e.CellStyle.BackColor = Color.White;
                    e.CellStyle.ForeColor = Color.Black;
                }
            }
        }
    }
}
