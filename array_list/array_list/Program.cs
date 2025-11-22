using System;
using System.Collections.Generic;

namespace array_list
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Ejemplo 1: uso de un arreglo
            // Un array tiene un tamaño fijo (aquí de 3 números)
            int[] numeros = new int[3];
            for (int i = 0; i < 3; i++)
            {
                Console.Write("Ingrese número " + (i + 1) + ": ");
                numeros[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("\nNúmeros ingresados:");
            foreach (var item in numeros)
            {
                Console.WriteLine(item);
            }

            int suma = 0;
            for (int i = 0; i < 3; i++)
            {
                suma += numeros[i];
            }
            Console.WriteLine("La suma de los números es: " + suma);
            Console.WriteLine("---------------------------------------");


            // Ejemplo 2: uso de una lista
            // Las listas son más flexibles que los arrays: pueden crecer o reducirse
            List<int> listaNumeros = new List<int>();
            listaNumeros.Add(10);
            listaNumeros.Add(20);
            listaNumeros.Add(30);

            Console.WriteLine("\nNúmeros en la lista:");
            foreach (int item in listaNumeros)
            {
                Console.WriteLine(item);
            }

            // Acceder a un elemento por su índice
            int primerNumero = listaNumeros[1];
            Console.WriteLine($"El número en la posición 1 es: {primerNumero}");

            // Modificar un elemento
            listaNumeros[2] = 50;
            Console.WriteLine($"Número modificado: {listaNumeros[2]}");

            // Insertar un número en una posición específica
            listaNumeros.Insert(2, 15);
            Console.WriteLine($"Nuevo valor en la posición 2: {listaNumeros[2]}");

            // Eliminar elementos
            listaNumeros.RemoveAt(1); // Elimina por posición
            listaNumeros.Remove(10);  // Elimina por valor
            Console.WriteLine("---------------------------------------");


            // Ejercicio principal: administración de productos
            List<string> productos = new List<string>();
            List<decimal> precios = new List<decimal>();

            while (true)
            {
                Console.WriteLine("\nMenú de opciones:");
                Console.WriteLine("1. Agregar producto");
                Console.WriteLine("2. Actualizar producto");
                Console.WriteLine("3. Eliminar producto");
                Console.WriteLine("4. Mostrar productos");
                Console.WriteLine("5. Salir");
                Console.Write("Elija una opción: ");
                int opcion = int.Parse(Console.ReadLine());
                Console.WriteLine();

                if (opcion == 1)
                {
                    Console.Write("Ingrese el nombre del producto: ");
                    string nombre = Console.ReadLine();

                    Console.Write("Ingrese el precio del producto: ");
                    decimal precio = decimal.Parse(Console.ReadLine());

                    productos.Add(nombre);
                    precios.Add(precio);

                    Console.WriteLine("Producto agregado correctamente.");
                }
                else if (opcion == 2)
                {
                    if (productos.Count == 0)
                    {
                        Console.WriteLine("No hay productos para actualizar.");
                        continue;
                    }

                    Console.WriteLine("Productos en la lista:");
                    for (int i = 0; i < productos.Count; i++)
                    {
                        Console.WriteLine($"{i}. {productos[i]} - Precio: ${precios[i]}");
                    }

                    Console.Write("\nIngrese el número del producto que desea modificar: ");
                    int indice = int.Parse(Console.ReadLine());

                    if (indice < 0 || indice >= productos.Count)
                    {
                        Console.WriteLine("Número inválido.");
                        continue;
                    }

                    Console.WriteLine($"Producto seleccionado: {productos[indice]} - Precio: ${precios[indice]}");

                    Console.Write("Ingrese el nuevo nombre (o presione Enter para dejar el mismo): ");
                    string nuevoNombre = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(nuevoNombre))
                    {
                        productos[indice] = nuevoNombre;
                    }

                    Console.Write("Ingrese el nuevo precio (o presione Enter para dejar el mismo): ");
                    string nuevoPrecio = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(nuevoPrecio))
                    {
                        if (decimal.TryParse(nuevoPrecio, out decimal precioDecimal))
                        {
                            precios[indice] = precioDecimal;
                        }
                        else
                        {
                            Console.WriteLine("Precio inválido. Se mantiene el anterior.");
                        }
                    }

                    Console.WriteLine("Producto actualizado correctamente.");
                }
                else if (opcion == 3)
                {
                    if (productos.Count == 0)
                    {
                        Console.WriteLine("No hay productos para eliminar.");
                        continue;
                    }

                    Console.WriteLine("Productos en la lista:");
                    for (int i = 0; i < productos.Count; i++)
                    {
                        Console.WriteLine($"{i}. {productos[i]} - Precio: ${precios[i]}");
                    }

                    Console.Write("\nIngrese el número del producto a eliminar: ");
                    string input = Console.ReadLine();

                    bool esNumero = int.TryParse(input, out int numDelete);

                    if (esNumero && numDelete >= 0 && numDelete < productos.Count)
                    {
                        productos.RemoveAt(numDelete);
                        precios.RemoveAt(numDelete);
                        Console.WriteLine("Producto eliminado correctamente.");
                    }
                    else
                    {
                        Console.WriteLine("Número inválido.");
                    }
                }
                else if (opcion == 4)
                {
                    if (productos.Count == 0)
                    {
                        Console.WriteLine("No hay productos en la lista.");
                    }
                    else
                    {
                        Console.WriteLine("Productos actuales:");
                        for (int i = 0; i < productos.Count; i++)
                        {
                            Console.WriteLine($"{i}. {productos[i]} - Precio: ${precios[i]}");
                        }
                    }
                }
                else if (opcion == 5)
                {
                    Console.WriteLine("Saliendo del programa...");
                    break;
                }
                else
                {
                    Console.WriteLine("Opción inválida, intente de nuevo.");
                }
            }
        }
    }
}
