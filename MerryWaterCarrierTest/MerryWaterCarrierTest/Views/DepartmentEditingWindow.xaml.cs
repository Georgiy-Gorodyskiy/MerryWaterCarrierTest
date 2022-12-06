using MerryWaterCarrierTest.Models;
using MerryWaterCarrierTest.Services;
using System;
using System.Windows;

namespace MerryWaterCarrierTest.Views
{
    /// <summary>
    /// Логика взаимодействия для DepartmentEditingWindow.xaml
    /// </summary>
    public partial class DepartmentEditingWindow : Window
    {
        private Department? curentDepartment;
        private WaterCarrierService service => App.GetService<WaterCarrierService>();

        public DepartmentEditingWindow(bool isEdit = false)
        {
            try
            {
                InitializeComponent();
                curentDepartment = isEdit ? service.CurentDepartament : null;
                LeaderCmb.ItemsSource = service.Employees;
                LeaderCmb.DisplayMemberPath = "DisplayName";
                if (curentDepartment != null)
                {
                    Load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Load()
        {
            NameTxt.Text = curentDepartment?.Name;
            LeaderCmb.SelectedItem = curentDepartment?.Leader;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (curentDepartment == null)
                    curentDepartment = new Department();
                curentDepartment.Name = NameTxt.Text;
                curentDepartment.Leader = (Employee)LeaderCmb.SelectedItem;
                service.EditDepartament(curentDepartment);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
