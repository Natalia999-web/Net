using System;

namespace Clase1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // === EJERCICIO 1 ===
            // Préstamo con interés anual del 5% a 5 años

            Console.WriteLine("=== EJERCICIO 1: PRÉSTAMO ===");
            Console.Write("Ingrese el monto del préstamo: ");
            decimal prestamo = decimal.Parse(Console.ReadLine());

            decimal tasaAnual = 0.05m;
            decimal interesAnual = prestamo * tasaAnual;
            decimal interesTrimestre = interesAnual / 4;
            decimal interesMensual = interesAnual / 12;
            decimal totalConIntereses = prestamo + (interesAnual * 5);

            Console.WriteLine($"Interés pagado en un año: {interesAnual:C}");
            Console.WriteLine($"Interés en el tercer trimestre: {interesTrimestre:C}");
            Console.WriteLine($"Interés del primer mes: {interesMensual:C}");
            Console.WriteLine($"Total a pagar en 5 años (con intereses): {totalConIntereses:C}");

            Console.WriteLine("\n--- Presione ENTER para continuar al siguiente ejercicio ---");
            Console.ReadLine();

            // === EJERCICIO 2 ===
            // Colilla de pago del empleado

            Console.WriteLine("=== EJERCICIO 2: COLILLA DE PAGO ===");
            const decimal EPS = 0.125m;
            const decimal PENSION = 0.16m;

            Console.Write("Ingrese el salario del empleado: ");
            decimal salario = decimal.Parse(Console.ReadLine());

            Console.Write("Ingrese el valor de ahorro mensual programado: ");
            decimal ahorro = decimal.Parse(Console.ReadLine());

            decimal deduccionEPS = salario * EPS;
            decimal deduccionPension = salario * PENSION;
            decimal totalRecibir = salario - deduccionEPS - deduccionPension - ahorro;

            Console.WriteLine($"\nSalario base: {salario:C}");
            Console.WriteLine($"Aporte EPS (12.5%): {deduccionEPS:C}");
            Console.WriteLine($"Aporte Pensión (16%): {deduccionPension:C}");
            Console.WriteLine($"Ahorro mensual: {ahorro:C}");
            Console.WriteLine($"Total a recibir: {totalRecibir:C}");

            Console.WriteLine("\n--- Presione ENTER para continuar al siguiente ejercicio ---");
            Console.ReadLine();

            // === EJERCICIO 3 ===
            // Cuotas de matrícula

            Console.WriteLine("=== EJERCICIO 3: MATRÍCULA ===");

            decimal cuota1 = 0.40m, cuota2 = 0.25m, cuota3 = 0.20m, cuota4 = 0.15m;

            Console.Write("Ingrese el valor total de la matrícula: ");
            decimal matricula = decimal.Parse(Console.ReadLine());

            Console.WriteLine($"\nPrimera cuota (40%): {matricula * cuota1:C}");
            Console.WriteLine($"Segunda cuota (25%): {matricula * cuota2:C}");
            Console.WriteLine($"Tercera cuota (20%): {matricula * cuota3:C}");
            Console.WriteLine($"Cuarta cuota (15%): {matricula * cuota4:C}");
            Console.WriteLine($"Total a pagar: {matricula:C}");

            Console.WriteLine("\n--- Presione ENTER para continuar al siguiente ejercicio ---");
            Console.ReadLine();

            // === EJERCICIO 4 ===
            // Calcular edad e información personal

            Console.WriteLine("=== EJERCICIO 4: INFORMACIÓN PERSONAL ===");
            Console.Write("Ingrese su nombre: ");
            string nombre = Console.ReadLine();

            Console.Write("Ingrese su dirección: ");
            string direccion = Console.ReadLine();

            Console.Write("Ingrese su año de nacimiento: ");
            int nacimiento = int.Parse(Console.ReadLine());

            int añoActual = DateTime.Now.Year;
            int edad = añoActual - nacimiento;

            Console.WriteLine($"\nNombre: {nombre}");
            Console.WriteLine($"Dirección: {direccion}");
            Console.WriteLine($"Año de nacimiento: {nacimiento}");
            Console.WriteLine($"Edad actual: {edad} años");

            Console.WriteLine("\n--- Fin del programa ---");
            Console.ReadLine();
        }
    }
}
