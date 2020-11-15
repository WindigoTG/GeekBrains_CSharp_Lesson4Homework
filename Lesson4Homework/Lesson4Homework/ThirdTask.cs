using System;
using System.IO;

//Олесов Максим

//3.Решить задачу с логинами из предыдущего урока, только логины и пароли считать из файла в массив. Создайте структуру Account, содержащую Login и Password.

namespace Lesson4Homework
{
    struct LoginInfo
    {
        string login;
        string password;

        public LoginInfo(string path)
        {
            string setLogin = "admin";
            string setPassword = "admin";
            bool success = false;
            try
            {
                using StreamReader sr = new StreamReader(path);
                string tempLogin = sr.ReadLine();
                string tempPassword = sr.ReadLine();
                sr.Close();
                if (tempLogin.StartsWith("login: ") && tempPassword.StartsWith("password: "))
                {
                    setLogin = tempLogin.Substring(7);
                    setPassword = tempPassword.Substring(10);
                    success = true;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                login = setLogin;
                password = setPassword;
                View view = new View();
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (success)
                    view.Print("Информация об аккаунте подгружена успешно");
                else
                    view.Print("Не удалось получить информацию об аккаунте. Попробуйте авторизироваться от имени администратора");
                Console.ResetColor();
            }
        }

        public string Login
        {
            get { return login; }
        }
        public string Password
        {
            get { return password; }
        }
    }
    class ThirdTask
    {
        public bool Run()
        {
            View view = new View();
            view.Print("Зачада 3. Проверка ввода логина и пароля");
            Console.ForegroundColor = ConsoleColor.Yellow;
            view.Print("Для получения доступа к следующей задаче необходимо произвести авторизацию\n");
            Console.ResetColor();
            LoginInfo loginInfo = new LoginInfo("LogInInfo.txt");

            int tryCount = 3;
            do
            {
                view.Print($"Количество попыток для входа: {tryCount}");
                string logInput = view.GetString("Введите логин");
                string passInput = view.GetString("Введите логин");
                if (logInput == loginInfo.Login && passInput == loginInfo.Password)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    view.Print("Вход выполнен. Вам предоставлен доступ к следующей задаче");
                    Console.ResetColor();
                    view.Pause();
                    return true;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                view.Print("Логин и/или пароль не вереню Попробуйте еще раз\n");
                Console.ResetColor();
                tryCount--;
            } while (tryCount > 0);
            Console.ForegroundColor = ConsoleColor.Red;
            view.Print("В доступе к следующей задаче отказано");
            Console.ResetColor();
            view.Pause();
            return false;
        }
    }
}
