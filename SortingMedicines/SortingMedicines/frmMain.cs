using System;
using System.Windows.Forms;

namespace SortingMedicines
{
    public partial class frmMain : Form
    {
        BLProduct _blProduct = new BLProduct(100);
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            ProductModule product = new ProductModule(
                txtCodigo.Text,
                txtNombre.Text,
                int.Parse(txtCantidad.Text),
                double.Parse(txtPrecio.Text));

            _blProduct.RegisterProduct(product);
            MessageBox.Show("Producto registrado correctamente!");
            frmEnabledOrDisabledComponents(false);

            ProductModule[] products = _blProduct.GetProducts();
            loadDataListView(products);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            tcMain.SelectedIndex = 1;
        }

        private void btnLista_Click(object sender, EventArgs e)
        {
            tcMain.SelectedIndex = 0;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            frmEnabledOrDisabledComponents(false);
        }

        private void btnNuevo_1_Click(object sender, EventArgs e)
        {
            frmEnabledOrDisabledComponents(btnNuevo_1.Text == "Nuevo");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ProductModule[] products = _blProduct.SearchProduct(txtCodigoBuscar.Text);
            if (products.Length > 0)
            {
                MessageBox.Show("Producto encontrado!");
            }
        }

        private void frmEnabledOrDisabledComponents(bool val)
        {
            txtNombre.Enabled = val;
            txtPrecio.Enabled = val;
            txtCantidad.Enabled = val;
            btnNuevo_1.Text = val ? "Cancel" : "Nuevo";
            gbBusqueda.Enabled = !val;
            btnRegistrar.Enabled = val;

            txtCantidad.Text = "";
            txtNombre.Text = "";
            txtPrecio.Text = "";

            txtNombre.Focus();

            if (btnNuevo_1.Text == "Nuevo")
            {
                txtCodigo.Text = _blProduct.GenerateCode();
            }
        }

        private void loadDataListView(ProductModule[] products)
        {
            lvDatos.Items.Clear();
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i] == null) continue;

                lvDatos.Items.Add(string.Format("{0} - {1} - {2} unidades - S/. {3} - Valor: S/. {4}",
                    products[i].Codigo,
                    products[i].Nombre,
                    products[i].Cantidad,
                    products[i].Precio,
                    products[i].MontoInvertido));
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarNumerosDecimales(sender, e);
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarNumerosDecimales(sender, e);
        }

        private void validarNumerosDecimales(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.') && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if (((e.KeyChar == '.') || (e.KeyChar == ',')) && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
