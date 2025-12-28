using System;
using System.Linq;
using MedAutomation.Data;
using MedAutomation.Models;
using MedAutomation.Utils;
using System.Globalization;

namespace MedAutomation.Services
{
    public class ClinicService
    {
        private readonly Repository _repo;

        public ClinicService(Repository repo)
        {
            _repo = repo;
        }

        public void RegisterPatient()
        {
            ConsoleUtils.WriteHeader("Регистрация нового пациента");
            var name = ConsoleUtils.ReadNonEmpty("ФИО: ");
            var birth = ConsoleUtils.ReadDate("Дата рождения (yyyy-MM-dd): ");
            var phone = ConsoleUtils.ReadNonEmpty("Телефон: ");
            var email = ConsoleUtils.Read("Email (опционально): ");
            var addr = ConsoleUtils.Read("Адрес (опционально): ");

            var p = new Patient
            {
                FullName = name,
                BirthDate = birth,
                Phone = phone,
                Email = email,
                Address = addr
            };
            _repo.Patients.Add(p);
            _repo.SaveAll();
            Console.WriteLine($"Пациент добавлен. ID: {p.Id}");
        }

        public void ListPatients()
        {
            ConsoleUtils.WriteHeader("Список пациентов");
            if (!_repo.Patients.Any()) { Console.WriteLine("Пациентов нет."); return; }
            foreach (var p in _repo.Patients)
            {
                Console.WriteLine($"{p.Id} | {p.FullName} | {p.BirthDate} | {p.Phone}");
            }
        }

        public void EditPatient()
        {
            ConsoleUtils.WriteHeader("Редактирование пациента");
            ListPatients();
            var id = ConsoleUtils.ReadNonEmpty("Введите ID пациента: ");
            if (!Guid.TryParse(id, out var gid)) { Console.WriteLine("Неверный ID."); return; }
            var p = _repo.Patients.FirstOrDefault(x => x.Id == gid);
            if (p == null) { Console.WriteLine("Пациент не найден."); return; }

            Console.WriteLine("Оставьте поле пустым, чтобы не менять.");
            var name = ConsoleUtils.Read($"ФИО ({p.FullName}): ");
            var birth = ConsoleUtils.Read($"Дата рождения ({p.BirthDate}): ");
            var phone = ConsoleUtils.Read($"Телефон ({p.Phone}): ");
            var email = ConsoleUtils.Read($"Email ({p.Email}): ");
            var addr = ConsoleUtils.Read($"Адрес ({p.Address}): ");

            if (!string.IsNullOrWhiteSpace(name)) p.FullName = name;
            if (!string.IsNullOrWhiteSpace(birth)) p.BirthDate = birth;
            if (!string.IsNullOrWhiteSpace(phone)) p.Phone = phone;
            if (!string.IsNullOrWhiteSpace(email)) p.Email = email;
            if (!string.IsNullOrWhiteSpace(addr)) p.Address = addr;

            _repo.SaveAll();
            Console.WriteLine("Данные обновлены.");
        }

