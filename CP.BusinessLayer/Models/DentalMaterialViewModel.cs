using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CP.DataLayer.Entites;

namespace CP.BusinessLayer.Models
{
    public class DentalMaterialViewModel
    {
        public int DentalMaterialId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string TypeMaterial { get; set; }
        public string MaterialName { get; set; }
        public Unit Unit { get; set; }       
        public double MaterialPrice { get; set; }
        public double MaterialQuantity { get; set; }
        
        public double MaterialCost { get; set; }


        public ObservableCollection<MaterialConsumptionViewModel> MaterialConsumptions { get; set; }
        public void CalculateCost()
        {

            MaterialCost = MaterialPrice * MaterialQuantity;
        }
    }
}
