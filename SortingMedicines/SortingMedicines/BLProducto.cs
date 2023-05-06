using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SortingMedicines
{
    public class BLProducto
    {
        private ProductoModel[] a_productosArray;
        private int a_contador;

        public BLProducto(int a_capacidad)
        {
            a_productosArray = new ProductoModel[a_capacidad];
            a_contador = 0;
        }

        public void RegistrarProducto(ProductoModel a_product)
        {
            if (a_contador < a_productosArray.Length)
            {
                a_productosArray[a_contador] = a_product;
                a_contador++;
            }
            else
            {
                MessageBox.Show("Se superó la capacidad permitida de registros.");
            }
        }

        public ProductoModel BuscarProductoPorNombre(string a_nombreProducto)
        {
            for (int a_i = 0; a_i < a_productosArray.Length; a_i++)
            {
                if (a_productosArray[a_i].Nombre == null) continue;
                if (a_productosArray[a_i].Nombre.ToLower() == a_nombreProducto.ToLower())
                {
                    return a_productosArray[a_i];
                }
            }
            
            return new ProductoModel();
        }

        public ProductoModel[] ObtenerProductosYOrdenar()
        {
            if (a_contador <= 1) return a_productosArray;

            for (int a_i = 0; a_i < a_productosArray.Length - 1; a_i++)
            {
                for (int a_j = a_i + 1; a_j < a_productosArray.Length; a_j++)
                {
                    if (a_productosArray[a_j].Nombre == null) break;
                    if (a_productosArray[a_i].Nombre.ToLower().CompareTo(a_productosArray[a_j].Nombre.ToLower()) > 0)
                    {
                        ProductoModel a_temp = a_productosArray[a_i];
                        a_productosArray[a_i] = a_productosArray[a_j];
                        a_productosArray[a_j] = a_temp;
                    }
                }
            }

            return a_productosArray;
        }

        public int EliminarProducto(string a_codigo)
        {
            int a_indice = -1;

            for (int a_i = 0; a_i < a_contador; a_i++)
            {
                if (a_productosArray[a_i].Codigo.ToLower() == a_codigo.ToLower())
                {
                    a_indice = a_i;
                    break;
                }
            }

            if (a_indice < 0) return a_indice;

            for (int i = a_indice; i < a_contador - 1; i++)
            {
                a_productosArray[i] = a_productosArray[i + 1];
            }

            a_contador--;

            a_productosArray[a_contador] = new ProductoModel();

            return a_indice;
        }
    }
}
