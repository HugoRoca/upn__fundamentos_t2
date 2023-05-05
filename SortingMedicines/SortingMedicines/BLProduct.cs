using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SortingMedicines
{
    public class BLProduct
    {
        private ProductModule[] a_products;
        private int a_contador;

        public BLProduct(int a_capacidad)
        {
            a_products = new ProductModule[a_capacidad];
            a_contador = 0;
        }

        public void RegisterProduct(ProductModule a_product)
        {
            if (a_contador < a_products.Length)
            {
                a_products[a_contador] = a_product;
                a_contador++;
            }
            else
            {
                MessageBox.Show("Se superó la capacidad permitida de registros.");
            }
        }

        public ProductModule SearchProductByName(string a_productName)
        {
            for (int i = 0; i < a_products.Length; i++)
            {
                if (a_products[i].Nombre == null) continue;
                if (a_products[i].Nombre.ToLower() == a_productName.ToLower())
                {
                    return a_products[i];
                }
            }
            
            return new ProductModule();
        }

        public ProductModule[] GetProductsAndSort()
        {
            if (a_contador <= 1) return a_products;

            for (int i = 0; i < a_products.Length - 1; i++)
            {
                for (int j = i + 1; j < a_products.Length; j++)
                {
                    if (a_products[j].Nombre == null) break;
                    if (a_products[i].Nombre.ToLower().CompareTo(a_products[j].Nombre.ToLower()) > 0)
                    {
                        ProductModule temp = a_products[i];
                        a_products[i] = a_products[j];
                        a_products[j] = temp;
                    }
                }
            }

            return a_products;
        }

        public int DeleteProduct(string a_code)
        {
            int a_indice = -1;

            for (int i = 0; i < a_contador; i++)
            {
                if (a_products[i].Codigo.ToLower() == a_code.ToLower())
                {
                    a_indice = i;
                    break;
                }
            }

            if (a_indice < 0) return a_indice;

            for (int i = a_indice; i < a_contador - 1; i++)
            {
                a_products[i] = a_products[i + 1];
            }

            a_contador--;

            a_products[a_contador] = new ProductModule();

            return a_indice;
        }
    }
}
