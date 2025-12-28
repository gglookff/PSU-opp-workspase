using System;

namespace MedAutomation.Utils
{
    public static class ConsoleUtils
    {
        public static void WriteHeader(string title)
        {
            Console.Clear();
            Console.WriteLine("=========================================");
            Console.WriteLine(title);
            Console.WriteLine("=========================================");
        }

        public static string Read(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? "";
        }

        public static string ReadNonEmpty(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(s)) return s;
                Console.WriteLine("Поле не может быть пустым.");
            }
        }

        public static string ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine();
                if (DateTime.TryParseExact(s, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _))
                    return s;
                Console.WriteLine("Неверный формат даты. Ожидается yyyy-MM-dd.");
            }
        }

        public static string ReadTime(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine();
                if (TimeSpan.TryParseExact(s, "hh\\:mm", null, out _) || TimeSpan.TryParse(s, out _))
                    return s;
                // Попробуем формат HH:mm
                if (DateTime.TryParseExact(s, "HH:mm", null, System.Globalization.DateTimeStyles.None, out _))
                    return s;
                Console.WriteLine("Неверный формат времени. Ожидается HH:mm.");
            }
        }
    }
}
