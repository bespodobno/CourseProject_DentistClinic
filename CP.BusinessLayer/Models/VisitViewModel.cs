using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.DataLayer.Entites;

namespace CP.BusinessLayer.Models
{
    public class VisitViewModel
    {
        public int VisitId { get; set; }       
        public DateTime VisitDate { get; set; }
        public string Diagnose { get; set; }
        public double VisitCost { get; set; }
        public double MaterialCost { get; set; }
        public double TotalCost { get; set; }
        public string VisitNumber { get; set; }

        //additional
        public int PatientId { get; set; }
        public PatientViewModel Patient { get; set; }

        public int DoctorId { get; set; }
        public DoctorViewModel Doctor { get; set; }

       // public int AppointmentId { get; set; }
       // public  AppointmentViewModel  Appointment { get; set; }

        public ObservableCollection<TreatmentViewModel> Treatments { get; set; }


        //методы
        public void CalculateCost()
        {
            double sumVisitCost = 0;
            double sumMaterialCost = 0;
            double sumTotalCost = 0;

            if (Treatments != null)
            {
                foreach (var t in Treatments)
                {
                    sumVisitCost += t.Quantity * t.PriceList.Price;
                }
            }
            foreach (var t in Treatments)
            {
                if (t.MaterialConsumptions != null)
                {
                    foreach (var m in t.MaterialConsumptions)
                    {
                        sumMaterialCost += Math.Round(m.ConsumptionQuant * m.SellingPrice);
                    }
                }
            }
            sumTotalCost = sumVisitCost + sumMaterialCost;
            this.VisitCost = sumVisitCost;
            this.MaterialCost = sumMaterialCost;
            this.TotalCost = sumTotalCost;
        }
    }
}
