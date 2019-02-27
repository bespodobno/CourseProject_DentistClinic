using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP.DataLayer.Entites
{
     public class Patient
    {
        public Patient()
        {
            Appointments = new List<Appointment>();
            Visits = new List<Visit>();
        }

        public int PatientId { get; set; }
        public string Name { get; set; }
        public Sex Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Contract { get; set; }

        //навигационное свойство
        public List<Appointment> Appointments { get; set; }
        public List<Visit> Visits { get; set; }

    }
    public enum Sex
    {
        жен,
        муж
    }
}
