using System;

namespace MedAutomation.Models
{
    public class MedicalRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PatientId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string DoctorNotes { get; set; }
        public string Diagnoses { get; set; }
        public string Prescriptions { get; set; }
    }
}
