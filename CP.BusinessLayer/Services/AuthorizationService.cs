using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.BusinessLayer.Interfaces;
using CP.BusinessLayer.Models;
using CP.DataLayer.Entites;
using CP.DataLayer.Interfaces;
using CP.DataLayer.Repositories;
using AutoMapper;
using System.Collections.ObjectModel;

namespace CP.BusinessLayer.Services
{
    public class AuthorizationService :IAuthorizationService
    {
        IUnitOfWork dataBase;
        public AuthorizationService(IUnitOfWork dataBase)
        {
            this.dataBase = dataBase;

        }
        public ObservableCollection<AuthorizationViewModel> GetAll()
        {
            var authorizationModels = Mapper.Map<ObservableCollection<AuthorizationViewModel>>(dataBase.Authorizations.GetAll());
            return authorizationModels;
        }
        public AuthorizationViewModel Get(int id)
        {
            var authorizationEntity = dataBase.Authorizations.Get(id);
            AuthorizationViewModel authorizationModel = Mapper.Map<AuthorizationViewModel>(authorizationEntity);
            return authorizationModel;
        }
        public void CreateAuthorization(AuthorizationViewModel authorizationModel)
        {
            var authorizationEntity = Mapper.Map<Authorization>(authorizationModel);
            //добавить запись на прием
            dataBase.Authorizations.Create(authorizationEntity);
            //Сохранить изменения
            dataBase.Save();
        }
        public void DeleteAuthorization(int authorizationId)
        {
            dataBase.Authorizations.Delete(authorizationId);
            dataBase.Save();
        }
        public void UpdateAuthorization(AuthorizationViewModel authorizationsModel)
        {
            Authorization authorizationEntityDB = dataBase.Authorizations.Get(authorizationsModel.AuthorizationId);
            // Appointment appointmentEntity = Mapper.Map<Appointment>(appointmentModel);
            authorizationEntityDB.User = authorizationsModel.User;
            authorizationEntityDB.Password = authorizationsModel.Password;
            authorizationEntityDB.Role = authorizationsModel.Role;
           

            dataBase.Authorizations.Update(authorizationEntityDB);
            dataBase.Save();
        }

        public AuthorizationViewModel GetUserCredentials(AuthorizationViewModel user)
        {
           var authorizationModels = Mapper.Map<ObservableCollection<AuthorizationViewModel>>(dataBase.Authorizations.GetAll());
            AuthorizationViewModel authorizationViewModel = authorizationModels.Where(a => a.User == user.User && a.Password==user.Password).FirstOrDefault();
            return authorizationViewModel;           
        }
      
    }
}