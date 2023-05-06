using System;
using System.Windows.Forms;

namespace SortingMedicines
{
    public partial class frmMain : Form
    {
        BLProducto a_blProduct = new BLProducto(100);
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            frmHabilitarDeshabilitarControles(false);
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (ValidarTextboxesParaRegistrar())
            {
                ProductoModel a_product = new ProductoModel(
                txtCodigo.Text,
                txtNombre.Text,
                int.Parse(txtCantidad.Text),
                double.Parse(txtPrecio.Text));

                a_blProduct.RegistrarProducto(a_product);
                MessageBox.Show("Producto registrado correctamente!");

                frmHabilitarDeshabilitarControles(false);

                ProductoModel[] products = a_blProduct.ObtenerProductosYOrdenar();
                CargarListView(products);
            } else
            {
                MessageBox.Show("Debe de completar todos los datos para registrar!");
                txtNombre.Focus();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ProductoModel a_product = a_blProduct.BuscarProductoPorNombre(txtNombreBuscar.Text);

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
            frmHabilitarDeshabilitarControles(btnNuevo_1.Text == "Nuevo");
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
            int a_indice = a_blProduct.EliminarProducto(txtCodigoBuscar.Text);

            if (a_indice >= 0)
            {
                MessageBox.Show("Producto eliminado correctamente!");
                ProductoModel[] a_products = a_blProduct.ObtenerProductosYOrdenar();
                CargarListView(a_products);
            }
            else
            {
                MessageBox.Show("Produto no se encontro con ese código.");
            }

            txtCodigoBuscar.Clear();
        }

        private bool ValidarTextboxesParaRegistrar()
        {
            return !(txtCodigo.Text == "" || txtNombre.Text == "" || txtCantidad.Text == "" || txtPrecio.Text == "");
        }

        private void frmHabilitarDeshabilitarControles(bool a_valor)
        {
            txtNombre.Enabled = a_valor;
            txtPrecio.Enabled = a_valor;
            txtCantidad.Enabled = a_valor;
            btnNuevo_1.Text = a_valor ? "Cancelar" : "Nuevo";
            gbBusqueda.Enabled = !a_valor;
            gbEliminar.Enabled = !a_valor;
            btnRegistrar.Enabled = a_valor;
            btnLista.Enabled = !a_valor;

            txtCantidad.Text = "";
            txtNombre.Text = "";
            txtPrecio.Text = "";

            txtNombre.Focus();

            if (btnNuevo_1.Text == "Nuevo")
            {
                txtCodigo.Text = GenegarCodigoRandom();
            }
        }

        private string GenegarCodigoRandom()
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

        private void CargarListView(ProductoModel[] a_productos)
        {
            lvDatos.Items.Clear();
            for (int a_i = 0; a_i < a_productos.Length; a_i++)
            {
                if (a_productos[a_i].Nombre == null) continue;

                lvDatos.Items.Add(string.Format("{0} - {1} - {2} unidades - S/. {3} - Valor: S/. {4}",
                    a_productos[a_i].Codigo,
                    a_productos[a_i].Nombre,
                    a_productos[a_i].Cantidad,
                    a_productos[a_i].Precio,
                    a_productos[a_i].MontoInvertido));
            }
        }
    }
}
