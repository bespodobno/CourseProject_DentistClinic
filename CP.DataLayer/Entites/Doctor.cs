using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CP.DataLayer.Entites
{
    public class Doctor
    {
        public Doctor()
        {
            Visits = new List<Visit>();
            Appointments = new List<Appointment>();
           
        }
        //[Key]
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Speciality { get; set; }
        public Category Category { get; set; }
        public int Room { get; set; }

        //навигационное свойство
        public List<Appointment>Appointments { get; set; }
        public List<Visit> Visits { get; set; }

    }
    public enum Category
    {
        первая,
        вторая,
        высшая,
        без_категории
    }
}