        public void ManageDoctorsSchedule()
        {
            ConsoleUtils.WriteHeader("Управление расписанием врачей");
            var docs = _repo.Doctors;
            for (int i = 0; i < docs.Count; i++)
            {
                Console.WriteLine($"{i+1}. {docs[i].FullName} ({docs[i].Specialty})");
            }
            Console.WriteLine("a - добавить врача");
            Console.Write("Выбор врача (номер) или 'a': ");
            var choice = Console.ReadLine();
            if (choice == "a")
            {
                var name = ConsoleUtils.ReadNonEmpty("ФИО врача: ");
                var spec = ConsoleUtils.ReadNonEmpty("Специальность: ");
                _repo.Doctors.Add(new Doctor { FullName = name, Specialty = spec });
                _repo.SaveAll();
                Console.WriteLine("Врач добавлен.");
                return;
            }

            if (!int.TryParse(choice, out int idx) || idx < 1 || idx > docs.Count) { Console.WriteLine("Неверный выбор."); return; }
            var doc = docs[idx-1];

            Console.WriteLine($"Выбран: {doc.FullName}. Доступные действия:");
            Console.WriteLine("1. Добавить рабочий слот");
            Console.WriteLine("2. Удалить рабочий слот");
            Console.WriteLine("3. Показать график");
            var act = Console.ReadLine();
            switch (act)
            {
                case "1":
                    var date = ConsoleUtils.ReadDate("Дата (yyyy-MM-dd): ");
                    var time = ConsoleUtils.ReadTime("Время (HH:mm): ");
                    if (!doc.Schedule.ContainsKey(date)) doc.Schedule[date] = new System.Collections.Generic.List<string>();
                    if (!doc.Schedule[date].Contains(time)) doc.Schedule[date].Add(time);
                    _repo.SaveAll();
                    Console.WriteLine("Слот добавлен.");
                    break;
                case "2":
                    var d2 = ConsoleUtils.ReadDate("Дата (yyyy-MM-dd): ");
                    if (!doc.Schedule.ContainsKey(d2)) { Console.WriteLine("Слотов в этот день нет."); break; }
                    Console.WriteLine("Существующие времена:");
                    for (int i = 0; i < doc.Schedule[d2].Count; i++) Console.WriteLine($"{i+1}. {doc.Schedule[d2][i]}");
                    var ti = ConsoleUtils.Read("Номер времени для удаления: ");
                    if (int.TryParse(ti, out int tix) && tix >=1 && tix <= doc.Schedule[d2].Count)
                    {
                        doc.Schedule[d2].RemoveAt(tix-1);
                        _repo.SaveAll();
                        Console.WriteLine("Слот удалён.");
                    }
                    else Console.WriteLine("Неверно.");
                    break;
                case "3":
                    Console.WriteLine($"График врача {doc.FullName}:");
                    foreach (var kv in doc.Schedule.OrderBy(x => x.Key))
                    {
                        Console.WriteLine($"{kv.Key}: {string.Join(", ", kv.Value)}");
                    }
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }

        public void BookAppointment()
        {
            ConsoleUtils.WriteHeader("Запись на приём");
            ListPatients();
            var pidStr = ConsoleUtils.ReadNonEmpty("ID пациента: ");
            if (!Guid.TryParse(pidStr, out var pid) || !_repo.Patients.Any(p => p.Id == pid)) { Console.WriteLine("Пациент не найден."); return; }

            Console.WriteLine("Врачи:");
            for (int i = 0; i < _repo.Doctors.Count; i++)
                Console.WriteLine($"{i+1}. {_repo.Doctors[i].FullName} ({_repo.Doctors[i].Specialty})");

            var docChoice = ConsoleUtils.ReadNonEmpty("Выберите номер врача: ");
            if (!int.TryParse(docChoice, out int dindex) || dindex < 1 || dindex > _repo.Doctors.Count) { Console.WriteLine("Неверный выбор."); return; }
            var doc = _repo.Doctors[dindex-1];

            var date = ConsoleUtils.ReadDate("Дата (yyyy-MM-dd): ");
            if (!doc.Schedule.ContainsKey(date) || doc.Schedule[date].Count == 0) { Console.WriteLine("Свободных слотов нет."); return; }

            Console.WriteLine("Доступные времена:");
            for (int i = 0; i < doc.Schedule[date].Count; i++)
                Console.WriteLine($"{i+1}. {doc.Schedule[date][i]}");

            var timeChoice = ConsoleUtils.ReadNonEmpty("Выберите номер времени: ");
            if (!int.TryParse(timeChoice, out int timeIndex) || timeIndex < 1 || timeIndex > doc.Schedule[date].Count) { Console.WriteLine("Неверный выбор."); return; }
            var time = doc.Schedule[date][timeIndex-1];

            // Проверка конфликтов
            if (_repo.Appointments.Any(a => a.DoctorId == doc.Id && a.Date == date && a.Time == time && a.Status == "Запланировано"))
            {
                Console.WriteLine("Слот уже занят.");
                return;
            }

            var reason = ConsoleUtils.Read("Причина визита: ");
            var ap = new Appointment
            {
                PatientId = pid,
                DoctorId = doc.Id,
                Date = date,
                Time = time,
                Reason = reason
            };
            _repo.Appointments.Add(ap);
            _repo.SaveAll();
            Console.WriteLine($"Запись создана. ID: {ap.Id}");
        }

        public void ViewAppointments()
        {
            ConsoleUtils.WriteHeader("Просмотр записей");
            Console.WriteLine("1. Все записи");
            Console.WriteLine("2. По пациенту");
            Console.WriteLine("3. По врачу");
            var ch = Console.ReadLine();
            var list = _repo.Appointments.AsEnumerable();
            switch (ch)
            {
                case "2":
                    ListPatients();
                    var pid = ConsoleUtils.ReadNonEmpty("ID пациента: ");
                    if (Guid.TryParse(pid, out var gid)) list = list.Where(a => a.PatientId == gid);
                    break;
                case "3":
                    for (int i = 0; i < _repo.Doctors.Count; i++) Console.WriteLine($"{i+1}. {_repo.Doctors[i].FullName}");
                    var di = ConsoleUtils.ReadNonEmpty("Номер врача: ");
                    if (int.TryParse(di, out int dix) && dix>=1 && dix<=_repo.Doctors.Count)
                    {
                        var did = _repo.Doctors[dix-1].Id;
                        list = list.Where(a => a.DoctorId == did);
                    }
                    break;
                default:
                    break;
            }

            foreach (var a in list.OrderBy(x => x.Date).ThenBy(x => x.Time))
            {
                var pat = _repo.Patients.FirstOrDefault(p => p.Id == a.PatientId);
                var doc = _repo.Doctors.FirstOrDefault(d => d.Id == a.DoctorId);
                Console.WriteLine($"{a.Id} | {a.Date} {a.Time} | Пациент: {pat?.FullName} | Врач: {doc?.FullName} | {a.Status}");
            }

            Console.WriteLine("Действия: c - отменить запись, m - отметить как выполненную, Enter - назад");
            var act = Console.ReadLine();
            if (act == "c")
            {
                var id = ConsoleUtils.ReadNonEmpty("ID записи для отмены: ");
                if (Guid.TryParse(id, out var gid))
                {
                    var a = _repo.Appointments.FirstOrDefault(x => x.Id == gid);
                    if (a != null) { a.Status = "Отменено"; _repo.SaveAll(); Console.WriteLine("Отменено."); }
                }
            }
            if (act == "m")
            {
                var id = ConsoleUtils.ReadNonEmpty("ID записи для отметки: ");
                if (Guid.TryParse(id, out var gid))
                {
                    var a = _repo.Appointments.FirstOrDefault(x => x.Id == gid);
                    if (a != null) { a.Status = "Завершено"; _repo.SaveAll(); Console.WriteLine("Отмечено как завершено."); }
                }
            }
        }

        public void ManageMedicalRecords()
        {
            ConsoleUtils.WriteHeader("Ведение медицинских карт");
            ListPatients();
            var pidStr = ConsoleUtils.ReadNonEmpty("ID пациента: ");
            if (!Guid.TryParse(pidStr, out var pid) || !_repo.Patients.Any(p => p.Id == pid)) { Console.WriteLine("Пациент не найден."); return; }

            Console.WriteLine("1. Показать все записи по пациенту");
            Console.WriteLine("2. Добавить запись (осмотр/диагноз/назначение)");
            var ch = Console.ReadLine();
            if (ch == "1")
            {
                var recs = _repo.MedicalRecords.Where(r => r.PatientId == pid).OrderByDescending(r => r.CreatedAt);
                foreach (var r in recs)
                {
                    Console.WriteLine($"{r.Id} | {r.CreatedAt:u}\nДиагноз: {r.Diagnoses}\nНазначения: {r.Prescriptions}\nЗапись врача: {r.DoctorNotes}\n---");
                }
            }
            else if (ch == "2")
            {
                var docName = ConsoleUtils.Read("ФИО врача: ");
                var diag = ConsoleUtils.Read("Диагноз: ");
                var pres = ConsoleUtils.Read("Назначения: ");
                var notes = ConsoleUtils.Read("Замечания врача: ");
                _repo.MedicalRecords.Add(new MedicalRecord
                {
                    PatientId = pid,
                    Diagnoses = diag,
                    Prescriptions = pres,
                    DoctorNotes = $"Врач: {docName}; {notes}"
                });
                _repo.SaveAll();
                Console.WriteLine("Запись добавлена.");
            }
        }

        public void ReportsAndExport()
        {
            ConsoleUtils.WriteHeader("Отчёты и экспорт данных");
            Console.WriteLine("1. Отчёт: нагрузка врачей (кол-во записей)");
            Console.WriteLine("2. Экспорт всех данных в JSON (data/export_all.json)");
            var ch = Console.ReadLine();
            if (ch == "1")
            {
                foreach (var d in _repo.Doctors)
                {
                    var cnt = _repo.Appointments.Count(a => a.DoctorId == d.Id && a.Status == "Запланировано");
                    Console.WriteLine($"{d.FullName} ({d.Specialty}) — запланировано: {cnt}");
                }
            }
            else if (ch == "2")
            {
                var export = new
                {
                    Patients = _repo.Patients,
                    Doctors = _repo.Doctors,
                    Appointments = _repo.Appointments,
                    MedicalRecords = _repo.MedicalRecords
                };
                var path = System.IO.Path.Combine("data", "export_all.json");
                var opt = new System.Text.Json.JsonSerializerOptions { WriteIndented = true };
                System.IO.File.WriteAllText(path, System.Text.Json.JsonSerializer.Serialize(export, opt));
                Console.WriteLine($"Данные экспортированы в {path}");
            }
        }
    }
}
