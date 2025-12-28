using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MedAutomation.Models;

namespace MedAutomation.Data
{
    public class Repository
    {
        private readonly string _folder;

        public List<Patient> Patients { get; set; } = new();
        public List<Doctor> Doctors { get; set; } = new();
        public List<Appointment> Appointments { get; set; } = new();
        public List<MedicalRecord> MedicalRecords { get; set; } = new();

        public Repository(string folder)
        {
            _folder = folder;
            if (!Directory.Exists(_folder)) Directory.CreateDirectory(_folder);
        }

        private void Save<T>(string filename, List<T> list)
        {
            var path = Path.Combine(_folder, filename);
            var opt = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(path, JsonSerializer.Serialize(list, opt));
        }

        private List<T> Load<T>(string filename)
        {
            try
            {
                var path = Path.Combine(_folder, filename);
                if (!File.Exists(path)) return new List<T>();
                var text = File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<T>>(text) ?? new List<T>();
            }
            catch
            {
                return new List<T>();
            }
        }

        public void SaveAll()
        {
            Save("patients.json", Patients);
            Save("doctors.json", Doctors);
            Save("appointments.json", Appointments);
            Save("medicalrecords.json", MedicalRecords);
        }

        public void LoadAll()
        {
            Patients = Load<Patient>("patients.json");
            Doctors = Load<Doctor>("doctors.json");
            Appointments = Load<Appointment>("appointments.json");
            MedicalRecords = Load<MedicalRecord>("medicalrecords.json");

            // Если врачей нет — добавить пример
            if (Doctors.Count == 0)
            {
                Doctors.Add(new Doctor { FullName = "Иванов И.И.", Specialty = "Терапевт" });
                Doctors.Add(new Doctor { FullName = "Петров П.П.", Specialty = "Хирург" });
            }
        }
    }
}
