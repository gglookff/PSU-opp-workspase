using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        if (args == null || args.Length == 0 || string.IsNullOrEmpty(args[0]))
        {
            Console.WriteLine("Пожалуйста, укажите имя файла как аргумент командной строки.");
            Console.WriteLine("Пример: Program.exe filename.txt");
            return;
        }

        string fileName = args[0];

        if (!File.Exists(fileName))
        {
            Console.WriteLine("Файл '" + fileName + "' не найден.");
            return;
        }

        try
        {
            // Читаем содержимое файла и преобразуем в List<char>
            string fileContent = File.ReadAllText(fileName);
            List<char> characters = fileContent.ToList();

            // Вычисляем количество гласных и согласных
            var result = CountVowelsAndConsonants(characters);

            Console.WriteLine("Количество гласных букв: " + result.Item1);
            Console.WriteLine("Количество согласных букв: " + result.Item2);
            Console.WriteLine("Всего букв: " + (result.Item1 + result.Item2));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка при чтении файла: " + ex.Message);
        }
    }

    // Метод для подсчета гласных и согласных букв с использованием List<T>
    static Tuple<int, int> CountVowelsAndConsonants(List<char> characters)
    {
        // Определяем множества гласных букв (русский и английский алфавиты)
        List<char> vowels = new List<char> {
            'a', 'e', 'i', 'o', 'u', 'y', // английские
            'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я', // русские
            'A', 'E', 'I', 'O', 'U', 'Y', // английские заглавные
            'А', 'Е', 'Ё', 'И', 'О', 'У', 'Ы', 'Э', 'Ю', 'Я'  // русские заглавные
        };

        int vowelCount = 0;
        int consonantCount = 0;

        // Используем цикл foreach для прохода по List<char>
        foreach (char c in characters)
        {
            if (char.IsLetter(c))
            {
                if (vowels.Contains(c))
                {
                    vowelCount++;
                }
                else
                {
                    consonantCount++;
                }
            }
        }

        return Tuple.Create(vowelCount, consonantCount);
    }
}