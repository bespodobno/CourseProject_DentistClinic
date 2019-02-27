using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using CP.BusinessLayer.Interfaces;
using CP.BusinessLayer.Models;
using CP.BusinessLayer.Services;
using System.Collections.ObjectModel;
using CP.DataLayer.Entites;
using CP.DataLayer.Interfaces;
using CP.DataLayer.Repositories;
using AutoMapper;
using System.ComponentModel;
using Microsoft.Win32;
using System.IO;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Controls.DataVisualization;
using Microsoft.Office.Interop;
using System.Diagnostics;


namespace CourseProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        IUnitOfWork dataBase;
        ObservableCollection<PatientViewModel> patientsModel;
        ObservableCollection<DoctorViewModel> doctorsModel;
        ObservableCollection<AppointmentViewModel> appointmentModel;
        ObservableCollection<DentalMaterialViewModel> dentalMaterialModel;
        ObservableCollection<VisitViewModel> visitsModel;
        ObservableCollection<TreatmentViewModel> treatmentsModel;
        ObservableCollection<PriceListViewModel> priceListViewModel;
        ObservableCollection<NormConsumptionRateViewModel> normConsumptionRateModel;
        ObservableCollection<MaterialConsumptionViewModel> materialConsumptionModel;

        IPatientService patientService;
        IAuthorizationService authorizationService;
        IAppointmentService appointmentService;
        IDoctorService doctorService;
        IVisitService visitService;
        IDentalMaterialService dentalMaterialService;
        ITreatmentService treatmentService;
        IPriceListService priceListService;
        INormConsumptionRateService normConsumptionService;
        IMaterialConsumptionService materialConsumptionService;

        ObservableCollection<MaterialRegister> materialRegister = new ObservableCollection<MaterialRegister>();
        ObservableCollection<PriceListView> priceListView = new ObservableCollection<PriceListView>();
        ObservableCollection<Report> reports = new ObservableCollection<Report>();
        AuthorizationViewModel authorization = new AuthorizationViewModel();



        public MainWindow()
        {
            InitializeComponent();

            LoadDataFromDatabase();

            dGridAppointment.DataContext = appointmentModel;
            dGridVisit.ItemsSource = visitsModel;
            dGridPatients.DataContext = patientsModel;
            cBoxDoctor.DataContext = doctorsModel;

            try
            {
                FillPriceListView();
                FillMaterialRegister();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dGridPrice.DataContext = priceListView;
            dGridMaterial.DataContext = materialRegister;


        }



        private void LoadDataFromDatabase()
        {
            dataBase = new EntityUnitOfWork("DBCP");

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Patient, PatientViewModel>().PreserveReferences();
                cfg.CreateMap<Appointment, AppointmentViewModel>().PreserveReferences();
                cfg.CreateMap<Doctor, DoctorViewModel>().PreserveReferences();

                cfg.CreateMap<Visit, VisitViewModel>()
                .ForMember(dest => dest.Doctor, opts => opts.MapFrom(src => src.Doctor))
                .ForMember(dest => dest.Patient, opts => opts.MapFrom(src => src.Patient))
                .ForMember(dest => dest.Treatments, opts => opts.MapFrom(src => src.Treatments)).PreserveReferences();
                // .ForMember(dest => dest.Treatments.Select(x => x.PriceList),opts => opts.MapFrom(src => src.Treatments.Select(x => x.PriceList))); 


                cfg.CreateMap<Treatment, TreatmentViewModel>()
                .ForMember(dest => dest.PriceList, opts => opts.MapFrom(src => src.PriceList)).PreserveReferences()
                 .ForMember(dest => dest.MaterialConsumptions, opts => opts.MapFrom(src => src.MaterialConsumptions))
                  //.ForMember(dest => dest.MaterialConsumptions.Select(x => x.DentalMaterial),
                  //          opts=>opts.MapFrom(src=>src.MaterialConsumptions.Select(x => x.DentalMaterial)))
                  .PreserveReferences();

                cfg.CreateMap<PriceList, PriceListViewModel>().PreserveReferences();
                cfg.CreateMap<NormConsumptionRate, NormConsumptionRateViewModel>().PreserveReferences();
                cfg.CreateMap<MaterialConsumption, MaterialConsumptionViewModel>().PreserveReferences();
                cfg.CreateMap<DentalMaterial, DentalMaterialViewModel>().PreserveReferences();
                cfg.CreateMap<Authorization, AuthorizationViewModel>().PreserveReferences();
            });

            authorizationService = new AuthorizationService(dataBase);
            patientService = new PatientService(dataBase);
            appointmentService = new AppointmentService(dataBase);
            doctorService = new DoctorService(dataBase);
            visitService = new VisitService(dataBase);
            dentalMaterialService = new DentalMaterialService(dataBase);
            treatmentService = new TreatmentService(dataBase);
            priceListService = new PriceListService(dataBase);
            normConsumptionService = new NormConsumptionRateService(dataBase);
            materialConsumptionService = new MaterialConsumptionService(dataBase);

            visitsModel = visitService.GetAll();
            patientsModel = patientService.GetAll();
            doctorsModel = doctorService.GetAll();
            appointmentModel = appointmentService.GetAll();
            priceListViewModel = priceListService.GetAll();
            dentalMaterialModel = dentalMaterialService.GetAll();
            normConsumptionRateModel = normConsumptionService.GetAll();
            materialConsumptionModel = materialConsumptionService.GetAll();
            treatmentsModel = treatmentService.GetAll();
        }

        private void DataWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //авторизация

            Authentication authentication = new Authentication(authorization);

            if (authentication.ShowDialog() == true)
            {

                var authorizationModel = authorizationService.GetUserCredentials(authorization);

                if (authorizationModel == null)
                {
                    MessageBox.Show("Неверный пользователь или пароль", "Авторизация не пройдена!", MessageBoxButton.OK, MessageBoxImage.Error);

                    AdditionalFunctions.LogMessageToFile("Вход. Пользователь: " + authorization.User + " неавторизированная попытка входа");

                    Environment.Exit(0);
                }
                this.Title = "Стоматологический кабинет. Пользователь: " + authorizationModel.User + " c ролью " + authorizationModel.Role;

                AdditionalFunctions.LogMessageToFile("Вход. Пользователь: " + authorization.User + " c ролью " + authorization.Role);

                if (authorizationModel.Role == Role.Registrator)
                {
                    TabReports.Visibility = Visibility.Hidden;
                    TabMaterialReceipts.Visibility = Visibility.Hidden;
                    ToolBarConsultation.Visibility = Visibility.Hidden;
                    btAddLinePrice.Visibility = Visibility.Hidden;
                    //btnChangeLinePrice.Visibility = Visibility.Hidden;
                    btnDeleteLinePrice.Visibility = Visibility.Hidden;

                }
                else if (authorizationModel.Role == Role.Dentist)
                {
                    TabControl.Items.Remove(TabMaterialReceipts);
                    btAddLinePrice.Visibility = Visibility.Hidden;
                   // btnChangeLinePrice.Visibility = Visibility.Hidden;
                    btnDeleteLinePrice.Visibility = Visibility.Hidden;
                }

            }
        }

        private void FillPriceListView()
        {
            priceListView.Clear();
            foreach (var pricelist in priceListViewModel)
            {
                double sum = 0;
                if (pricelist.NormConsumptionRates.Count != 0)
                {
                    foreach (var norm in pricelist.NormConsumptionRates)
                    {

                        if (norm.AutoSelection == false)
                        {
                            sum += norm.Norm * norm.PriceMaterial;
                            double sumAuto = 0;
                            foreach (var normAuto in pricelist.NormConsumptionRates)
                            {

                                if (normAuto.AutoSelection == true)
                                {
                                    sumAuto += normAuto.Norm * normAuto.PriceMaterial;
                                }
                            }
                            sum += sumAuto;
                            priceListView.Add(new PriceListView
                            {
                                PriceList = pricelist,
                                MaterialName = norm.NameMaterial,
                                MaterialCost = sum,
                                TotalCost = sum + pricelist.Price
                            });
                        }
                    }
                }
                else
                {
                    priceListView.Add(new PriceListView
                    {
                        PriceList = pricelist,
                        MaterialName = "",
                        MaterialCost = 0,
                        TotalCost = pricelist.Price
                    });
                }

            }
        }

        private void FillMaterialRegisterWithFilter(DateTime DateFrom, DateTime DateTo)
        {
            if (DateFrom != null || DateTo != null)
            {
                materialRegister.Clear();
                DateTime date;

                foreach (var mat in dentalMaterialModel)
                {
                    date = mat.PurchaseDate;
                    if (date >= DateFrom && date <= DateTo)
                    {
                        if (materialRegister != null && materialRegister.Where(m => m.Date == date).Any())
                        {
                            continue;
                        }

                        double sum = 0;
                        foreach (var m in dentalMaterialModel.Where(v => v.PurchaseDate == date))
                        {
                            sum += m.MaterialQuantity * m.MaterialPrice;

                        }
                        materialRegister.Add(new MaterialRegister
                        {
                            Date = date,
                            Author = "Тронь Н.А.",
                            Storehouse = "Основной склад",
                            TypeMaterialReceipt = "Приход материалов",
                            TotalCost = sum
                        });
                    }

                }
            }

        }

        private void FillMaterialRegister()
        {
            materialRegister.Clear();
            DateTime date;

            foreach (var mat in dentalMaterialModel)
            {
                date = mat.PurchaseDate;
                if (materialRegister != null && materialRegister.Where(m => m.Date == date).Any())
                {
                    continue;
                }

                double sum = 0;
                foreach (var m in dentalMaterialModel.Where(v => v.PurchaseDate == date))
                {
                    sum += m.MaterialQuantity * m.MaterialPrice;

                }
                materialRegister.Add(new MaterialRegister
                {
                    Date = date,
                    Author = "Тронь Н.А.",
                    Storehouse = "Основной склад",
                    TypeMaterialReceipt = "Приход материалов",
                    TotalCost = sum
                });

            }

            // materialRegister= new ObservableCollection<MaterialRegister>(materialRegister.OrderBy(v => v.Date));

        }

        private void ResetCollection(string type)
        {
            if (type == "PatientViewModel")
            {
                patientsModel.Clear();
                foreach (PatientViewModel patient in patientService.GetAll())
                {
                    patientsModel.Add(patient);
                }
            }
            if (type == "AppointmentViewModel")
            {
                appointmentModel.Clear();
                foreach (AppointmentViewModel appointment in appointmentService.GetAll())
                {
                    appointmentModel.Add(appointment);
                }
            }
            if (type == "DoctorViewModel")
            {
                doctorsModel.Clear();
                foreach (DoctorViewModel doctor in doctorService.GetAll())
                {
                    doctorsModel.Add(doctor);
                }
            }
            if (type == "VisitViewModel")
            {
                visitsModel.Clear();
                foreach (VisitViewModel visit in visitService.GetAll())
                {
                    visitsModel.Add(visit);
                }
            }

            if (type == "DentalMaterialViewModel")
            {
                dentalMaterialModel.Clear();
                foreach (var item in dentalMaterialService.GetAll())
                {
                    dentalMaterialModel.Add(item);
                }
            }
            if (type == "PriceListViewModel")
            {
                priceListViewModel.Clear();
                foreach (var item in priceListService.GetAll())
                {
                    priceListViewModel.Add(item);
                }
            }
        }

        private void ResetCollectionWithFilters(string type)
        {
            
            if (type == "AppointmentViewModel")
            {
                appointmentModel.Clear();
                if (cBoxDoctor.SelectedItem != null)
                {
                    //Console.WriteLine(cBoxDoctor.SelectedValue + "");                  
                    foreach (AppointmentViewModel appointment in appointmentService.GetAll())
                    {
                        if (appointment.DoctorId == (int)cBoxDoctor.SelectedValue)
                        {
                            if (DatePickerFrom.SelectedDate != null && DatePickerTo.SelectedDate != null)
                            {
                                if (appointment.Date >= DatePickerFrom.SelectedDate && appointment.Date <= DatePickerTo.SelectedDate)
                                {
                                    appointmentModel.Add(appointment);
                                }
                            }
                            else appointmentModel.Add(appointment);
                        }
                    }
                }
                else if (DatePickerFrom.SelectedDate != null && DatePickerTo.SelectedDate != null)
                {
                    foreach (AppointmentViewModel appointment in appointmentService.GetAll())
                    {
                        if (appointment.Date >= DatePickerFrom.SelectedDate && appointment.Date <= DatePickerTo.SelectedDate)
                        {
                            appointmentModel.Add(appointment);
                        }
                    }
                }
                else ResetCollection("AppointmentViewModel");
            }
           
        }

        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            PatientViewModel patientModel = new PatientViewModel();
            patientModel.DateOfBirth = DateTime.Today;
            EditPatient dw = new EditPatient(patientModel);//создаем окно
            dw.Title = "Добавить Пациента";
            dw.Owner = this;//устанавливаем собственника окна
            var result = dw.ShowDialog();

            if (result == true)
            {
                patientService.CreatePatient(patientModel);
                ResetCollection("PatientViewModel");
                int Index = patientsModel.Count - 1;
                dGridPatients.SelectedIndex = Index;
                dw.Close();
            }
        }

        private void btnEditPatient_Click(object sender, RoutedEventArgs e)
        {
            PatientViewModel patientModel = dGridPatients.SelectedItem as PatientViewModel;
            if (patientModel != null)
            {
                int Index = patientsModel.IndexOf(patientModel);
                //Console.WriteLine("" + patientModel.PatientId + patientModel.DateOfBirth +
                //       patientModel.Address + patientModel.Telephone + patientModel.Sex );
                //Console.WriteLine("" + patientModel.Appointments.Count);
                var dw = new EditPatient(patientModel);
                dw.Title = "Изменить Пациента";
                dw.Owner = this;//устанавливаем собственника окна
                var result = dw.ShowDialog();
                if (result == true)
                {
                    //Console.WriteLine(""+patientModel.PatientId + patientModel.DateOfBirth + 
                    //    patientModel.Address + patientModel.Telephone + patientModel.Sex );
                    //Console.WriteLine("" + patientModel.Appointments.Count);


                    patientService.UpdatePatient(patientModel);                  
                    dGridPatients.SelectedIndex = Index;
                    dw.Close();
                }
                ResetCollection("PatientViewModel");
            }
            else
            {
                MessageBox.Show("Не выделена строка!");
            }
        }

        private void btnDeletePatient_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Удалить запись", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                var patientModel = (PatientViewModel)dGridPatients.SelectedItem;
                if (patientModel != null)
                {
                    patientService.DeletePatient(patientModel.PatientId);
                    ResetCollection("PatientViewModel");
                    dGridPatients.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Не выделена строка!");
                }
            }
        }

        private void btnSearchPatient_Click(object sender, RoutedEventArgs e)
        {
            string filter = tbSearchField.Text;
            ICollectionView viewSource = CollectionViewSource.GetDefaultView(dGridPatients.ItemsSource);
            if (filter == "") viewSource.Filter = null;
            else
            {
                viewSource.Filter = o =>
                {
                    PatientViewModel p = o as PatientViewModel;
                    return p.Name.ToString().Contains(filter);
                };
                dGridPatients.ItemsSource = viewSource;
            }
        }

        private void btnResetPatientFilter_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView viewSource = CollectionViewSource.GetDefaultView(dGridPatients.ItemsSource);
            viewSource.Filter = null;
            dGridPatients.ItemsSource = viewSource;          
            tbSearchField.Text = "Поиск...";
        }
        
        private void AddAppoinment_Click(object sender, RoutedEventArgs e)
        {
            AppointmentViewModel appointmentModel = new AppointmentViewModel();
            appointmentModel.Date = DateTime.Today;
            appointmentModel.Time = new TimeSpan(10, 00, 00);
            EditAppointment dw = new EditAppointment(appointmentModel, patientsModel, doctorsModel);//создаем окно
            dw.Title = "Добавить запись";
            dw.Owner = this;//устанавливаем собственника окна
            var result = dw.ShowDialog();

            if (result == true)
            {
                appointmentService.CreateAppointment(appointmentModel);
                ResetCollectionWithFilters("AppointmentViewModel");
                int Index = patientsModel.Count - 1;
                dGridPatients.SelectedIndex = Index;
                dw.Close();
            }
        }

        private void EditAppoinment_Click(object sender, RoutedEventArgs e)
        {
            AppointmentViewModel appointmentModel = dGridAppointment.SelectedItem as AppointmentViewModel;
            if (appointmentModel != null)
            {
                int Index = dGridAppointment.SelectedIndex;
                var dw = new EditAppointment(appointmentModel, patientsModel, doctorsModel);
                dw.Title = "Изменить Запись";
                dw.Owner = this;//устанавливаем собственника окна
                var result = dw.ShowDialog();
                if (result == true)
                {
                    appointmentService.UpdateAppointment(appointmentModel);
                    //int index = patients.IndexOf(patient);
                    // patients[index] = patientService.Get(patient.Id);

                    dGridPatients.SelectedIndex = Index;
                    dw.Close();
                }
                ResetCollectionWithFilters("AppointmentViewModel");
            }
            else
            {
                MessageBox.Show("Не выделена строка!");
            }
        }

        private void DeleteAppoinment_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Удалить запись", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                var appointmentModel = (AppointmentViewModel)dGridAppointment.SelectedItem;
                if (appointmentModel != null)
                {
                    appointmentService.DeleteAppointment(appointmentModel.AppointmentId);
                    ResetCollectionWithFilters("AppointmentViewModel");//нужно ли reset делать для др viewmodel
                    dGridAppointment.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Не выделена строка!");
                }
            }
        }

        private void cBoxDoctor_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (cBoxDoctor.SelectedItem != null)
            {
                //Console.WriteLine(cBoxDoctor.SelectedValue + "");
                appointmentModel.Clear();
                foreach (AppointmentViewModel appointment in appointmentService.GetAll())
                {

                    if (appointment.DoctorId == (int)cBoxDoctor.SelectedValue)
                    {
                        if (DatePickerFrom.SelectedDate != null && DatePickerTo.SelectedDate != null)
                        {
                            if (appointment.Date >= DatePickerFrom.SelectedDate && appointment.Date <= DatePickerTo.SelectedDate)
                            {
                                appointmentModel.Add(appointment);
                            }
                        }
                        else appointmentModel.Add(appointment);
                    }
                }
            }
        }

        private void AddPeriod_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("" + DatePickerFrom.SelectedDate + " " + DatePickerTo.SelectedDate);
            if (DatePickerFrom.SelectedDate != null && DatePickerTo.SelectedDate != null)
            {
                appointmentModel.Clear();
                foreach (AppointmentViewModel appointment in appointmentService.GetAll())
                {
                    if (appointment.Date >= DatePickerFrom.SelectedDate && appointment.Date <= DatePickerTo.SelectedDate)
                    {
                        if (cBoxDoctor.SelectedItem != null)
                        {
                            if (appointment.DoctorId == (int)cBoxDoctor.SelectedValue)
                            {
                                appointmentModel.Add(appointment);
                            }
                        }
                        else appointmentModel.Add(appointment);
                    }
                }
            }
        }

        private void ResetFilterAppoinment_Click(object sender, RoutedEventArgs e)
        {
            DatePickerFrom.SelectedDate = null;
            DatePickerTo.SelectedDate = null;
            cBoxDoctor.SelectedItem = null;
            ResetCollection("AppointmentViewModel");
        }

        private void btnAddVisit_Click(object sender, RoutedEventArgs e)
        {
            VisitViewModel visitModel = new VisitViewModel();
            visitModel.VisitDate = DateTime.Today;
            visitModel.VisitNumber = "б/н";

            Consultation dw = new Consultation(visitModel, patientsModel, doctorsModel, priceListViewModel, dentalMaterialModel);//создаем окно

            dw.Title = "Добавить Прием";
            dw.Owner = this;//устанавливаем собственника окна
            var result = dw.ShowDialog();

            if (result == true)
            {

                try
                {
                    // спишем материалы если есть
                    foreach (var t in visitModel.Treatments)
                    {
                        if (t.MaterialConsumptions != null)
                        {
                            foreach (var m in t.MaterialConsumptions)
                            {
                                var material = dentalMaterialService.Get(m.DentalMaterialId);

                                if (material.MaterialQuantity >= m.ConsumptionQuant)
                                {
                                    material.MaterialQuantity -= m.ConsumptionQuant;
                                }

                                dentalMaterialService.UpdateMaterial(material);

                            }
                        }
                    }
                    visitService.CreateVisit(visitModel);
                }
                catch (Exception ex) { MessageBox.Show("Ошибка" + ex.Message); }

                ResetCollection("VisitViewModel");
                ResetCollection("DentalMaterialViewModel");
                int Index = visitsModel.Count - 1;
                dGridVisit.SelectedIndex = Index;

                dw.Close();
            }
        }

        private void btnEditVisit_Click(object sender, RoutedEventArgs e)
        {
            VisitViewModel visitModel = dGridVisit.SelectedItem as VisitViewModel;

            if (visitModel != null)
            {

                List<TreatmentViewModel> treatmentsOld = new List<TreatmentViewModel>(visitModel.Treatments);//или Observable collection??? 
                List<MaterialConsumptionViewModel> materialsConsumptionOld = new List<MaterialConsumptionViewModel>();               

                int Index = dGridVisit.SelectedIndex;
                var dw = new Consultation(visitModel, patientsModel, doctorsModel, priceListViewModel, dentalMaterialModel);
                dw.Title = "Изменить Прием";
                dw.Owner = this;//устанавливаем собственника окна

                var result = dw.ShowDialog();

                if (result == true)
                {
                    //удалим старое
                    foreach (var m in materialsConsumptionOld)
                    {
                        materialConsumptionService.DeleteConsumption(m.DentalMaterialId);

                        var material = dentalMaterialService.Get(m.DentalMaterialId);
                        material.MaterialQuantity += m.ConsumptionQuant;
                        dentalMaterialService.UpdateMaterial(material);
                    }
                    foreach (var t in treatmentsOld)
                    {
                        treatmentService.DeleteTreatment(t.TreatmentId);
                    }
                 
                    //добавим новое
                    foreach (var t in visitModel.Treatments)
                    {
                        //списание материалов со склада
                        if (t.MaterialConsumptions != null)
                        {
                            foreach (var m in t.MaterialConsumptions)
                            {
                                var material = dentalMaterialService.Get(m.DentalMaterialId);

                                if (material.MaterialQuantity >= m.ConsumptionQuant)
                                {
                                    material.MaterialQuantity -= m.ConsumptionQuant;
                                }

                                dentalMaterialService.UpdateMaterial(material);
                            }
                        }
                        treatmentService.CreateTreatment(t);

                    }
                    
                    try
                    {
                        visitService.UpdateVisit(visitModel);
                    }
                    catch (Exception ex) { MessageBox.Show("Ошибка" + ex.Message); }

                    ResetCollection("DentalMaterialViewModel");

                    dGridVisit.SelectedIndex = Index;

                    dw.Close();
                }
                ResetCollection("VisitViewModel");
            }
            else
                MessageBox.Show("Не выбрана запись!");
        }

        private void btSaveVisit_Click(object sender, RoutedEventArgs e)
        {
            VisitViewModel visitModel = dGridVisit.SelectedItem as VisitViewModel;

            if (visitModel != null)
            {
                List<TreatmentViewModel> treatmentsOld = new List<TreatmentViewModel>(visitModel.Treatments);
                const string template = "Template_Invoice.xlsx";
                //описать сохранение данных приема в excel
                Microsoft.Office.Interop.Excel.Application app = null;
                Microsoft.Office.Interop.Excel.Workbook wb = null;
                Microsoft.Office.Interop.Excel.Worksheet ws = null;

                SaveFileDialog openDlg = new SaveFileDialog();

                openDlg.Filter = "Excel (.xls)|*.xls |Excel (.xlsx)|*.xlsx |All files (*.*)|*.*";
                openDlg.FilterIndex = 1;
                openDlg.RestoreDirectory = true;


                if (openDlg.ShowDialog() == true)
                {
                    string path = openDlg.FileName;

                    app = new Microsoft.Office.Interop.Excel.Application();
                    // app.Visible = true;
                    app.DisplayAlerts = false;
                    //надо скопировать template в новый файл
                    wb = app.Workbooks.Open(System.IO.Path.Combine(Environment.CurrentDirectory, template));


                    ws = wb.ActiveSheet;
                    // Записываем данные
                    ws.Range["D2"].Value = visitModel.VisitNumber.ToString();
                    ws.Range["F2"].Value = visitModel.VisitDate.ToShortDateString();
                    ws.Range["C4"].Value = visitModel.Patient.Name.ToString();
                    ws.Range["C5"].Value = visitModel.Doctor.DoctorName.ToString();
                    ws.Range["C6"].Value = visitModel.Patient.Contract.ToString();
                    ws.Range["C7"].Value = visitModel.Diagnose.ToString();

                    double sumTreatmentTotal = 0;
                    double sumMatTotal = 0;
                    
                    for (int i = 0; i < visitModel.Treatments.Count; i++)
                    {
                        ws.Cells[i + 12, 2].Value = visitModel.Treatments[i].PriceList.ServiceCode;
                        ws.Cells[i + 12, 3].Value = visitModel.Treatments[i].PriceList.ServiceName;
                        ws.Cells[i + 12, 4].Value = "обследование";
                        ws.Cells[i + 12, 5].Value = visitModel.Treatments[i].Quantity;
                        ws.Cells[i + 12, 6].Value = visitModel.Treatments[i].PriceList.Price;
                        double sumTreatment = visitModel.Treatments[i].Quantity * visitModel.Treatments[i].PriceList.Price;
                        sumTreatmentTotal += sumTreatment;
                        ws.Cells[i + 12, 7].Value = sumTreatment;
                        if (visitModel.Treatments[i].MaterialConsumptions.Count != 0)
                        {
                            double sumMat = 0;
                            foreach (var m in visitModel.Treatments[i].MaterialConsumptions)
                                sumMat += m.ConsumptionQuant * m.SellingPrice;
                            ws.Cells[i + 12, 8].Value = sumMat;
                            sumMatTotal += sumMat;
                            ws.Cells[i + 12, 9].Value = sumTreatment + sumMat;
                            
                        }
                        else
                        {
                            ws.Cells[i + 12, 8].Value = 0;
                            ws.Cells[i + 12, 9].Value = sumTreatment;
                        }
                    }

                   
                    int number1 = visitModel.Treatments.Count + 12;
                    ws.Cells[number1, 3].Value = "ИТОГО:";                    
                    ws.Cells[number1, 7].Value = sumTreatmentTotal;
                    ws.Cells[number1, 8].Value = sumMatTotal;
                    ws.Cells[number1, 9].Value = sumMatTotal + sumTreatmentTotal;

                    ws.Cells[number1 + 2, 3].Value = "________________________________"+ visitModel.Doctor.DoctorName;
                    Microsoft.Office.Interop.Excel.Range myRange = ws.Range["B12", "I" + number1];
                    myRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                   

                    wb.SaveCopyAs(path + ".xlsx");
                    wb.Close();
                    System.Diagnostics.Process.Start(path + ".xlsx");

                }
            }
            else
                MessageBox.Show("Не выбрана запись!");
        }

        private void btnDeleteVisit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Удалить запись", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                var visitModel = (VisitViewModel)dGridVisit.SelectedItem;
                if (visitModel != null)
                {
                    visitService.DeleteVisit(visitModel.VisitId);

                    //восстановить материалы на складе

                    foreach (var t in visitModel.Treatments)
                    {
                        if (t.MaterialConsumptions != null)
                        {
                            foreach (var m in t.MaterialConsumptions)
                            {
                                var material = dentalMaterialService.Get(m.DentalMaterialId);

                                material.MaterialQuantity += m.ConsumptionQuant;

                                dentalMaterialService.UpdateMaterial(material);
                            }
                        }
                    }
                }
                else { MessageBox.Show("Не выделена строка!"); }

                ResetCollection("VisitViewModel");
                ResetCollection("DentalMaterialViewModel");
                dGridVisit.SelectedIndex = 0;
            }
        }

        private void AddPeriodVisit_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerFromVisit.SelectedDate != null && DatePickerToVisit.SelectedDate != null)
            {
                visitsModel.Clear();
                foreach (VisitViewModel visit in visitService.GetAll())
                {
                    if (visit.VisitDate >= DatePickerFromVisit.SelectedDate && visit.VisitDate <= DatePickerToVisit.SelectedDate)
                    {
                        visitsModel.Add(visit);
                    }
                }
            }
        }

        private void ResetFilterVisit_Click(object sender, RoutedEventArgs e)
        {
            DatePickerFromVisit.SelectedDate = null;
            DatePickerToVisit.SelectedDate = null;
            ResetCollection("VisitViewModel");
        }

        private void btnAddLinePrice_Click(object sender, RoutedEventArgs e)
        {
            PriceListViewModel priceMedService = new PriceListViewModel();

            EditPrice dw = new EditPrice(priceMedService, priceListViewModel, dentalMaterialModel);//создаем окно

            dw.Title = "Добавить Услугу в Прайслист";
            dw.Owner = this;//устанавливаем собственника окна
            var result = dw.ShowDialog();

            if (result == true)
            {
                priceListService.CreatePriceList(priceMedService);

                ResetCollection("PriceListViewModel");
                FillPriceListView();
                int Index = priceListView.Count - 1;
                dGridPrice.SelectedIndex = Index;

                dw.Close();
            }
        }

        private void btnDeleteLinePrice_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Удалить запись", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                var pricelistView = (PriceListView)dGridPrice.SelectedItem;
                if (pricelistView != null)
                {

                    priceListService.DeletePriceList(pricelistView.PriceList.PriceListId);
                    ResetCollection("PriceListViewModel");
                    FillPriceListView();
                    dGridVisit.SelectedIndex = 0;
                }
                else { MessageBox.Show("Не выделена строка!"); }
            }
        }

         private void btnSavePrice_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application app = null;
            Microsoft.Office.Interop.Excel.Workbook wb = null;
            Microsoft.Office.Interop.Excel.Worksheet ws = null;
            //var process = Process.GetProcessesByName("EXCEL");

            SaveFileDialog openDlg = new SaveFileDialog();
            //openDlg.FileName = "Прейскурант №";
            openDlg.Filter = "Excel (.xls)|*.xls |Excel (.xlsx)|*.xlsx |All files (*.*)|*.*";
            openDlg.FilterIndex = 1;
            openDlg.RestoreDirectory = true;


            if (openDlg.ShowDialog() == true)
            {
                string path = openDlg.FileName;

                app = new Microsoft.Office.Interop.Excel.Application();
                // app.Visible = true;
                app.DisplayAlerts = false;
                wb = app.Workbooks.Add();
                ws = wb.ActiveSheet;
                dGridPrice.SelectAllCells();
                dGridPrice.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, dGridPrice);

                ws.Paste();               
                ws.Range["A1", "F1"].Font.Bold = true;
                int number1 = ws.UsedRange.Rows.Count;
                Microsoft.Office.Interop.Excel.Range myRange = ws.Range["A1", "F" + number1];
                myRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                myRange.WrapText = false;
                ws.Columns.EntireColumn.AutoFit();
                dGridPrice.UnselectAllCells();
                wb.SaveCopyAs(path+".xlsx");
                wb.Close();
                System.Diagnostics.Process.Start(path + ".xlsx");
                
            }
        }

        private void btnSearchVisit_Click(object sender, RoutedEventArgs e)
        {
            string filter = tbSearchVisitField.Text;
            ICollectionView viewSource = CollectionViewSource.GetDefaultView(dGridVisit.ItemsSource);
            if (filter == "") viewSource.Filter = null;
            else
            {
                viewSource.Filter = o =>
                {
                    VisitViewModel p = o as VisitViewModel;
                    return p.Patient.Name.ToString().Contains(filter);
                };
                dGridVisit.ItemsSource = viewSource;
            }
        }

        private void btnAddMaterial_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DentalMaterialViewModel> dentalMaterials = new ObservableCollection<DentalMaterialViewModel>();

            AddMaterial dw = new AddMaterial(dentalMaterials);//создаем окно
            dw.Title = "Добавить поступление материалов";
            dw.Owner = this;//устанавливаем собственника окна
            var result = dw.ShowDialog();

            if (result == true)
            {
                foreach (var material in dentalMaterials)
                    dentalMaterialService.CreateMaterial(material);

                //добавить запись в Register
                ResetCollection("DentalMaterialViewModel");
                FillMaterialRegister();

                int Index = materialRegister.Count - 1;
                dGridMaterial.SelectedIndex = Index;
                dw.Close();
            }
        }

        private ObservableCollection<DentalMaterialViewModel> FindMaterials(MaterialRegister register)
        {
            ObservableCollection<DentalMaterialViewModel> dentalMaterials = new ObservableCollection<DentalMaterialViewModel>();
            ResetCollection("DentalMaterialViewModel");
            foreach (var material in dentalMaterialModel)
            {
                if (material.PurchaseDate == register.Date)
                {
                    material.CalculateCost();
                    dentalMaterials.Add(material);
                }
            }
            return dentalMaterials;
        }

        private void btnEditMaterial_Click(object sender, RoutedEventArgs e)
        {
            MaterialRegister register = dGridMaterial.SelectedItem as MaterialRegister;
            if (register != null)
            {
                int Index = dGridMaterial.SelectedIndex;
                ObservableCollection<DentalMaterialViewModel> dentalMaterials = FindMaterials(register);
                List<DentalMaterialViewModel> dentalMaterialsOld = new List<DentalMaterialViewModel>(dentalMaterials);
                AddMaterial dw = new AddMaterial(dentalMaterials);//создаем окно
                dw.Title = "Изменить поступление материалов";
                dw.Owner = this;
                var result = dw.ShowDialog();

                if (result == true)
                {
                    foreach (var materialOld in dentalMaterialsOld)
                    {
                        bool flag = true;
                        foreach (var material in dentalMaterials)
                        {
                            if (materialOld.DentalMaterialId == material.DentalMaterialId) // если изменили
                            {
                                dentalMaterialService.UpdateMaterial(material);
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            dentalMaterialService.DeleteMaterial(materialOld.DentalMaterialId);// если удалили
                        }
                    }
                    foreach (var material in dentalMaterials)
                    {
                        if (material.DentalMaterialId == 0)
                        {
                            dentalMaterialService.CreateMaterial(material);//если добавили новый
                        }
                    }
                    ResetCollection("DentalMaterialViewModel");
                    FillMaterialRegister();
                    dGridMaterial.SelectedIndex = Index;
                }
            }
            else { MessageBox.Show("Не выделена строка!"); }
        }

        private void btnDeleteMaterial_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Удалить запись", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                var register = (MaterialRegister)dGridMaterial.SelectedItem;
                if (register != null)
                {
                    foreach (var material in dentalMaterialModel)
                        if (material.PurchaseDate == register.Date)
                            dentalMaterialService.DeleteMaterial(material.DentalMaterialId);

                    ResetCollection("DentalMaterialViewModel");
                    FillMaterialRegister();
                    //dGridMaterial.DataContext = materialRegister;
                    dGridMaterial.SelectedIndex = 0;
                }
                else { MessageBox.Show("Не выделена строка!"); }

            }
        }

        private void btnSearchMaterial_Click(object sender, RoutedEventArgs e)
        {
            string filter = tbSearchMaterialField.Text;
            ICollectionView viewSource = CollectionViewSource.GetDefaultView(dGridMaterial.ItemsSource);
            if (filter == "") viewSource.Filter = null;
            else
            {
                viewSource.Filter = o =>
                {
                    MaterialRegister p = o as MaterialRegister;
                    return p.Author.ToString().Contains(filter);
                };
                dGridMaterial.ItemsSource = viewSource;
            }
        }

        private void AddPeriodMaterialClick(object sender, RoutedEventArgs e)
        {
            if (DatePickerFromMaterial.SelectedDate != null && DatePickerToMaterial.SelectedDate != null)
            {
                FillMaterialRegisterWithFilter(DatePickerFromMaterial.SelectedDate.Value, DatePickerToMaterial.SelectedDate.Value);
            }
            else MessageBox.Show("Не выбраны даты");
        }

        private void ResetFilterMaterial_Click(object sender, RoutedEventArgs e)
        {
            DatePickerFromMaterial.SelectedDate = null;
            DatePickerToMaterial.SelectedDate = null;
            FillMaterialRegister();
        }

        private void AddPeriodReport_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerFromReport.SelectedDate != null && DatePickerToReport.SelectedDate != null)
            {
                FillReport(DatePickerFromReport.SelectedDate.Value, DatePickerToReport.SelectedDate.Value);
            }
            else MessageBox.Show("Не выбраны даты");
        }

        private void FillReport(DateTime DateFrom, DateTime DateTo)
        {
            if (DateFrom != null || DateTo != null)
            {
                reports.Clear();
                DoctorViewModel doctor = new DoctorViewModel();
                DateTime date;


                foreach (var visit in visitsModel)
                {
                    date = visit.VisitDate;
                    if (date >= DateFrom && date <= DateTo)
                    {
                        doctor = visit.Doctor;
                        if (reports.Count == 0)
                        {
                            reports.Add(new Report
                            {
                                Doctor = doctor,
                                Income = visit.TotalCost,
                                Expenses = visit.MaterialCost,
                                Consultations = 1
                            });
                        }
                        else
                        {
                            if (reports.Where(v => v.Doctor == doctor).Any())
                            {
                                var report = reports.Where(v => v.Doctor == doctor).FirstOrDefault();
                                report.Consultations += 1;
                                report.Expenses += visit.MaterialCost;
                                report.Income += visit.TotalCost;
                            }
                            else
                            {
                                reports.Add(new Report
                                {
                                    Doctor = doctor,
                                    Income = visit.TotalCost,
                                    Expenses = visit.MaterialCost,
                                    Consultations = 1
                                });

                            }
                        }
                    }
                }
            }
            int consultations = 0;
            double incomeTotal = 0;
            double expensesTotal = 0;

            foreach (var r in reports)
            {
                if (r.Expenses != 0)
                {
                    double margin = Math.Round(((r.Income - r.Expenses) / r.Expenses * 100), 2);
                    r.ProfitMargin = margin;
                }
                else
                    r.ProfitMargin = 100;

                consultations += r.Consultations;
                incomeTotal += r.Income;
                expensesTotal += r.Expenses;
            }
           
            dGridReport.DataContext = reports;
            tbTotalConsultations.Text = consultations + "";

            //format
            try
            {
                tbIncomePerConsultation.Text =  Math.Round(incomeTotal / consultations, 2).ToString("C", System.Globalization.CultureInfo.CreateSpecificCulture("be-BY"));
                tbExpenses.Text = expensesTotal.ToString("C", System.Globalization.CultureInfo.CreateSpecificCulture("be-BY"));
                tbIncome.Text = incomeTotal.ToString("C", System.Globalization.CultureInfo.CreateSpecificCulture("be-BY"));
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (expensesTotal != 0)
            {
                tbMargin.Text = Math.Round(((incomeTotal - expensesTotal) / expensesTotal * 100), 2) + "%";
            }

            LoadChartData();
        }

        private void LoadChartData()
        {
            ((PieSeries)mcChart.Series[0]).ItemsSource = reports;
            ((BarSeries)mbChart.Series[0]).ItemsSource = reports;
           
        }

        private void dGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        //private void btnChangeLinePrice_Click(object sender, RoutedEventArgs e)
        //{
            
        //}

        //private void btnPrintPrice_Click(object sender, RoutedEventArgs e)
        //{

        //}
        private void Window_Closing(object sender, CancelEventArgs e)
        {           
            AdditionalFunctions.LogMessageToFile("Выход. Пользователь: " + authorization.User + " c ролью " + authorization.Role);
        }
    }
}
