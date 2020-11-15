using System;
//Олесов Максим

namespace Lesson4Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            FirstTask firstTask = new FirstTask();
            firstTask.Run();
            SecondTask secondTask = new SecondTask();
            secondTask.Run();
            ThirdTask thirdTask = new ThirdTask();
            if (thirdTask.Run())
            {
                FourthTask fourthTask = new FourthTask();
                fourthTask.Run();
            }
        }
    }
}
