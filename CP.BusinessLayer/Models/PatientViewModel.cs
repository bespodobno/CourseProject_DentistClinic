using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CP.DataLayer.Entites;

namespace CP.BusinessLayer.Models
{
    public class PatientViewModel
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public Sex Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Contract { get; set; }

        public ObservableCollection<AppointmentViewModel> Appointments { get; set; }
        public ObservableCollection<VisitViewModel> Visits { get; set; }

    }
}
