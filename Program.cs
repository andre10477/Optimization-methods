using System;
using OptimizationLibrary;

namespace OPTConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {



            Console.WriteLine("Функция оптимизации(Function of optimization) - х^2");
            Console.WriteLine("Выберите метод оптимизации(Choose method of optimization): ");
            // Read the entire line and parse it to an integer
            int method;
            if (!int.TryParse(Console.ReadLine(), out method))
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                return;
            }
            Console.WriteLine();
            OptimizationLibrary.Optimization.MODO modo = new();
            OptimizationLibrary.Optimization.ODMMUR odmmur = new();



            double delta = 0;
            Console.WriteLine("Введите параметры методов(Enter parameters of method): ");
            Console.WriteLine("Параметр(Paramether) a: ");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Параметр(Paramether) b: ");
            double b = Convert.ToDouble(Console.ReadLine());
            //epsilon - can be written like 0.01 or 1e-02
            //epsilon - можно ввести как 0.01 или 1e-02
            Console.WriteLine("Параметр(Paramether) epsilon: ");
            double epsilon = Convert.ToDouble(Console.ReadLine());
            if (method == 1 || method == 2)
            {
                Console.WriteLine("Параметр(Paramether) delta: ");
                delta = Convert.ToDouble(Console.ReadLine());
            }            



            switch (method)
            {
                /// * Равномерного поиска
                /// * Метод Дихотомии
                /// * Метод Фибоначчи
                case 0: //равномерный поиск - uniform search
                    modo.UniformSearch(a, b, epsilon);
                    break;
                case 1: //метод дихотомии - dychotomy method
                    modo.Dichotomy(a, b, epsilon, delta);
                    break;
               case 2: //метод Фибоначчи - Fibonacchi method
                    modo.FibonacchiOptimization(a, b, epsilon, delta);
                    break;
                /// * Метод средней точки
                /// * Метод хорд
                case 3: //метод средней точки - middle point method
                    odmmur.MetodSredneytochki(a, b, epsilon);
                    break;
                case 4: //метод хорд - chord method
                    odmmur.MetodHord(a, b, epsilon);
                    break;
            }



            Console.WriteLine("Оптимизация завершена.");
        }
    }
}