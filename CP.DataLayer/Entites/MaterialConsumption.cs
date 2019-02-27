using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP.DataLayer.Entites
{
    public class MaterialConsumption
    {
        public int MaterialConsumptionId { get; set; }
        public double SellingPrice { get; set; }
        public double ConsumptionQuant { get; set; }

        //навигационные свойства
        public int TreatmentId { get; set; }
        public Treatment Treatment { get; set; }

        public int DentalMaterialId { get; set; }
        public DentalMaterial DentalMaterial { get; set; }
    }
}
