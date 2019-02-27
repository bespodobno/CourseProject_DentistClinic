using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP.DataLayer.Entites
{
    public class Visit
    {
        public Visit()
        {
            Treatments = new List<Treatment>();            
        }
        
        public int VisitId { get; set; }
        public string VisitNumber { get; set; }
        public DateTime VisitDate { get; set; }
        public string Diagnose { get; set; }
        public double VisitCost { get; set; }
        public double MaterialCost { get; set; }
        public double TotalCost { get; set; }

        //навигационное свойство
        public int PatientId { get; set; }
        public Patient Patient { get; set; }    
        
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public List<Treatment> Treatments { get; set; }      

    }
}
