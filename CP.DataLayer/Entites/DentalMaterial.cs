using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP.DataLayer.Entites
{
   public  class DentalMaterial
    {
        public DentalMaterial()
        {            
            MaterialConsumptions = new List<MaterialConsumption>();
            
        }

        public int DentalMaterialId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string TypeMaterial { get; set; }
        public string MaterialName { get; set; }
        public Unit Unit { get; set; }
        public double MaterialPrice { get; set; }
        public double MaterialQuantity { get; set; }

        //навигационное свойства        

        public List<MaterialConsumption> MaterialConsumptions { get; set; }
        
    }
    public enum Unit
    {
        шт,// pcs,
        упак,//pkge,
        г,//gr,
        набор,//set,
        см,//sm,
        мм,//mm,
        мл//ml
    }   
}
