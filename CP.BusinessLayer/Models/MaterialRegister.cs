using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP.BusinessLayer.Models
{
    public class MaterialRegister //: IComparable<MaterialRegister>
    {
        public DateTime Date { get; set; }
        public double TotalCost { get; set; }
        public string Author { get; set; }
        public string TypeMaterialReceipt { get; set; }
        public string Storehouse { get; set; }
    }
}
