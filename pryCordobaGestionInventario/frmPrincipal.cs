using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
