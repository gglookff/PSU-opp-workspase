using System;

namespace MedAutomation.Models
{
    public class Appointment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public string Date { get; set; } // yyyy-MM-dd
        public string Time { get; set; } // HH:mm
        public string Reason { get; set; }
        public string Status { get; set; } = "Запланировано"; // Запланировано, Отменено, Завершено
    }
}
