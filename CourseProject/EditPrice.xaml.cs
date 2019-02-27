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
    /// Interaction logic for AddLineVisit.xaml
    /// </summary>
    public partial class EditPrice : Window
    {
        PriceListViewModel priceMedService;
        ObservableCollection<DentalMaterialViewModel> materials = new ObservableCollection<DentalMaterialViewModel>();
        ObservableCollection<NormConsumptionRateViewModel> norms = new ObservableCollection<NormConsumptionRateViewModel>();

        List<Unit> userTypes = Enum.GetValues(typeof(Unit)).Cast<Unit>().ToList();

        public EditPrice()
        {
            InitializeComponent();
        }
        public EditPrice(PriceListViewModel priceMedService, ObservableCollection<PriceListViewModel> priceListViewModel,
            ObservableCollection<DentalMaterialViewModel> dentalMaterials) : this()
        {
            this.priceMedService = priceMedService;
            this.DataContext = priceMedService;
            this.materials = dentalMaterials;

            if (priceMedService.NormConsumptionRates == null)
            {
                //cbType1.ItemsSource = TypeMaterial.TypeMaterials;
                foreach (ComboBox combobox in stackPanelMaterialType.Children)
                {
                    combobox.ItemsSource = TypeMaterial.TypeMaterials;
                }
                //cbMaterialType.ItemsSource = TypeMaterial.TypeMaterials;
                //cbMaterialUnit.ItemsSource = userTypes;
                //CalculateCostToTbCost(materials);
            }
            else { }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            //RadioButton pressed = (RadioButton)sender;
            //MessageBox.Show(pressed.Name.ToString());
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            bool resultPrice = AdditionalFunctions.IsDouble(tbServicePrice.Text);

            if (tbServiceCode.Text.Length != 0 && tbServiceName.Text.Length != 0 && resultPrice == true)
            {               
               
                //есть ли заполненные нормы материалов
                bool flagNoMaterial = true;
                bool flagMaterial = false;
               

                foreach (ComboBox combo in stackPanelMaterialType.Children)
                {                  
                    if (combo.SelectedItem != null)//если выбран материал
                    {
                        flagNoMaterial = false;
                        //проверки
                        int index = stackPanelMaterialType.Children.IndexOf(combo);
                        ComboBox itemMaterial = (ComboBox)stackPanelMaterialName.Children[index];
                        if (itemMaterial.SelectedItem != null)
                        {
                            DentalMaterialViewModel material = itemMaterial.SelectedItem as DentalMaterialViewModel;

                            TextBox textBoxPrice = (TextBox)stackPaneltbPrice.Children[index];
                            TextBox textBoxNorm = (TextBox)stackPaneltbNorm.Children[index];

                            bool resultPriceNorm = AdditionalFunctions.IsDouble(textBoxPrice.Text);
                            bool resultNorm = AdditionalFunctions.IsDouble(textBoxNorm.Text);

                            if (!resultPriceNorm)
                            {
                                MessageBox.Show("Введите корректное значение в поле Цена для " + combo.SelectedItem);
                                break;
                            }
                            if (!resultNorm)
                            {
                                MessageBox.Show("Введите корректное значение в поле Норма расхода для " + combo.SelectedItem);
                                break;
                            }
                            RadioButton radio = (RadioButton)stackPanelRadio.Children[index];
                            bool flag = radio.IsChecked.Value ? false : true;
                            double normConsumption = double.Parse(textBoxNorm.Text);
                            double priceMaterial = double.Parse(textBoxPrice.Text);

                            NormConsumptionRateViewModel norm = new NormConsumptionRateViewModel();

                            norm.AutoSelection = flag;
                            norm.NameMaterial = material.MaterialName;
                            norm.Norm = normConsumption;
                            norm.PriceMaterial = priceMaterial;
                            norm.TypeMaterial = material.TypeMaterial;
                            //тут свалилось 
                            norms.Add(norm);
                           
                            flagMaterial = true;
                        }
                        else
                        {
                            MessageBox.Show("Заполните Наименование материала для " + combo.SelectedItem);
                            break;
                        }

                    }

                }

                if (flagNoMaterial || flagMaterial)
                {
                    priceMedService.NormConsumptionRates = norms;
                    this.DialogResult = true;
                }
            }
            else
                MessageBox.Show("Введите корректные данные в поле Код, Наименование услуги и Цена услуги");
            
        }

        private void cbTypeMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox item = (ComboBox)sender;
            if (item.SelectedItem != null)
            {
                string typeMaterial = (string)item.SelectedItem;
                List<DentalMaterialViewModel> listMaterial = new List<DentalMaterialViewModel>();
                foreach (var material in materials)
                {
                    if (material.TypeMaterial == typeMaterial && material.MaterialQuantity > 0) listMaterial.Add(material);
                }

                int index = stackPanelMaterialType.Children.IndexOf((ComboBox)sender);
                ComboBox itemMaterial = (ComboBox)stackPanelMaterialName.Children[index];
                itemMaterial.ItemsSource = listMaterial;
                itemMaterial.IsEnabled = true;
            }
        }

        private void cbMaterialName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox item = (ComboBox)sender;
            if (item.SelectedItem != null)
            {
                int index = stackPanelMaterialName.Children.IndexOf((ComboBox)sender);
                TextBox textBoxPriceMaterial = (TextBox)stackPaneltbPrice.Children[index];
                var material = (DentalMaterialViewModel)item.SelectedItem;
                textBoxPriceMaterial.Text = "" + material.MaterialPrice * 1.3;
            }
        }

        private void btRemoveField_Click(object sender, RoutedEventArgs e)
        {
            int index = stackPanelButton.Children.IndexOf((Button)sender);
            ComboBox itemMaterial = (ComboBox)stackPanelMaterialName.Children[index];
            itemMaterial.SelectedItem = null;
            itemMaterial.IsEnabled = false;            
            ComboBox itemType = (ComboBox)stackPanelMaterialType.Children[index];
            itemType.SelectedItem = null;
            TextBox textBoxPrice = (TextBox)stackPaneltbPrice.Children[index];
            textBoxPrice.Text = "1";
            TextBox textBoxNorm = (TextBox)stackPaneltbNorm.Children[index];
            textBoxNorm.Text = "1";
        }
    }
}
