using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CP.DataLayer.Entites;

namespace CP.BusinessLayer.Models
{
    public class PriceListViewModel
    {
        public int PriceListId { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }       

        public ObservableCollection<NormConsumptionRateViewModel> NormConsumptionRates { get; set; }
        public ObservableCollection<Treatment> Treatments { get; set; }
    }
}
