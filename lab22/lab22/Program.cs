using System;

namespace LabWork
{
    // Базовый класс
    public class Izdelie
    {
        public string Name { get; set; }

        public Izdelie(string name)
        {
            Name = name;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Изделие: {Name}");
        }
    }

    // Производный класс - Узел
    public class Uzel : Izdelie
    {
        public int PartCount { get; set; }

        public Uzel(string name, int partCount) : base(name)
        {
            PartCount = partCount;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Количество деталей в узле: {PartCount}");
        }
    }

    // Производный класс - Механизм
    public class Mekhanizm : Uzel
    {
        public string Function { get; set; }

        public Mekhanizm(string name, int partCount, string function)
            : base(name, partCount)
        {
            Function = function;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Функция механизма: {Function}");
        }
    }

    // Производный класс - Деталь
    public class Detal : Mekhanizm
    {
        public string Material { get; set; }

        public Detal(string name, int partCount, string function, string material)
            : base(name, partCount, function)
        {
            Material = material;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Материал детали: {Material}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Detal detal = new Detal("Шестерня", 5, "Передача вращения", "Сталь");
            detal.DisplayInfo();

            Console.ReadLine();
        }
    }
}
