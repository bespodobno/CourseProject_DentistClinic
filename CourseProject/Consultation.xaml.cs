using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CP.BusinessLayer.Models;
using CP.DataLayer.Entites;
using System.Collections.ObjectModel;

namespace CourseProject
{
    /// <summary>
    /// Interaction logic for Consultation.xaml
    /// </summary>
    public partial class Consultation : Window
    {
        VisitViewModel visitModel;
        ObservableCollection<TreatmentViewModel> treatmentModel = new ObservableCollection<TreatmentViewModel>();
        ObservableCollection<DentalMaterialViewModel> materialsExisting = new ObservableCollection<DentalMaterialViewModel>();
        List<string> alerts = new List<string>();
        public Consultation()
        {
            InitializeComponent();
        }
        public Consultation(VisitViewModel visitModel,
                              ObservableCollection<PatientViewModel> patientsModel,
                              ObservableCollection<DoctorViewModel> doctorsModel,
                              ObservableCollection<PriceListViewModel> priceListViewModel,
                              ObservableCollection<DentalMaterialViewModel> dentalMaterialModel
                             ) : this() // 
        {
            this.visitModel = visitModel;

            cbPatientName.ItemsSource = patientsModel;
            cbPricelist.ItemsSource = priceListViewModel;
            this.DataContext = visitModel;
            cbDoctor.ItemsSource = doctorsModel;
            // cbMateriallist.ItemsSource = dentalMaterialModel;
            materialsExisting = dentalMaterialModel;

            if (visitModel.Treatments == null)      //was  --- visitModel.Treatments==null
            {
                visitModel.Treatments = treatmentModel;
            }
            else
            {
                treatmentModel = visitModel.Treatments; //заполнить список имеющимися записями
            }
            dGridConsultation.DataContext = visitModel.Treatments;
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            var doctor = cbDoctor.SelectedItem as DoctorViewModel;
            if (doctor != null)
            {
                var patient = cbPatientName.SelectedItem as PatientViewModel;
                if (patient != null)
                {
                    visitModel.VisitNumber = tbNumberConsultancy.Text;
                    visitModel.Diagnose = tbDiagnose.Text;
                    visitModel.DoctorId = doctor.DoctorId;
                    visitModel.PatientId = patient.PatientId;

                    if (dpVisitDate.SelectedDate != null)
                    {
                        var result = AdditionalFunctions.IsDateTime(dpVisitDate.SelectedDate.Value.ToShortDateString());
                        if (result)
                        {
                            visitModel.VisitDate = (DateTime)dpVisitDate.SelectedDate;
                            visitModel.CalculateCost();

                            //foreach (var t in visitModel.Treatments)
                            //    if (t.MaterialConsumptions != null)
                            //    {
                            //        foreach (var m in t.MaterialConsumptions)
                            //            Console.WriteLine(m.DentalMaterialId + " " + m.ConsumptionQuant);
                            //    }

                            this.DialogResult = true;
                        }
                        else MessageBox.Show("Введите правильный формат даты!");
                    }
                }
                else MessageBox.Show("Выберите пациента!");
            }
            else MessageBox.Show("Выберите доктора!");
        }

        private void btAddLine_Click(object sender, RoutedEventArgs e)
        {
            //проверить есть ли расход материлов по другим 

            if (cbMateriallist.SelectedItem != null || cbMateriallist.ItemsSource == null)
            {
                if (cbPricelist.SelectedItem != null)
                {
                    uint uintNumber;
                    uint quantity;
                    var result = uint.TryParse(tbQuantity.Text, out uintNumber);

                    if (!result)
                    {
                        MessageBox.Show("Измените количество услуги на целое число!");
                        tbQuantity.Text = "1";
                    }
                    else
                    {
                        quantity = uint.Parse(tbQuantity.Text);

                        //add treatment service
                        var pricelist = (PriceListViewModel)cbPricelist.SelectedItem;
                        TreatmentViewModel treatment = new TreatmentViewModel
                        {
                            Quantity = (double)quantity,
                            PriceListId = (int)cbPricelist.SelectedValue,
                            PriceList = pricelist
                        };

                        //add material consumption
                        ObservableCollection<MaterialConsumptionViewModel> materialConsumptions = new ObservableCollection<MaterialConsumptionViewModel>();
                        AddMaterialConsumption(materialConsumptions, pricelist, treatment);

                        if (visitModel.VisitId != 0) treatment.VisitId = visitModel.VisitId;
                        treatmentModel.Add(treatment);
                        visitModel.Treatments = treatmentModel;

                        int Index = treatmentModel.Count - 1;
                        dGridConsultation.SelectedIndex = Index;
                        cbPricelist.SelectedItem = null;
                        cbMateriallist.SelectedItem = null;
                        tbQuantity.Text = "1";
                    }
                }
            }
            else MessageBox.Show("Выберите материал");
        }

