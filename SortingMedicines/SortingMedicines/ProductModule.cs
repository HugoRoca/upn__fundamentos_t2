﻿namespace SortingMedicines
{
    public class ProductModule
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }

        public ProductModule(string codigo, string nombre, int cantidad, double precio)
        {
            Codigo = codigo;
            Nombre = nombre;
            Cantidad = cantidad;
            Precio = precio;
        }

        public double MontoInvertido => Cantidad* Precio;
    }
}
