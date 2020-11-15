using System;
using System.IO;
using System.Text;

/*4. * а) Реализовать класс для работы с двумерным массивом. Реализовать конструктор, заполняющий массив случайными числами.
Создать методы, которые возвращают сумму всех элементов массива, сумму всех элементов массива больше заданного, свойство, возвращающее минимальный элемент массива,
свойство, возвращающее максимальный элемент массива, метод, возвращающий номер максимального элемента массива (через параметры, используя модификатор ref или out)
*б) Добавить конструктор и методы, которые загружают данные из файла и записывают данные в файл.
Дополнительные задачи
в) Обработать возможные исключительные ситуации при работе с файлами.*/

namespace Lesson4Homework
{
    class TwoDimArray
    {
        int[,] a;

        public TwoDimArray(int n, int el)
        {
            a = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    a[i, j] = el;
        }
        public TwoDimArray(int n, int min, int max)
        {
            a = new int[n, n];
            if (min > max)
            {
                min += max;
                max = min - max;
                min = min - max;
            }
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    a[i, j] = rnd.Next(min, max);
            WriteToFile("TwoDimArray.txt");
        }
        public TwoDimArray(int n, int m, int min, int max)
        {
            a = new int[n, m];
            if (min > max)
            {
                min += max;
                max = min - max;
                min = min - max;
            }
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    a[i, j] = rnd.Next(min, max);
            WriteToFile("TwoDimArray.txt");
        }
        //Создание массива и заполнение его значениями, считанными из файла
        public TwoDimArray(string path)
        {
            using StreamReader sr = new StreamReader(path);
            //  Считываем количество элементов массива
            int N = int.Parse(sr.ReadLine());
            int M = int.Parse(sr.ReadLine());
            a = new int[N, M];
            int readCount = 0;
            //  Считываем массив
            for (int i = 0; i < N; i++)
                for (int j = 0; j < M; j++)
                {
                    a[i,j] = int.Parse(sr.ReadLine());
                    readCount++;
                }
            if (!sr.EndOfStream) //Если после заполнения массива в файле еще остались несчитанные элементы, выбрасываем исключение
                throw new Exception("\nПохоже, массив был сохранён некорректно и его размер меньше количества сохранённых элементов");
            sr.Close();
        }

        public int Min
        {
            get
            {
                int min = a[0, 0];
                for (int i = 0; i < a.GetLength(0); i++)
                    for (int j = 0; j < a.GetLength(1); j++)
                        if (a[i, j] < min) min = a[i, j];
                return min;
            }
        }
        public int Max
        {
            get
            {
                int max = a[0, 0];
                for (int i = 0; i < a.GetLength(0); i++)
                    for (int j = 0; j < a.GetLength(1); j++)
                        if (a[i, j] > max) max = a[i, j];
                return max;
            }
        }


        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                    s += a[i, j] + " ";
                s += "\n";

            }
            return s;
        }

        //Запись массива в файл
        public void WriteToFile(string path)
        {
            try
            {
                if (String.IsNullOrEmpty(path)) return;
                using StreamWriter sw = new StreamWriter(path, false, Encoding.ASCII);
                sw.WriteLine(a.GetLength(0));
                sw.WriteLine(a.GetLength(1));
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    for (int j = 0; j < a.GetLength(1); j++)
                    {
                        sw.WriteLine(a[i,j]);
                    }
                }
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(ex.Message);
                Console.WriteLine("К сожалению, сохранить массив в файл для повторного использования в будущем не удалось");
                Console.ResetColor();
            }
        }

        public string MaxPosition()
        {
            string s = "";
            int max = this.Max;
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (a[i, j] == max)
                        s += $"[{i}, {j}], ";
                }
            return s.TrimEnd(',', ' ');
        }
    }


    class FourthTask
    {
        public void Run()
        {
            View view = new View();
            view.Print("Задача 4. Создание класса одномерного массива");

            TwoDimArray arr;
            bool loadSuccess = false;
            string path = "TwoDimArray.txt";

            if (File.Exists(path))
            {
                view.Print("Обнаружен файл, который может содержать данные массива.\nХотите загрузить массив из файла? (y/n)");

                ConsoleKeyInfo key;
                do
                {
                    key = Console.ReadKey();
                } while (key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N);

                if (key.Key == ConsoleKey.Y)
                {
                    try
                    {
                        arr = new TwoDimArray(path);
                        loadSuccess = true;
                        Console.ForegroundColor = ConsoleColor.Green;
                        view.Print("\nЗагрузка массива прошла успешно");
                        Console.ResetColor();
                        ArrayOperations(arr);
                    }
                    catch (System.OverflowException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        view.Print("\nПохоже, величины каких-то значений в файле некорректны");
                        Console.ResetColor();
                    }
                    catch (System.FormatException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        view.Print("\nПохоже, в файле имеются данные неверного формата");
                        Console.ResetColor();
                    }
                    catch (System.ArgumentNullException ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        view.Print("\nПохоже, массив был сохранён некорректно и его размер больше количества сохранённых элементов");
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        view.Print(ex.Message);
                        Console.ResetColor();
                    }
                }
            }

            if (!loadSuccess)
            {
                view.Print("\nДавайте создадим новый массив");
                int length1 = view.GetInt("\nВведите желаемую длинну первого измерения массива");
                int length2 = view.GetInt("\nВведите желаемую длинну второго измерения массива");
                int min = view.GetInt("\nВведите минимально допустимую величину элементов массива");
                int max = view.GetInt("\nВведите максимально допустимую величину элементов массива");
                arr = new TwoDimArray(length1, length2, min, max);
                ArrayOperations(arr);
            }

            view.Pause();
        }

        private void ArrayOperations(TwoDimArray arr)
        {
            View view = new View();
            view.Print($"\nВаш массив:\n{arr.ToString()}");
            view.Print($"\nМинимальный элемент массива: {arr.Min}");
            view.Print($"\nМаксимальный элемент массива: {arr.Max}");
            view.Print($"\nМаксимальный элемент можно найти по номеру:\n{arr.MaxPosition()}");
        }
    }
}
