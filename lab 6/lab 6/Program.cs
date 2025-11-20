using System;
using System.Collections.Generic;
using System.Linq;

namespace AirportTicketSystem
{
    // 3. Тип данных - перечисление
    public enum FlightType
    {
        Domestic,   // Внутренний
        International, // Международный
        Charter     // Чартерный
    }

    // Класс Тариф
    public class Tariff
    {
        public string Destination { get; set; }     // Направление
        public decimal Price { get; set; }          // Стоимость
        public FlightType FlightType { get; set; }  // Тип рейса

        public Tariff(string destination, decimal price, FlightType flightType)
        {
            Destination = destination;
            Price = price;
            FlightType = flightType;
        }

        public override string ToString()
        {
            return $"{Destination} ({FlightType}) - {Price:C}";
        }
    }

    // Класс Пассажир
    public class Passenger
    {
        public string PassportData { get; set; }    // Паспортные данные
        public string FullName { get; set; }        // ФИО
        public List<Ticket> Tickets { get; set; }   // Билеты пассажира

        public Passenger(string passportData, string fullName)
        {
            PassportData = passportData;
            FullName = fullName;
            Tickets = new List<Ticket>();
        }

        // Расчет стоимости всех билетов пассажира
        public decimal CalculateTotalCost()
        {
            return Tickets.Sum(ticket => ticket.Tariff.Price);
        }

        public override string ToString()
        {
            return $"{FullName} (Паспорт: {PassportData})";
        }
    }

    // Класс Билет
    public class Ticket
    {
        public Passenger Passenger { get; set; }    // Пассажир
        public Tariff Tariff { get; set; }          // Тариф
        public DateTime PurchaseDate { get; set; }  // Дата покупки
        public string TicketNumber { get; set; }    // Номер билета

        public Ticket(Passenger passenger, Tariff tariff, string ticketNumber)
        {
            Passenger = passenger;
            Tariff = tariff;
            PurchaseDate = DateTime.Now;
            TicketNumber = ticketNumber;
        }

        public override string ToString()
        {
            return $"Билет №{TicketNumber}: {Passenger.FullName} -> {Tariff.Destination}";
        }
    }

    // Основной класс Аэропорт (содержит коллекции)
    public class Airport
    {
        // Коллекция тарифов
        public List<Tariff> Tariffs { get; set; }

        // Коллекция билетов
        public List<Ticket> Tickets { get; set; }

        // Коллекция пассажиров
        public List<Passenger> Passengers { get; set; }

        public Airport()
        {
            Tariffs = new List<Tariff>();
            Tickets = new List<Ticket>();
            Passengers = new List<Passenger>();
        }

        // Добавление тарифа
        public void AddTariff(Tariff tariff)
        {
            Tariffs.Add(tariff);
        }

        // Регистрация пассажира
        public void RegisterPassenger(Passenger passenger)
        {
            Passengers.Add(passenger);
        }

        // Покупка билета
        public Ticket PurchaseTicket(Passenger passenger, Tariff tariff, string ticketNumber)
        {
            var ticket = new Ticket(passenger, tariff, ticketNumber);
            Tickets.Add(ticket);
            passenger.Tickets.Add(ticket);
            return ticket;
        }

        // Расчет стоимости всех проданных билетов
        public decimal CalculateTotalRevenue()
        {
            return Tickets.Sum(ticket => ticket.Tariff.Price);
        }

        // Поиск пассажира по паспортным данным
        public Passenger FindPassengerByPassport(string passportData)
        {
            return Passengers.FirstOrDefault(p => p.PassportData == passportData);
        }

        // Поиск тарифа по направлению
        public Tariff FindTariffByDestination(string destination)
        {
            return Tariffs.FirstOrDefault(t => t.Destination == destination);
        }

        // Получение списка билетов пассажира
        public List<Ticket> GetPassengerTickets(string passportData)
        {
            var passenger = FindPassengerByPassport(passportData);
            return passenger?.Tickets ?? new List<Ticket>();
        }
    }

    // 4. Ввод/вывод реализован вне проектируемого класса
    class Program
    {
        static void Main(string[] args)
        {
            Airport airport = new Airport();

            // Инициализация тестовых данных
            InitializeTestData(airport);

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== СИСТЕМА УПРАВЛЕНИЯ АВИАКАССАМИ ===");
                Console.WriteLine("1. Просмотр тарифов");
                Console.WriteLine("2. Добавить тариф");
                Console.WriteLine("3. Зарегистрировать пассажира");
                Console.WriteLine("4. Покупка билета");
                Console.WriteLine("5. Стоимость билетов пассажира");
                Console.WriteLine("6. Общая выручка");
                Console.WriteLine("7. Список всех билетов");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ShowTariffs(airport);
                        break;
                    case "2":
                        AddTariff(airport);
                        break;
                    case "3":
                        RegisterPassenger(airport);
                        break;
                    case "4":
                        PurchaseTicket(airport);
                        break;
                    case "5":
                        CalculatePassengerTotal(airport);
                        break;
                    case "6":
                        ShowTotalRevenue(airport);
                        break;
                    case "7":
                        ShowAllTickets(airport);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            }
        }

