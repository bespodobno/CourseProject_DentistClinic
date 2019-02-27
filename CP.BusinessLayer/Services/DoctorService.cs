using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP.BusinessLayer.Interfaces;
using CP.BusinessLayer.Models;
using CP.BusinessLayer.Services;
using System.Collections.ObjectModel;
using CP.DataLayer.Entites;
using CP.DataLayer.Interfaces;
using CP.DataLayer.Repositories;
using AutoMapper;

namespace CP.BusinessLayer.Services
{
    public class DoctorService:IDoctorService
    {
        IUnitOfWork dataBase;
        public DoctorService(IUnitOfWork dataBase)
        {
            this.dataBase = dataBase;
        }
        public ObservableCollection<DoctorViewModel> GetAll()
        {
            var doctorModels = Mapper.Map<ObservableCollection<DoctorViewModel>>(dataBase.Doctors.GetAll());
            return doctorModels;
        }
        public DoctorViewModel Get(int id)
        {
            var doctorEntity = dataBase.Doctors.Get(id);
            DoctorViewModel doctorModel = Mapper.Map<DoctorViewModel>(doctorEntity);
            return doctorModel;
        }
        public void CreateDoctor(DoctorViewModel doctorModel)
        {
            var doctorEntity = Mapper.Map<Doctor>(doctorModel);
            dataBase.Doctors.Create(doctorEntity);
            dataBase.Save();
        }
        public void DeleteDoctor(int doctorId)
        {
            dataBase.Doctors.Delete(doctorId);
            dataBase.Save();
        }
        public void UpdateDoctor(DoctorViewModel doctorModel)
        {
            Doctor doctorEntityDB = dataBase.Doctors.Get(doctorModel.DoctorId);
            //Doctor doctorEntity = Mapper.Map<Doctor>(doctorModel);
            doctorEntityDB.Category = doctorModel.Category;
            doctorEntityDB.DoctorName = doctorModel.DoctorName;
            doctorEntityDB.Speciality = doctorModel.Speciality;
            doctorEntityDB.Room = doctorModel.Room;            
            dataBase.Doctors.Update(doctorEntityDB);
            dataBase.Save();
        }
    }
}
