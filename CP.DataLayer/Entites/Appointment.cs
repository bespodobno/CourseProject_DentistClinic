using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP.DataLayer.Entites
{
    public class Appointment
    {       
        public int AppointmentId { get; set; }        
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        //навигационные свойства
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }      
    }
    
}