        static void InitializeTestData(Airport airport)
        {
            // Добавляем тестовые тарифы
            airport.AddTariff(new Tariff("Москва", 5000, FlightType.Domestic));
            airport.AddTariff(new Tariff("Нью-Йорк", 25000, FlightType.International));
            airport.AddTariff(new Tariff("Стамбул", 15000, FlightType.International));
            airport.AddTariff(new Tariff("Сочи", 7000, FlightType.Domestic));
            airport.AddTariff(new Tariff("Анталья", 12000, FlightType.Charter));

            // Регистрируем тестовых пассажиров
            airport.RegisterPassenger(new Passenger("4500112233", "Иванов Иван Иванович"));
            airport.RegisterPassenger(new Passenger("4511223344", "Петрова Анна Сергеевна"));
        }

        static void ShowTariffs(Airport airport)
        {
            Console.WriteLine("\n=== СПИСОК ТАРИФОВ ===");
            if (airport.Tariffs.Count == 0)
            {
                Console.WriteLine("Тарифы отсутствуют.");
                return;
            }

            foreach (var tariff in airport.Tariffs)
            {
                Console.WriteLine(tariff);
            }
        }

        static void AddTariff(Airport airport)
        {
            Console.WriteLine("\n=== ДОБАВЛЕНИЕ ТАРИФА ===");

            Console.Write("Направление: ");
            string destination = Console.ReadLine();

            Console.Write("Стоимость: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Тип рейса:");
            Console.WriteLine("1 - Внутренний");
            Console.WriteLine("2 - Международный");
            Console.WriteLine("3 - Чартерный");
            Console.Write("Выберите тип: ");

            FlightType flightType = (FlightType)(int.Parse(Console.ReadLine()) - 1);

            airport.AddTariff(new Tariff(destination, price, flightType));
            Console.WriteLine("Тариф успешно добавлен!");
        }

        static void RegisterPassenger(Airport airport)
        {
            Console.WriteLine("\n=== РЕГИСТРАЦИЯ ПАССАЖИРА ===");

            Console.Write("Паспортные данные: ");
            string passport = Console.ReadLine();

            Console.Write("ФИО: ");
            string fullName = Console.ReadLine();

            airport.RegisterPassenger(new Passenger(passport, fullName));
            Console.WriteLine("Пассажир успешно зарегистрирован!");
        }

        static void PurchaseTicket(Airport airport)
        {
            Console.WriteLine("\n=== ПОКУПКА БИЛЕТА ===");

            Console.Write("Паспортные данные пассажира: ");
            string passport = Console.ReadLine();

            var passenger = airport.FindPassengerByPassport(passport);
            if (passenger == null)
            {
                Console.WriteLine("Пассажир не найден!");
                return;
            }

            Console.Write("Направление: ");
            string destination = Console.ReadLine();

            var tariff = airport.FindTariffByDestination(destination);
            if (tariff == null)
            {
                Console.WriteLine("Тариф не найден!");
                return;
            }

            Console.Write("Номер билета: ");
            string ticketNumber = Console.ReadLine();

            var ticket = airport.PurchaseTicket(passenger, tariff, ticketNumber);
            Console.WriteLine($"Билет успешно продан! Стоимость: {tariff.Price:C}");
        }

        static void CalculatePassengerTotal(Airport airport)
        {
            Console.WriteLine("\n=== СТОИМОСТЬ БИЛЕТОВ ПАССАЖИРА ===");

            Console.Write("Паспортные данные пассажира: ");
            string passport = Console.ReadLine();

            var passenger = airport.FindPassengerByPassport(passport);
            if (passenger == null)
            {
                Console.WriteLine("Пассажир не найден!");
                return;
            }

            decimal total = passenger.CalculateTotalCost();
            Console.WriteLine($"Общая стоимость билетов пассажира: {total:C}");

            if (passenger.Tickets.Count > 0)
            {
                Console.WriteLine("\nСписок билетов:");
                foreach (var ticket in passenger.Tickets)
                {
                    Console.WriteLine($"  {ticket.Tariff.Destination} - {ticket.Tariff.Price:C}");
                }
            }
        }

        static void ShowTotalRevenue(Airport airport)
        {
            Console.WriteLine("\n=== ОБЩАЯ ВЫРУЧКА ===");
            decimal total = airport.CalculateTotalRevenue();
            Console.WriteLine($"Общая стоимость всех проданных билетов: {total:C}");
        }

        static void ShowAllTickets(Airport airport)
        {
            Console.WriteLine("\n=== ВСЕ ПРОДАННЫЕ БИЛЕТЫ ===");
            if (airport.Tickets.Count == 0)
            {
                Console.WriteLine("Билеты отсутствуют.");
                return;
            }

            foreach (var ticket in airport.Tickets)
            {
                Console.WriteLine($"{ticket.TicketNumber}: {ticket.Passenger.FullName} -> " +
                                $"{ticket.Tariff.Destination} ({ticket.Tariff.Price:C})");
            }
        }
    }
}