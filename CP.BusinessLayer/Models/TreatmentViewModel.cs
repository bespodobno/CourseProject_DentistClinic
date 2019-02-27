using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.DataLayer.Entites;

namespace CP.BusinessLayer.Models
{
    public class TreatmentViewModel
    {
        public int TreatmentId { get; set; }
        public double Quantity { get; set; }
        
        public int VisitId { get; set; }
        public VisitViewModel Visit { get; set; }

        public int PriceListId { get; set; }
        public PriceListViewModel PriceList { get; set; }

        public ObservableCollection<MaterialConsumptionViewModel> MaterialConsumptions { get; set; }
    }
}
