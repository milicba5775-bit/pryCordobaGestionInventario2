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
            clsConexionBD clsConexionBD = new clsConexionBD();
            clsConexionBD.ConectarBD();

            clsConexionBD.CargarCategoria(cboCategoria);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            clsConexionBD clsConexionBD = new clsConexionBD();
            clsConexionBD.ConectarBD();

            Int32 id = 0;
            Int32 categoria = Convert.ToInt32(cboCategoria.Text);
            String nombre = Convert.ToString(txtNombre.Text);
            string descripcion = Convert.ToString(txtDescripcion.Text);

            clsConexionBD.AgregarProductos(id,categoria,nombre,descripcion);
        }
    }
}
