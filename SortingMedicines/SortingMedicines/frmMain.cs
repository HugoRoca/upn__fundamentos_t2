using System;
using System.Windows.Forms;

namespace SortingMedicines
{
    public partial class frmMain : Form
    {
        BLProduct a_blProduct = new BLProduct(100);
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            frmEnabledOrDisabledComponents(false);
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (isCorrectDataForRegister())
            {
                ProductModule a_product = new ProductModule(
                txtCodigo.Text,
                txtNombre.Text,
                int.Parse(txtCantidad.Text),
                double.Parse(txtPrecio.Text));

                a_blProduct.RegisterProduct(a_product);
                MessageBox.Show("Producto registrado correctamente!");
                frmEnabledOrDisabledComponents(false);

                ProductModule[] products = a_blProduct.GetProductsAndSort();
                loadDataListView(products);
            } else
            {
                MessageBox.Show("Debe de completar todos los datos para registrar!");
                txtNombre.Focus();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ProductModule a_product = a_blProduct.SearchProductByName(txtNombreBuscar.Text);

            string a_message = "Producto NO encontrado!";

            if (a_product.Nombre != null)
            {
                a_message = string.Format("Producto encontrado con éxito: \n" +
                    "- Código: {0}\n" +
                    "- Nombre: {1}\n" +
                    "- Precio: S/. {2}\n" +
                    "- Cantidad: {3}", a_product.Codigo, a_product.Nombre, a_product.Precio, a_product.Cantidad);
            }

            MessageBox.Show(a_message);

            txtNombreBuscar.Clear();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            tcMain.SelectedIndex = 1;
        }

        private void btnLista_Click(object sender, EventArgs e)
        {
            tcMain.SelectedIndex = 0;
        }

        private void btnNuevo_1_Click(object sender, EventArgs e)
        {
            frmEnabledOrDisabledComponents(btnNuevo_1.Text == "Nuevo");
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarNumerosDecimales(sender, e);
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarNumerosDecimales(sender, e);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int a_indice = a_blProduct.DeleteProduct(txtCodigoBuscar.Text);

            if (a_indice >= 0)
            {
                MessageBox.Show("Producto eliminado correctamente!");
                ProductModule[] a_products = a_blProduct.GetProductsAndSort();
                loadDataListView(a_products);
            }
            else
            {
                MessageBox.Show("Produto no se encontro con ese código.");
            }

            txtCodigoBuscar.Clear();
        }

        private bool isCorrectDataForRegister()
        {
            return !(txtCodigo.Text == "" || txtNombre.Text == "" || txtCantidad.Text == "" || txtPrecio.Text == "");
        }

        private void frmEnabledOrDisabledComponents(bool a_val)
        {
            txtNombre.Enabled = a_val;
            txtPrecio.Enabled = a_val;
            txtCantidad.Enabled = a_val;
            btnNuevo_1.Text = a_val ? "Cancel" : "Nuevo";
            gbBusqueda.Enabled = !a_val;
            gbEliminar.Enabled = !a_val;
            btnRegistrar.Enabled = a_val;
            btnLista.Enabled = !a_val;

            txtCantidad.Text = "";
            txtNombre.Text = "";
            txtPrecio.Text = "";

            txtNombre.Focus();

            if (btnNuevo_1.Text == "Nuevo")
            {
                txtCodigo.Text = generateCodeRandom();
            }
        }

        private string generateCodeRandom()
        {
            int a_zeros = 3;
            int a_maxNumber = (int)Math.Pow(10, a_zeros) - 1;
            int a_randomNumber = new Random().Next(a_maxNumber + 1);
            return "COD" + a_randomNumber.ToString($"D{a_zeros}");
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

        private void loadDataListView(ProductModule[] products)
        {
            lvDatos.Items.Clear();
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].Nombre == null) continue;

                lvDatos.Items.Add(string.Format("{0} - {1} - {2} unidades - S/. {3} - Valor: S/. {4}",
                    products[i].Codigo,
                    products[i].Nombre,
                    products[i].Cantidad,
                    products[i].Precio,
                    products[i].MontoInvertido));
            }
        }
    }
}
