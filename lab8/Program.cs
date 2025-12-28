using System;
using MedAutomation.Services;
using MedAutomation.Data;
using MedAutomation.Utils;

namespace MedAutomation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Автоматизация лечебного учреждения - Консольное приложение";
            var repo = new Repository("data");
            var service = new ClinicService(repo);

            // Загрузить начальные данные (если нужно)
            repo.LoadAll();

            while (true)
            {
                ConsoleUtils.WriteHeader("Главное меню");
                Console.WriteLine("1. Регистрация нового пациента");
                Console.WriteLine("2. Просмотр списка пациентов");
                Console.WriteLine("3. Редактирование данных пациента");
                Console.WriteLine("4. Управление расписанием врачей");
                Console.WriteLine("5. Запись пациента на приём");
                Console.WriteLine("6. Просмотр расписания и записей");
                Console.WriteLine("7. Ведение медицинской карты");
                Console.WriteLine("8. Отчёты / Экспорт данных");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");
                var key = Console.ReadLine();

                try
                {
                    switch (key)
                    {
                        case "1":
                            service.RegisterPatient();
                            break;
                        case "2":
                            service.ListPatients();
                            break;
                        case "3":
                            service.EditPatient();
                            break;
                        case "4":
                            service.ManageDoctorsSchedule();
                            break;
                        case "5":
                            service.BookAppointment();
                            break;
                        case "6":
                            service.ViewAppointments();
                            break;
                        case "7":
                            service.ManageMedicalRecords();
                            break;
                        case "8":
                            service.ReportsAndExport();
                            break;
                        case "0":
                            repo.SaveAll();
                            Console.WriteLine("Данные сохранены. Выход.");
                            return;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }

                Console.WriteLine("\nНажмите Enter, чтобы продолжить...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
