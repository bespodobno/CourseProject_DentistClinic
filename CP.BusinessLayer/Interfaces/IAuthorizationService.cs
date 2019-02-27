using System.Collections.ObjectModel;
using CP.BusinessLayer.Models;

namespace CP.BusinessLayer.Interfaces
{
    public interface IAuthorizationService
    {
        ObservableCollection<AuthorizationViewModel> GetAll();
        AuthorizationViewModel Get(int id);
        void CreateAuthorization(AuthorizationViewModel authorizationModel);
        void DeleteAuthorization(int authorizationId);
        void UpdateAuthorization(AuthorizationViewModel authorizationModel);
        AuthorizationViewModel GetUserCredentials(AuthorizationViewModel user);       
    }
}
