using System;
using static System.Math;

namespace OptimizationLibrary
{
    public class Optimization
    {
        private static double function1variable(double x) => x * x;
        //методы одномерной оптимизации
        //- methods of one-dimensional pptimization
        public sealed class MODO : Optimization             
        {
            /// <summary>
            /// В данном классе представлены следующие методы одномерной оптимизации:
            /// * Равномерного поиска
            /// * Метод Дихотомии
            /// * Метод Фибоначчи
            /// В будущем будут реализованы и другие методы ОМО
            /// 
            /// This class presents the following methods of one-dimensional optimization:
            /// * Uniform search
            /// * Dichotomy method
            /// * The Fibonacci method
            /// Further will be realized more methods of ODO
            /// </summary>

            public void UniformSearch(double a, double b, double epsilon) //метод равномерного поиска
            {
                int N = (int)((b - a) / epsilon); //количество точек равномерного разбиения
                double[] points = new double[N + 1]; //массив точек равномерного разбиения
                double min = double.MaxValue; //минимальное значение функции
                double minPoint = 0; //точка минимума

                //заполняем массив точек равномерного разбиения
                for (int i = 0; i <= N; i++)
                {
                    points[i] = a + i * (b - a) / N;
                    double f = function1variable(points[i]);
                    if (f < min)
                    {
                        min = f;
                        minPoint = points[i];
                    }
                }

                Console.WriteLine("Число итераций: {0}", N + 1);
                Console.WriteLine("Количество вычислений функции: {0}", N + 1);
                Console.WriteLine("Минимум функции: {0}, f(x) = {1}", minPoint, min);
            }



            public void Dichotomy(double a, double b, double epsilon, double delta) //метод дихотомии
            {
                int calls = 0, N = 0;
                //0 < delta < epsilon / 2;
                while ((b - a) >= epsilon)
                {
                    N++;
                    double x1 = (a + b) / 2 - delta;
                    double x2 = (a + b) / 2 + delta;
                    double f1 = function1variable(x1);
                    calls++;
                    double f2 = function1variable(x2);
                    calls++;
                    if (f1 < f2)
                    {
                        b = x2;
                    }
                    else
                    {
                        a = x1;
                    }
                }

                Console.WriteLine("Число итераций: {0}", N);
                Console.WriteLine("Количество вычислений функции: {0}", calls);
                Console.WriteLine("Минимум функции: {0}, f(x) = {1}", (a + b) / 2, function1variable((a + b) / 2));
            }



            static int Fibonacci(int n) //число Фибоначчи
            {
                if (n == 0 || n == 1) return n;

                return Fibonacci(n - 1) + Fibonacci(n - 2);
            }

            public void FibonacchiOptimization(double a, double b, double epsilon, double delta)// метод Фибоначчи
            {
                int calls = 0, iterations = 0;
                int n = 0;
                while (Fibonacci(n) <= (b - a) / epsilon)
                    n++;

                double x1 = a + (b - a) * Fibonacci(n - 2) / Fibonacci(n);
                double x2 = a + (b - a) * Fibonacci(n - 1) / Fibonacci(n);
                double f1 = function1variable(x1);
                double f2 = function1variable(x2);
                calls += 2; //учитываем два вызова функции при инициализации x1 и x2

                int k = 1;
                while (k < n - 1) //изменено с n - 2 на n - 1
                {
                    iterations++; //увеличиваем счетчик итераций

                    if (f1 > f2)
                    {
                        a = x1;
                        x1 = x2;
                        f1 = f2;
                        x2 = a + (b - a) * Fibonacci(n - k - 1) / Fibonacci(n - k);
                        f2 = function1variable(x2);
                        calls++; // учитываем вызов функции при вычислении f2
                    }
                    else
                    {
                        b = x2;
                        x2 = x1;
                        f2 = f1;
                        x1 = a + (b - a) * Fibonacci(n - k - 2) / Fibonacci(n - k);
                        f1 = function1variable(x1);
                        calls++; //учитываем вызов функции при вычислении f1
                    }
                    k++;
                }

                iterations++; //увеличиваем счетчик итераций за последнюю итерацию

                x2 = x1 + delta;
                if (function1variable(x1) > function1variable(x2))
                    a = x1;
                else
                    b = x2;

                //возвращаем оптимальное значение x и соответствующее ему значение функции y
                Console.WriteLine("Число итераций: {0}", iterations);
                Console.WriteLine("Количество вычислений функции: {0}", calls);
                Console.WriteLine("Минимум функции: {0}, f(x) = {1}", (a + b) / 2, function1variable((a + b) / 2));
            }
        }

