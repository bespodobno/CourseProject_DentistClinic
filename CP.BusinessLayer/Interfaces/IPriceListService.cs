using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.BusinessLayer.Models;

namespace CP.BusinessLayer.Interfaces
{
    public interface IPriceListService
    {
        ObservableCollection<PriceListViewModel> GetAll();
        PriceListViewModel Get(int id);        
        void CreatePriceList(PriceListViewModel priceListModel);
        void DeletePriceList(int priceListId);
        void UpdatePriceList(PriceListViewModel priceListModel);
    }
}
