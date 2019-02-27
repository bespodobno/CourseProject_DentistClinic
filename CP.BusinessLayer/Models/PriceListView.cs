using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP.BusinessLayer.Models
{
    public class PriceListView
    {
        public PriceListViewModel PriceList { get; set; }
        public string MaterialName { get; set; }
        public double MaterialCost { get; set; }
        public double TotalCost { get; set; }

        public override string ToString()
        {
            return PriceList.ServiceCode + " " + PriceList.ServiceName + " " + PriceList.Price + " " + MaterialName + " "
                + MaterialCost + " " + TotalCost;
          }
    }
   
}
