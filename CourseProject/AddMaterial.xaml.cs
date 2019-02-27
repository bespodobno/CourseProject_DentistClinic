using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CP.BusinessLayer.Models;
using CP.DataLayer.Entites;
using System.Collections.ObjectModel;

namespace CourseProject
{
    /// <summary>
    /// Interaction logic for AddMaterial.xaml
    /// </summary>
    public partial class AddMaterial : Window
    {
        ObservableCollection<DentalMaterialViewModel> materials = new ObservableCollection<DentalMaterialViewModel>();
       
        List<Unit> userTypes = Enum.GetValues(typeof(Unit)).Cast<Unit>().ToList();

        public AddMaterial()
        {
            InitializeComponent();
        }
       
        public AddMaterial(ObservableCollection<DentalMaterialViewModel> dentalMaterials) : this()
        {           
            this.materials = dentalMaterials;
            this.DataContext = materials;

            if (dentalMaterials.Count == 0)
            {
                dpMaterialReceiptsDate.SelectedDate = DateTime.Today;
            }
            else
            {
                dpMaterialReceiptsDate.SelectedDate = dentalMaterials[0].PurchaseDate;
            }

            cbMaterialType.ItemsSource = TypeMaterial.TypeMaterials;
            cbMaterialUnit.ItemsSource = userTypes;
            CalculateCostToTbCost(materials);
        }
        private void CalculateCostToTbCost(ObservableCollection<DentalMaterialViewModel> dentalMaterials)
        {
            double sum = 0;
            foreach (var material in dentalMaterials)
            {
                sum += material.MaterialCost;
            }
            tbCost.Text = sum.ToString();
        }
        private void btAddLine_Click(object sender, RoutedEventArgs e)
        {
            if (cbMaterialType.SelectedItem != null && cbMaterialUnit.SelectedItem != null)
            {
                string materialName = tbNameMaterial.Text.ToString();

                if (materialName.Length != 0)
                {

                    bool resultQuantity = AdditionalFunctions.IsDouble(tbQuantity.Text);
                    bool resultPrice = AdditionalFunctions.IsDouble(tbPrice.Text);
                    bool resDate = AdditionalFunctions.IsDateTime(dpMaterialReceiptsDate.SelectedDate.Value.ToShortDateString());
                    
                    if (!resultQuantity)
                    {
                        MessageBox.Show("Измените количество материала на число!");
                        tbQuantity.Text = "1";
                    }
                    else if (!resultPrice)
                    {
                        MessageBox.Show("Измените цену материала на число!");
                    }
                    else if(!resDate)
                    {
                        MessageBox.Show("Введите правильный формат даты документа!");
                    }
                    else
                    {
                        double price = double.Parse(tbPrice.Text);
                        double quantity = double.Parse(tbQuantity.Text);
                       
                        DentalMaterialViewModel materialNew = new DentalMaterialViewModel
                        {
                            MaterialName = materialName,                           
                            MaterialPrice = price,
                            MaterialQuantity = quantity,
                            MaterialCost = quantity * price,
                            TypeMaterial = (string)cbMaterialType.SelectedValue,
                            Unit = (Unit)cbMaterialUnit.SelectedValue,
                            PurchaseDate = (DateTime)dpMaterialReceiptsDate.SelectedDate
                    };
                        materials.Add(materialNew);

                        cbMaterialType.SelectedItem = null;
                        cbMaterialUnit.SelectedItem = null;
                        tbPrice.Text = "";
                        tbQuantity.Text = "1";
                        tbNameMaterial.Text = "";
                        CalculateCostToTbCost(materials);
                    }
                }
                else MessageBox.Show("Выберите название материала!");
            }
            else MessageBox.Show("Введите корректные данные.");
        }

        private void btDeleteLine_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Удалить строку", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                var material = dGridMaterialReceipts.SelectedItem as DentalMaterialViewModel;

                if (material != null)
                {
                    materials.Remove(material);
                    CalculateCostToTbCost(materials);
                }
                else MessageBox.Show("Не выделена строка!");

            }
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            if (materials.Count != 0)
            {
                if (dpMaterialReceiptsDate.SelectedDate != null)
                {
                    this.DialogResult = true;                  
                }
                else MessageBox.Show("Введите дату документа!");

            }
            else MessageBox.Show("Введите приход материалов!");
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

    }
}
