using System;
using System.IO;
using System.Text;

//Олесов Максим

/*2. а) Дописать класс для работы с одномерным массивом. Реализовать конструктор, создающий массив заданной размерности и заполняющий массив числами от начального значения с заданным шагом.
Создать свойство Sum, которые возвращают сумму элементов массива, метод Inverse, меняющий знаки у всех элементов массива, метод Multi, умножающий каждый элемент массива на определенное число,
свойство MaxCount, возвращающее количество максимальных элементов. В Main продемонстрировать работу класса.
б)Добавить конструктор и методы, которые загружают данные из файла и записывают данные в файл.*/

namespace Lesson4Homework
{
    class OneDimArray
    {
        int[] a;
        //  Создание массива и заполнение его одним значением el  
        public OneDimArray(int n, int el)
        {
            a = new int[n];
            for (int i = 0; i < n; i++)
                a[i] = el;
        }
        //  Создание массива и заполнение его случайными числами от min до max
        public OneDimArray(int n, int min, int max)
        {
            a = new int[n];
            if (min > max)
            {
                min += max;
                max = min - max;
                min = min - max;
            }
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
                a[i] = rnd.Next(min, max);
            WriteToFile("OneDimArray.txt"); //Сохранения массива в файл
        }
        //Создание массива и заполнение его значениями, считанными из файла
        public OneDimArray(string path)
        {
            using StreamReader sr = new StreamReader(path);
                //  Считываем количество элементов массива
                int N = int.Parse(sr.ReadLine());
                a = new int[N];
                int readCount = 0;
                //  Считываем массив
                for (int i = 0; i < N; i++)
                {
                    a[i] = int.Parse(sr.ReadLine());
                    readCount++;
                }
                if (!sr.EndOfStream) //Если после заполнения массива в файле еще остались несчитанные элементы, выбрасываем исключение
                    throw new Exception("\nПохоже, массив был сохранён некорректно и его размер меньше количества сохранённых элементов");
                sr.Close();
        }

        public int Length
        {
            get
            {
                return a.Length;
            }
        }

        public int Max
        {
            get
            {
                int max = a[0];
                for (int i = 1; i < a.Length; i++)
                    if (a[i] > max) max = a[i];
                return max;
            }
        }
        public int Min
        {
            get
            {
                int min = a[0];
                for (int i = 1; i < a.Length; i++)
                    if (a[i] < min) min = a[i];
                return min;
            }
        }
        public int CountPositiv
        {
            get
            {
                int count = 0;
                for (int i = 0; i < a.Length; i++)
                    if (a[i] > 0) count++;
                return count;
            }
        }
        public override string ToString()
        {
            string s = "";
            foreach (int v in a)
                s = s + v + " ";
            return s;
        }

        public int Sum
        {
            get
            {
                int sum = 0;
                foreach (int el in a)
                    sum += el;
                return sum;
            }
        }

        //Запись массива в файл
        public void WriteToFile(string path)
        {
            try
            {
                if (String.IsNullOrEmpty(path)) return;
                using StreamWriter sw = new StreamWriter(path, false, Encoding.ASCII);
                sw.WriteLine(a.Length);
                for (int i = 0; i < a.Length; i++)
                {
                    sw.WriteLine(a[i]);
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

        public int MaxCount
        {
            get
            {
                int maxCount = 0;
                int max = this.Max;
                foreach (int el in a)
                {
                    if (el == max)
                        maxCount++;
                }

                    return maxCount;
            }
        }

        public void Multi(int multiplier)
        {
            for (int i = 0; i < a.Length; i++)
                a[i] *= multiplier;
        }

        public void Inverse()
        {
            for (int i = 0; i < a.Length; i++)
                a[i] *= -1;
        }
    }

    class SecondTask
    {
        public void Run()
        {
            View view = new View();
            view.Print("Задача 2. Доработка класса одномерного массива");

            OneDimArray arr;
            bool loadSuccess = false;
            string path = "OneDimArray.txt";

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
                        arr = new OneDimArray(path);
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
                int length = view.GetInt("\nВведите желаемую длинну массива");
                int min = view.GetInt("\nВведите минимально допустимую величину элементов массива");
                int max = view.GetInt("\nВведите максимально допустимую величину элементов массива");

                arr = new OneDimArray(length, min, max);
                ArrayOperations(arr);
            }
            view.Pause();
        }

        private void ArrayOperations(OneDimArray arr)
        {
            View view = new View();
            view.Print($"\nВаш массив:\n{arr.ToString()}");

            view.Print($"\nСумма элементов массива равна {arr.Sum}");

            view.Print($"\nМаксимальный элемент массива: {arr.Max}\nКоличество таких элементов в массиве: {arr.MaxCount}");

            int multiplier = view.GetInt("\nВведите число, на которое вы хотели бы умножить все элементы массива");
            arr.Multi(multiplier);
            view.Print($"\nТеперь массив массив выглядит вот так:\n{arr.ToString()}");

            arr.Inverse();
            view.Print($"\nВ инвертированном виде массив будет выглядеть вот так:\n{arr.ToString()}");
            view.Print($"\nА максимальный элемент массива теперь: {arr.Max}\nКоличество таких элементов в массиве: {arr.MaxCount}");
        }
    }
}
