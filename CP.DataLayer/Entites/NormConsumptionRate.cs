using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP.DataLayer.Entites
{
    public class NormConsumptionRate
    {        
        public int NormConsumptionRateId { get; set; }
        public string TypeMaterial { get; set; }
        public string NameMaterial { get; set; }
        public double Norm { get; set; }
        public double PriceMaterial { get; set; }
        public bool AutoSelection { get; set; }

        //навигационные свойства

        public int PriceListId { get; set; }
        public PriceList PriceList { get; set; }       
        
    }

}


