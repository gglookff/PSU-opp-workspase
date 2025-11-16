using System;

public class Program
{
    public static void Main(string[] args)
    {
        // Создаем массив температур
        int[,] temperature = GenerateTemperatureArray();

        Console.WriteLine("Температуры за год:");
        PrintTemperatureArray(temperature);

        // Вычисляем средние температуры по месяцам
        double[] monthlyAverages = CalculateMonthlyAverages(temperature);

        Console.WriteLine("\nСредние температуры по месяцам:");
        PrintMonthlyAverages(monthlyAverages);

        // Сортируем средние температуры
        Array.Sort(monthlyAverages);

        Console.WriteLine("\nОтсортированные средние температуры по возрастанию:");
        PrintSortedAverages(monthlyAverages);
    }

    // Метод для генерации случайного массива температур
    public static int[,] GenerateTemperatureArray()
    {
        Random random = new Random();
        int[,] temperature = new int[12, 30];

        // Генерируем температуры для каждого дня каждого месяца
        for (int month = 0; month < 12; month++)
        {
            // Определяем диапазон температур в зависимости от месяца (условно)
            int minTemp, maxTemp;

            switch (month)
            {
                case 0:
                case 1:
                case 11: // Зима: декабрь, январь, февраль
                    minTemp = -20; maxTemp = 0;
                    break;
                case 2:
                case 3:
                case 4: // Весна: март, апрель, май
                    minTemp = -5; maxTemp = 15;
                    break;
                case 5:
                case 6:
                case 7: // Лето: июнь, июль, август
                    minTemp = 15; maxTemp = 30;
                    break;
                case 8:
                case 9:
                case 10: // Осень: сентябрь, октябрь, ноябрь
                    minTemp = 0; maxTemp = 15;
                    break;
                default:
                    minTemp = -10; maxTemp = 10;
                    break;
            }

            for (int day = 0; day < 30; day++)
            {
                temperature[month, day] = random.Next(minTemp, maxTemp + 1);
            }
        }

        return temperature;
    }

    // Метод для печати массива температур
    public static void PrintTemperatureArray(int[,] temperature)
    {
        string[] monthNames = {
            "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
            "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
        };

        for (int month = 0; month < 12; month++)
        {
            Console.Write($"{monthNames[month]}: ");
            for (int day = 0; day < 30; day++)
            {
                Console.Write($"{temperature[month, day]} ");
                if ((day + 1) % 10 == 0) Console.Write("  "); // Разделитель для читаемости
            }
            Console.WriteLine();
        }
    }

    // Метод для вычисления средних температур по месяцам
    public static double[] CalculateMonthlyAverages(int[,] temperature)
    {
        double[] averages = new double[12];

        for (int month = 0; month < 12; month++)
        {
            int sum = 0;
            for (int day = 0; day < 30; day++)
            {
                sum += temperature[month, day];
            }
            averages[month] = Math.Round((double)sum / 30, 1); // Округляем до 1 знака после запятой
        }

        return averages;
    }

    // Метод для печати средних температур по месяцам
    public static void PrintMonthlyAverages(double[] averages)
    {
        string[] monthNames = {
            "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
            "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
        };

        for (int i = 0; i < 12; i++)
        {
            Console.WriteLine($"{monthNames[i]}: {averages[i]}°C");
        }
    }

    // Метод для печати отсортированных средних температур
    public static void PrintSortedAverages(double[] averages)
    {
        for (int i = 0; i < averages.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {averages[i]}°C");
        }
    }
}