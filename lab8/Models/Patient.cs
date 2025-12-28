using System;

namespace MedAutomation.Models
{
    public class Patient
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public string BirthDate { get; set; } // yyyy-MM-dd
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
