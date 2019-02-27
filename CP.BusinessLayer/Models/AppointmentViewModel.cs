using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CP.DataLayer.Entites;


namespace CP.BusinessLayer.Models
{
   public  class AppointmentViewModel
    {
       public int AppointmentId { get; set; }
       public DateTime Date { get; set; }
       public TimeSpan Time { get; set; }

       public int DoctorId { get; set; }
       public DoctorViewModel Doctor { get; set; }

       public int PatientId { get; set; }
       public PatientViewModel Patient { get; set; }

    }
}
