using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP.DataLayer.Entites
{
    public class Treatment
    {
        public Treatment()
        {
            MaterialConsumptions = new List<MaterialConsumption>();
        }
        public int TreatmentId { get; set; }
        public double Quantity { get; set; }

        //навигационные свойство
        public int VisitId { get; set; }
        public Visit Visit { get; set; }

        public int PriceListId { get; set; }
        public PriceList PriceList { get; set; }

        public List<MaterialConsumption> MaterialConsumptions { get; set; }     
    }
}