        private void AddMaterialConsumption(ObservableCollection<MaterialConsumptionViewModel> materialConsumptions, PriceListViewModel pricelist, TreatmentViewModel treatment)
        {
            bool flag = false;

            if (pricelist.NormConsumptionRates.Count > 0)
            {
                ObservableCollection<DentalMaterialViewModel> materialsExistingUpdate = materialsExisting;
                foreach (var norm in pricelist.NormConsumptionRates)
                {
                    var normConsumption = norm.Norm * treatment.Quantity;//всего расход материала на услугу                                                                       


                    if (norm.AutoSelection == true)
                    {
                        //var dentalMaterial = cbMateriallist.SelectedItem as DentalMaterialViewModel;
                        foreach (var m in materialsExistingUpdate)
                        {
                            if (m.TypeMaterial == norm.TypeMaterial && m.MaterialQuantity >= normConsumption)
                            {
                                materialConsumptions.Add(new MaterialConsumptionViewModel
                                {
                                    DentalMaterialId = m.DentalMaterialId,
                                    ConsumptionQuant = normConsumption,/*умножим на количество услуг*/
                                    SellingPrice = norm.PriceMaterial,

                                });
                                //Console.WriteLine(m.MaterialName + " " + normConsumption);
                                m.MaterialQuantity -= normConsumption;
                                normConsumption = 0;
                            }
                        }

                    }
                    else
                    {
                        if (!flag)
                        {
                            flag = true;
                            DentalMaterialViewModel materialAutoSelected = cbMateriallist.SelectedItem as DentalMaterialViewModel;
                            if (materialAutoSelected != null)
                            {

                                if (materialAutoSelected.MaterialQuantity >= normConsumption)
                                {

                                    materialConsumptions.Add(

                                                       new MaterialConsumptionViewModel
                                                       {
                                                           DentalMaterialId = materialAutoSelected.DentalMaterialId,
                                                           ConsumptionQuant = normConsumption,
                                                           SellingPrice = norm.PriceMaterial
                                                       }
                                                       );
                                    //Console.WriteLine(materialAutoSelected.MaterialName + " " + normConsumption);
                                    foreach (var m in materialsExistingUpdate)
                                    {
                                        if (m.DentalMaterialId == materialAutoSelected.DentalMaterialId)
                                        {

                                            m.MaterialQuantity -= normConsumption;//обновить для материалов
                                            break;
                                        }
                                    }

                                    normConsumption = 0;

                                }

                            }
                        }
                        else normConsumption = 0;
                    }
                    if (normConsumption > 0)
                        alerts.Add( norm.TypeMaterial + "  в количестве"+ normConsumption );
                }


                if (alerts.Count > 0)
                {
                    string message = null;
                    foreach (var el in alerts)
                    {
                        message += el.ToString() + "\n";
                    }
                    MessageBox.Show("Не хватает материалов\n" + message);
                    alerts = null;
                }
                else
                {
                    treatment.MaterialConsumptions = materialConsumptions;
                    materialsExisting = materialsExistingUpdate;

                }
            }
        }

        private void btDeleteLine_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Удалить строку", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                var treatment = dGridConsultation.SelectedItem as TreatmentViewModel;
                if (treatment != null)
                {
                    treatmentModel.Remove(treatment);


                    visitModel.Treatments = treatmentModel;
                    if (treatment.MaterialConsumptions != null)
                    {
                        //восстановить материалы
                        foreach (var material in treatment.MaterialConsumptions)
                        {
                            // Console.WriteLine(material.DentalMaterial.MaterialName + " " + material.ConsumptionQuant);
                            foreach (var m in materialsExisting)
                            {
                                if (m.DentalMaterialId == material.DentalMaterialId)
                                {

                                    m.MaterialQuantity += material.ConsumptionQuant;//обновить для материалов
                                    //Console.WriteLine(m.MaterialName + " " + material.ConsumptionQuant);
                                    break;
                                }
                            }
                        }
                    }
                }
                else MessageBox.Show("Не выделена строка!");
            }
        }

        private void dGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }       

        private void cbPriceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cbPricelist.SelectedItem != null)
            {
                PriceListViewModel pricelist = (PriceListViewModel)cbPricelist.SelectedItem;
                bool changes = false;
                cbMateriallist.ItemsSource = null;
                //List<string> list = new List<string>();
                ObservableCollection<DentalMaterialViewModel> list = new ObservableCollection<DentalMaterialViewModel>();

                foreach (var n in pricelist.NormConsumptionRates)
                {
                    if (n.AutoSelection == false)
                    {
                        changes = true;

                        foreach (var m in materialsExisting)
                        {
                            if (n.TypeMaterial == m.TypeMaterial && m.MaterialQuantity > 0) list.Add(m);
                        }

                        break;//если нашел одно автозаполнение
                    }

                }

                if (!changes)
                {
                    cbMateriallist.SelectedItem = null;
                    cbMateriallist.IsEnabled = false;
                }
                else
                {
                    cbMateriallist.ItemsSource = list;
                    cbMateriallist.IsEnabled = true;
                }
            }
        }

    }
}