        //методы одномерной оптимизации с использованием производной
        //- ONE-DIMENSIONAL METHODS MINIMIZATION USING THE DERIVATIVE
        public sealed class ODMMUR : Optimization   
        {
            /// <summary>
            /// В данном классе представлены следующие методы одномерной оптимизации
            /// с использованием первой производной:
            /// * Метод средней точки
            /// * Метод хорд
            /// В будущем будут реализованы и другие методы ОМО
            /// 
            /// This class presents the following methods of one-dimensional optimization
            /// with using of first derivative:
            /// * Middle point method
            /// * Chord method
            /// Further will be realized more methods of ODO
            /// </summary>

            double function(double x) => x + 1 / Math.Log(x);
            double derivative(double x) => 1 - 1 / (x * Math.Pow(Math.Log(x), 2));
            public void MetodSredneytochki(double a, double b, double epsilon)
            {
                int k = 0, sch = 0;
                double xmin, fmin, xsr, proizvodnaya;
                while (true)
                {
                    xsr = (a + b) / 2;
                    proizvodnaya = derivative(xsr);
                    sch++;
                    if (Math.Abs(proizvodnaya) <= epsilon)
                    {
                        xmin = xsr;
                        fmin = function(xmin);
                        sch++;
                        break;
                    }
                    else
                    {
                        if (proizvodnaya > 0)
                        {
                            b = xsr;
                        }
                        else
                        {
                            a = xsr;
                        }
                    }
                    k++;
                }
                Console.WriteLine("Метод средней точки");
                Console.WriteLine("Количество итераций: " + k);
                Console.WriteLine("Количество вычислений функции или её производной: " + sch);
                Console.WriteLine("Отрезок: [" + a + "," + b + "]");
                Console.WriteLine("Минимальный X функции: " + xmin);
                Console.WriteLine("Минимальное значение функции: " + fmin);
                Console.WriteLine("");
            }


            public void MetodHord(double a, double b, double epsilon)
            {
                int sch = 0, k = 0;
                double fmin = 0, xmin = 0, prf = 0, xk = 0, pra = derivative(a), prb = derivative(b);
                while (true)
                {
                    if (pra * prb < 0)
                    {
                        xk = a - (pra / (pra - prb)) * (a - b);
                        prf = derivative(xk);
                        sch++;
                    }
                    else
                    {
                        if (pra > 0 && prb > 0)
                        {
                            xmin = a;
                        }
                        else if (pra < 0 && prb < 0)
                        {
                            xmin = b;
                        }
                        else if (pra * prb == 0)
                        {
                            if (pra == 0)
                            {
                                xmin = a;
                            }
                            else if (prb == 0)
                            {
                                xmin = b;
                            }
                        }
                    }
                    if (Math.Abs(prf) <= epsilon)
                    {
                        xmin = xk;
                        fmin = function(xmin);
                        sch++;
                        break;
                    }
                    else if (Math.Abs(prf) > epsilon)
                    {
                        if (prf > 0)
                        {
                            b = xk;
                            prb = prf;
                            k++;
                        }
                        else if (prf <= 0)
                        {
                            a = xk;
                            pra = prf;
                            k++;
                        }
                    }
                }
                Console.WriteLine("Метод хорд");
                Console.WriteLine("Количество итераций: " + k);
                Console.WriteLine("Количество вычислений функции или её производной: " + sch);
                Console.WriteLine("Отрезок: [" + a + "," + b + "]");
                Console.WriteLine("Минимальный X функции: " + xmin);
                Console.WriteLine("Минимальное значение функции: " + fmin);
            }
        }
    }
}