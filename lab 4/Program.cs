using System;
using System.IO;

class Program
{
    static void Main()
    {
        
        string filePath = "spis.txt";

        
        while (true)
        {
            Console.WriteLine("Введите фамилию (или 'стоп' для выхода):");
            string surname = Console.ReadLine();

            if (surname.ToLower() == "стоп")
                break;

            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine();

            Console.WriteLine("Введите возраст:");
            string age = Console.ReadLine();

           
            StreamWriter writer = new StreamWriter(filePath, true);
            writer.WriteLine($"{surname} {name} {age} лет");
            writer.Close();

            Console.WriteLine("Данные записаны!\n");
        }

        Console.WriteLine("Всё записано в файл spis.txt");
        Console.ReadKey();
    }
}