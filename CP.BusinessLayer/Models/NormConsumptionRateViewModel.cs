using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP.BusinessLayer.Models
{
    public class NormConsumptionRateViewModel
    {
        public int NormConsumptionRateId { get; set; }
        public string TypeMaterial { get; set; }
        public string NameMaterial { get; set; }
        public double Norm { get; set; }
        public double PriceMaterial { get; set; }
        public bool AutoSelection { get; set; }

        public int PriceListId { get; set; }
        public PriceListViewModel PriceList { get; set; }

        public void CalculatePriceMaterial(double price)
        {
            PriceMaterial = price * 1.3;
        }
    }
}
