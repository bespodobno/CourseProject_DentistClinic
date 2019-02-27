using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.DataLayer.Entites;
using System.Collections.ObjectModel;


namespace CP.BusinessLayer.Models
{
    public class MaterialConsumptionViewModel
    {
        public int MaterialConsumptionId { get; set; }
        public double SellingPrice { get; set; }
        public double ConsumptionQuant { get; set; }

        //навигационные свойства
        public int TreatmentId { get; set; }
        public TreatmentViewModel Treatment { get; set; }

        public int DentalMaterialId { get; set; }
        public DentalMaterialViewModel DentalMaterial { get; set; }
    }
}
