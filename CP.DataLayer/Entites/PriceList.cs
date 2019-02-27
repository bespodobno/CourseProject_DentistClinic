using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP.DataLayer.Entites
{
    public class PriceList
    {
        public PriceList()
        {
            Treatments = new List<Treatment>();
            NormConsumptionRates = new List<NormConsumptionRate>();
        }
        public int PriceListId { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }       

        //навигационное свойство        

        public List<NormConsumptionRate> NormConsumptionRates { get; set; }
        public List<Treatment> Treatments { get; set; }
    }
}
