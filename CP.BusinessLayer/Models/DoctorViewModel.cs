using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.DataLayer.Entites;

namespace CP.BusinessLayer.Models
{
    public class DoctorViewModel
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Speciality { get; set; }
        public Category Category { get; set; }
        public int Room { get; set; }
        public ObservableCollection <AppointmentViewModel> Appointments { get; set; }
        public ObservableCollection<VisitViewModel> Visits { get; set; }
    }
}
