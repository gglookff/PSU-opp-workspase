using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UniversityManagementSystem
{
    // Класс для представления студента
    public class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Group { get; set; }
        public int Course { get; set; }
        public string Institute { get; set; }
        public Dictionary<string, int> Grades { get; set; } // Предмет -> Оценка

        public Student(string lastName, string firstName, string group, int course, string institute)
        {
            LastName = lastName;
            FirstName = firstName;
            Group = group;
            Course = course;
            Institute = institute;
            Grades = new Dictionary<string, int>();
        }

        public double AverageGrade => Grades.Count > 0 ? Grades.Values.Average() : 0;

        public bool IsExcellentStudent => Grades.Count > 0 && Grades.Values.All(grade => grade == 5);
        public bool HasNoThreesOrTwos => Grades.Count > 0 && Grades.Values.All(grade => grade >= 4);
        public int CountOfTwos => Grades.Count > 0 ? Grades.Values.Count(grade => grade == 2) : 0;
        public bool HasZeros => Grades.Count > 0 && Grades.Values.Any(grade => grade == 0);

        public void AddGrade(string subject, int grade)
        {
            Grades[subject] = grade;
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} - {Institute}, {Course} курс, группа {Group}, Средний балл: {AverageGrade:F2}";
        }
    }

    // Основной класс для управления данными
    public class UniversityManager
    {
        public List<Student> Students { get; set; }
        public List<string> Institutes { get; set; }
        public List<string> Subjects { get; set; }

        public UniversityManager()
        {
            Students = new List<Student>();
            Institutes = new List<string>();
            Subjects = new List<string>();
        }

        // Инициализация тестовых данных
        public void InitializeTestData()
        {
            // Добавляем институты
            Institutes.AddRange(new[] { "КФ", "ЭЭТ", "СТ", "ПР" });

            // Добавляем предметы
            Subjects.AddRange(new[] { "Математика", "Физика", "Программирование", "История", "Английский язык" });

            // Создаем студентов с оценками
            var student1 = new Student("Иванов", "Пётр", "22-КФ", 1, "КФ");
            student1.AddGrade("Математика", 5);
            student1.AddGrade("Физика", 5);
            student1.AddGrade("Программирование", 5);
            Students.Add(student1);

            var student2 = new Student("Голубев", "Петр", "23-КФ", 1, "КФ");
            student2.AddGrade("Математика", 2);
            student2.AddGrade("Физика", 2);
            student2.AddGrade("Программирование", 3);
            Students.Add(student2);

            var student3 = new Student("Малькова", "Анна", "22-ЭЭТ", 2, "ЭЭТ");
            student3.AddGrade("Математика", 5);
            student3.AddGrade("История", 5);
            student3.AddGrade("Английский язык", 5);
            Students.Add(student3);

            var student4 = new Student("Козыревский", "Дмитрий", "19-СТ", 3, "СТ");
            student4.AddGrade("Математика", 4);
            student4.AddGrade("Физика", 4);
            student4.AddGrade("Программирование", 5);
            Students.Add(student4);

            var student5 = new Student("Сардыко", "Алексей", "23-ПР", 1, "ПР");
            student5.AddGrade("Математика", 2);
            student5.AddGrade("Физика", 2);
            student5.AddGrade("Программирование", 2);
            Students.Add(student5);
        }

        // 1) Фамилии студентов, у которых две и более двоек за сессию, и удалить их
        public void RemoveStudentsWithTwoOrMoreTwos(string outputPath)
        {
            var studentsToRemove = Students.Where(s => s.CountOfTwos >= 2).ToList();

            using (StreamWriter sw = new StreamWriter(outputPath, true, Encoding.UTF8))
            {
                sw.WriteLine("=== Студенты с двумя и более двойками (удалены): ===");
                foreach (var student in studentsToRemove)
                {
                    sw.WriteLine($"{student.LastName} {student.FirstName} - {student.Institute}, {student.Course} курс, группа {student.Group}");
                    Students.Remove(student);
                }
                if (studentsToRemove.Count == 0)
                    sw.WriteLine("Таких студентов нет");
                sw.WriteLine();
            }
        }

        // 2) Институт, на котором на первом курсе наибольшее количество отличников
        public void InstituteWithMostFirstYearExcellentStudents(string outputPath)
        {
            var result = Students
                .Where(s => s.Course == 1 && s.IsExcellentStudent)
                .GroupBy(s => s.Institute)
                .Select(g => new { Institute = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .FirstOrDefault();

            using (StreamWriter sw = new StreamWriter(outputPath, true, Encoding.UTF8))
            {
                sw.WriteLine("=== Институт с наибольшим количеством отличников на 1 курсе: ===");
                if (result != null)
                    sw.WriteLine($"{result.Institute} - {result.Count} отличников");
                else
                    sw.WriteLine("Данные отсутствуют");
                sw.WriteLine();
            }
        }

        // 4) Институт с наибольшим количеством отличников
        public void InstituteWithMostExcellentStudents(string outputPath)
        {
            var result = Students
                .Where(s => s.IsExcellentStudent)
                .GroupBy(s => s.Institute)
                .Select(g => new { Institute = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .FirstOrDefault();

            using (StreamWriter sw = new StreamWriter(outputPath, true, Encoding.UTF8))
            {
                sw.WriteLine("=== Институт с наибольшим количеством отличников: ===");
                if (result != null)
                    sw.WriteLine($"{result.Institute} - {result.Count} отличников");
                else
                    sw.WriteLine("Данные отсутствуют");
                sw.WriteLine();
            }
        }

        // 5) Полный список отличников с указанием института, группы и курса
        public void AllExcellentStudents(string outputPath)
        {
            var excellentStudents = Students.Where(s => s.IsExcellentStudent).ToList();

            using (StreamWriter sw = new StreamWriter(outputPath, true, Encoding.UTF8))
            {
                sw.WriteLine("=== Полный список отличников: ===");
                foreach (var student in excellentStudents)
                {
                    sw.WriteLine($"{student.LastName} {student.FirstName} - {student.Institute}, {student.Course} курс, группа {student.Group}");
                }
                if (excellentStudents.Count == 0)
                    sw.WriteLine("Отличников нет");
                sw.WriteLine();
            }
        }

        // 6) Группа, где нет двоечников
        public void GroupsWithoutTwos(string outputPath)
        {
            var groupsWithTwos = Students
                .Where(s => s.CountOfTwos > 0)
                .Select(s => s.Group)
                .Distinct()
                .ToList();

            var groupsWithoutTwos = Students
                .Select(s => s.Group)
                .Distinct()
                .Where(group => !groupsWithTwos.Contains(group))
                .ToList();

            using (StreamWriter sw = new StreamWriter(outputPath, true, Encoding.UTF8))
            {
                sw.WriteLine("=== Группы без двоечников: ===");
                foreach (var group in groupsWithoutTwos)
                {
                    sw.WriteLine(group);
                }
                if (groupsWithoutTwos.Count == 0)
                    sw.WriteLine("Таких групп нет");
                sw.WriteLine();
            }
        }

        // 8) Фамилии студентов, у которых нет троек и двоек
        public void StudentsWithoutThreesAndTwos(string outputPath)
        {
            var goodStudents = Students.Where(s => s.HasNoThreesOrTwos).ToList();

            using (StreamWriter sw = new StreamWriter(outputPath, true, Encoding.UTF8))
            {
                sw.WriteLine("=== Студенты без троек и двоек: ===");
                foreach (var student in goodStudents)
                {
                    sw.WriteLine($"{student.LastName} {student.FirstName} - {student.Institute}, {student.Course} курс");
                }
                if (goodStudents.Count == 0)
                    sw.WriteLine("Таких студентов нет");
                sw.WriteLine();
            }
        }

        // 10) Фамилии студентов-отличников на третьем курсе
        public void ThirdYearExcellentStudents(string outputPath)
        {
            var thirdYearExcellent = Students
                .Where(s => s.Course == 3 && s.IsExcellentStudent)
                .ToList();

            using (StreamWriter sw = new StreamWriter(outputPath, true, Encoding.UTF8))
            {
                sw.WriteLine("=== Отличники на 3 курсе: ===");
                foreach (var student in thirdYearExcellent)
                {
                    sw.WriteLine($"{student.LastName} {student.FirstName} - {student.Institute}, группа {student.Group}");
                }
                if (thirdYearExcellent.Count == 0)
                    sw.WriteLine("Таких студентов нет");
                sw.WriteLine();
            }
        }

        // 12) Фамилии студентов, группу и институт, где средний бал составляет 4,5
        public void StudentsWithAverage45(string outputPath)
        {
            var studentsWithHighAverage = Students
                .Where(s => Math.Abs(s.AverageGrade - 4.5) < 0.1)
                .ToList();

            using (StreamWriter sw = new StreamWriter(outputPath, true, Encoding.UTF8))
            {
                sw.WriteLine("=== Студенты со средним баллом 4.5: ===");
                foreach (var student in studentsWithHighAverage)
                {
                    sw.WriteLine($"{student.LastName} {student.FirstName} - {student.Institute}, группа {student.Group}, средний балл: {student.AverageGrade:F2}");
                }
                if (studentsWithHighAverage.Count == 0)
                    sw.WriteLine("Таких студентов нет");
                sw.WriteLine();
            }
        }

        // 16) Институты, в которых нет двоечников
        public void InstitutesWithoutTwos(string outputPath)
        {
            var institutesWithTwos = Students
                .Where(s => s.CountOfTwos > 0)
                .Select(s => s.Institute)
                .Distinct()
                .ToList();

            var institutesWithoutTwos = Institutes
                .Where(institute => !institutesWithTwos.Contains(institute))
                .ToList();

            using (StreamWriter sw = new StreamWriter(outputPath, true, Encoding.UTF8))
            {
                sw.WriteLine("=== Институты без двоечников: ===");
                foreach (var institute in institutesWithoutTwos)
                {
                    sw.WriteLine(institute);
                }
                if (institutesWithoutTwos.Count == 0)
                    sw.WriteLine("Таких институтов нет");
                sw.WriteLine();
            }
        }

        // 23) Фамилии студентов-отличников на втором курсе с указанием группы и института
        public void SecondYearExcellentStudents(string outputPath)
        {
            var secondYearExcellent = Students
                .Where(s => s.Course == 2 && s.IsExcellentStudent)
                .ToList();

            using (StreamWriter sw = new StreamWriter(outputPath, true, Encoding.UTF8))
            {
                sw.WriteLine("=== Отличники на 2 курсе: ===");
                foreach (var student in secondYearExcellent)
                {
                    sw.WriteLine($"{student.LastName} {student.FirstName} - {student.Institute}, группа {student.Group}");
                }
                if (secondYearExcellent.Count == 0)
                    sw.WriteLine("Таких студентов нет");
                sw.WriteLine();
            }
        }

        // Сохранение всех данных в файл
        public void SaveAllData(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.WriteLine("=== ВСЕ ДАННЫЕ О СТУДЕНТАХ ===");
                foreach (var student in Students)
                {
                    sw.WriteLine($"\n{student.LastName} {student.FirstName}");
                    sw.WriteLine($"Институт: {student.Institute}");
                    sw.WriteLine($"Курс: {student.Course}");
                    sw.WriteLine($"Группа: {student.Group}");
                    sw.WriteLine($"Средний балл: {student.AverageGrade:F2}");
                    sw.WriteLine("Оценки:");
                    foreach (var grade in student.Grades)
                    {
                        sw.WriteLine($"  {grade.Key}: {grade.Value}");
                    }
                }
            }
        }

        // Загрузка данных из файла (упрощенная версия)
        public void LoadData(string filePath)
        {
            // Реализация загрузки данных из файла
            // В реальном приложении здесь был бы парсинг файла
            Console.WriteLine("Загрузка данных...");
        }
    }

    // Основная программа с меню
    class Program
    {
        static void Main(string[] args)
        {
            UniversityManager manager = new UniversityManager();
            manager.InitializeTestData();

            string outputDirectory = @"C:\UniversityReports";
            string allDataFile = Path.Combine(outputDirectory, "all_data.txt");
            string reportsFile = Path.Combine(outputDirectory, "reports.txt");

            // Создаем директорию, если она не существует
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== СИСТЕМА УПРАВЛЕНИЯ УНИВЕРСИТЕТОМ ===");
                Console.WriteLine("1. Показать всех студентов");
                Console.WriteLine("2. Сохранить все данные в файл");
                Console.WriteLine("3. Выполнить все запросы и сохранить в файл");
                Console.WriteLine("4. Удалить студентов с 2+ двойками");
                Console.WriteLine("5. Показать отличников");
                Console.WriteLine("6. Показать институты без двоечников");
                Console.WriteLine("7. Показать группы без двоечников");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ShowAllStudents(manager);
                        break;
                    case "2":
                        manager.SaveAllData(allDataFile);
                        Console.WriteLine($"Данные сохранены в файл: {allDataFile}");
                        break;
                    case "3":
                        ExecuteAllQueries(manager, reportsFile);
                        Console.WriteLine($"Все отчеты сохранены в файл: {reportsFile}");
                        break;
                    case "4":
                        manager.RemoveStudentsWithTwoOrMoreTwos(reportsFile);
                        Console.WriteLine("Операция выполнена");
                        break;
                    case "5":
                        ShowExcellentStudents(manager);
                        break;
                    case "6":
                        ShowInstitutesWithoutTwos(manager);
                        break;
                    case "7":
                        ShowGroupsWithoutTwos(manager);
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

        static void ShowAllStudents(UniversityManager manager)
        {
            Console.WriteLine("\n=== ВСЕ СТУДЕНТЫ ===");
            foreach (var student in manager.Students)
            {
                Console.WriteLine(student);
                Console.WriteLine("Оценки: " + string.Join(", ", student.Grades.Select(g => $"{g.Key}: {g.Value}")));
                Console.WriteLine();
            }
        }

        static void ShowExcellentStudents(UniversityManager manager)
        {
            Console.WriteLine("\n=== ОТЛИЧНИКИ ===");
            var excellentStudents = manager.Students.Where(s => s.IsExcellentStudent);
            foreach (var student in excellentStudents)
            {
                Console.WriteLine(student);
            }
            if (!excellentStudents.Any())
                Console.WriteLine("Отличников нет");
        }

        static void ShowInstitutesWithoutTwos(UniversityManager manager)
        {
            Console.WriteLine("\n=== ИНСТИТУТЫ БЕЗ ДВОЕЧНИКОВ ===");
            var institutesWithTwos = manager.Students
                .Where(s => s.CountOfTwos > 0)
                .Select(s => s.Institute)
                .Distinct()
                .ToList();

            var institutesWithoutTwos = manager.Institutes
                .Where(institute => !institutesWithTwos.Contains(institute))
                .ToList();

            foreach (var institute in institutesWithoutTwos)
            {
                Console.WriteLine(institute);
            }
            if (institutesWithoutTwos.Count == 0)
                Console.WriteLine("Таких институтов нет");
        }

        static void ShowGroupsWithoutTwos(UniversityManager manager)
        {
            Console.WriteLine("\n=== ГРУППЫ БЕЗ ДВОЕЧНИКОВ ===");
            var groupsWithTwos = manager.Students
                .Where(s => s.CountOfTwos > 0)
                .Select(s => s.Group)
                .Distinct()
                .ToList();

            var groupsWithoutTwos = manager.Students
                .Select(s => s.Group)
                .Distinct()
                .Where(group => !groupsWithTwos.Contains(group))
                .ToList();

            foreach (var group in groupsWithoutTwos)
            {
                Console.WriteLine(group);
            }
            if (groupsWithoutTwos.Count == 0)
                Console.WriteLine("Таких групп нет");
        }

        static void ExecuteAllQueries(UniversityManager manager, string outputPath)
        {
            // Очищаем файл перед записью новых отчетов
            File.WriteAllText(outputPath, "=== ОТЧЕТЫ ПО УНИВЕРСИТЕТУ ===\n\n", Encoding.UTF8);

            // Выполняем все запросы
            manager.RemoveStudentsWithTwoOrMoreTwos(outputPath);
            manager.InstituteWithMostFirstYearExcellentStudents(outputPath);
            manager.InstituteWithMostExcellentStudents(outputPath);
            manager.AllExcellentStudents(outputPath);
            manager.GroupsWithoutTwos(outputPath);
            manager.StudentsWithoutThreesAndTwos(outputPath);
            manager.ThirdYearExcellentStudents(outputPath);
            manager.StudentsWithAverage45(outputPath);
            manager.InstitutesWithoutTwos(outputPath);
            manager.SecondYearExcellentStudents(outputPath);
        }
    }
}