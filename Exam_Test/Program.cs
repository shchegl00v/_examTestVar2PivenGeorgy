using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace CirclesApp
{
    // Класс, представляющий точку на плоскости
    class Point
    {
        public double X { get; }
        public double Y { get; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    // Класс, представляющий окружность на плоскости
    class Circle : IComparable<Circle>
    {
        public Point Center { get; }
        public double Radius { get; }

        public Circle(Point center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        // Вычисление площади круга
        public double Area()
        {
            return Math.PI * Radius * Radius;
        }

        // Реализация интерфейса для сравнения окружностей по площади
        public int CompareTo(Circle other)
        {
            return Area().CompareTo(other.Area());
        }

        public override string ToString()
        {
            return $"Центр: ({Center.X}, {Center.Y}), Радиус: {Radius}, Площадь: {Area():F2}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var circles = ReadCirclesFromFile("input.txt");
                Console.WriteLine("До сортировки:");
                foreach (var circle in circles)
                {
                    Console.WriteLine(circle);
                }

                circles.Sort();

                Console.WriteLine("\nПосле сортировки:");
                foreach (var circle in circles)
                {
                    Console.WriteLine(circle);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл input.txt не найден.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Файл input.txt содержит данные в неправильном формате.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
        
        // Чтение данных об окружностях из файла
        static List<Circle> ReadCirclesFromFile(string fileName)
        {
            var circles = new List<Circle>();
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл не найден.");
                return circles;
            }
    
            var lines = File.ReadAllLines(fileName);
            if (lines.Length == 0)
            {
                Console.WriteLine("Файл пуст.");
                return circles;
            }

            foreach (var line in lines)
            {
                Console.WriteLine($"Чтение строки: {line}"); // Отладочный вывод
                var parts = line.Trim('(', ')').Split(',');
                if (parts.Length == 3 &&
                    double.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out double x) &&
                    double.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double y) &&
                    double.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double radius) &&
                    radius > 0)
                {
                    circles.Add(new Circle(new Point(x, y), radius));
                }
                else
                {
                    Console.WriteLine("Неверный формат данных в строке."); // Отладочный вывод
                }
            }
            return circles;
        }

    }
}
