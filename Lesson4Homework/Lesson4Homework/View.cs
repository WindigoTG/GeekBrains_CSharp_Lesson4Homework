using System;

//Олесов Максим

namespace Lesson4Homework
{
    class View
    {
        public void Print(object value, bool isNewLine = true)
        {
            if (isNewLine)
            {
                Console.WriteLine(value);
            }
            else
            {
                Console.Write(value);
            }
        }

        public void Pause()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Print("\nНажмите любую клавишу для продолжения");
            Console.ReadKey();
            Console.ResetColor();
            Console.Clear();
        }

        public string GetString()
        {
            return Console.ReadLine();
        }
        public string GetString(string str)
        {
            Print(str);
            return Console.ReadLine();
        }

        public int GetInt(string str)
        {
            int inputResult;
            Print(str);
            while (!int.TryParse(GetString(), out inputResult))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Print($"Ошибка ввода!");
                Console.ResetColor();
                Print(str);
            }
            return inputResult;
        }

        public double GetDouble(string str)
        {
            double inputResult;
            Print(str);
            while (!double.TryParse(GetString().Replace('.', ','), out inputResult))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Print($"Ошибка ввода!");
                Console.ResetColor();
                Print(str);
            }
            return inputResult;
        }

        public double GetDoublePos(string str)
        {
            double inputResult;
            Print(str);
            while (!double.TryParse(GetString().Replace('.', ','), out inputResult))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Print($"Ошибка ввода!");
                Console.ResetColor();
                Print(str);
            }
            if (inputResult < 0)
                return -inputResult;
            return inputResult;
        }
    }
}
