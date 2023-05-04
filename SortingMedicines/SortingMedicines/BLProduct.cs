using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SortingMedicines
{
    public class BLProduct
    {
        private ProductModule[] products;
        private int contador;

        public BLProduct(int capacidad)
        {
            products = new ProductModule[capacidad];
            contador = 0;
        }

        public void RegisterProduct(ProductModule product)
        {
            if (contador < products.Length)
            {
                products[contador] = product;
                contador++;
            }
            else
            {
                MessageBox.Show("Se superó la capacidad permitida de registros.");
            }
        }

        public ProductModule[] SearchProduct(string productName)
        {
            //return products.Where(p => p.Nombre.Contains(productName)).ToArray();
            List<ProductModule> lstProducts = new List<ProductModule>();
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].Nombre == productName)
                {
                    lstProducts.Add(products[i]);
                }
            }

            return lstProducts.ToArray();
        }

        public ProductModule[] GetProducts()
        {
            if (contador <= 1) return products;

            ProductModule[] productosOrdenados = new ProductModule[products.Length];
            Array.Copy(products, productosOrdenados, products.Length);

            for (int i = 0; i < productosOrdenados.Length - 1; i++)
            {
                for (int j = i + 1; j < productosOrdenados.Length; j++)
                {
                    if ((productosOrdenados[i]?.Nombre ?? string.Empty).CompareTo(productosOrdenados[j]?.Nombre ?? string.Empty) > 0)
                    {
                        ProductModule temp = productosOrdenados[i];
                        productosOrdenados[i] = productosOrdenados[j];
                        productosOrdenados[j] = temp;
                    }
                }
            }

            return productosOrdenados;
        }

        public string GenerateCode()
        {
            return "COD" + (contador + 1).ToString($"D{5}");
        }
    }
}
