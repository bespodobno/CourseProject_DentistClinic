using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP.BusinessLayer.Models
{
    public class Report
    {
        //public DateTime DateFrom { get; set; }
       // public DateTime DateTo { get; set; }
        public DoctorViewModel Doctor { get; set; }
        public double Income { get; set; }
        public double Expenses { get; set; }
        public double ProfitMargin { get; set; }
        public int Consultations { get; set; }

    }
}
