using System;
using System.Collections.Generic;

namespace MedAutomation.Models
{
    public class Doctor
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public string Specialty { get; set; }

        // Рабочие слоты: ключ - дата (yyyy-MM-dd), значение - список часов (например "09:00")
        public Dictionary<string, List<string>> Schedule { get; set; } = new();
    }
}
