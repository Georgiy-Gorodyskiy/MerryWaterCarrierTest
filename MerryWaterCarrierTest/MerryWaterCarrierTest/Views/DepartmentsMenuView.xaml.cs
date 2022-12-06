using MerryWaterCarrierTest.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MerryWaterCarrierTest.Views
{
    /// <summary>
    /// Логика взаимодействия для DepartmentsMenuView.xaml
    /// </summary>
    public partial class DepartmentsMenyView : Grid
    {
        private WaterCarrierService service => App.GetService<WaterCarrierService>();
        public DepartmentsMenyView()
        {
            InitializeComponent();
            Edit.IsEnabled = false;
            Delete.IsEnabled = false;
            service.CurentDepartmentChanged += Service_CurentDepartmentChanged;
        }

        private void Service_CurentDepartmentChanged(object? sender, EventArgs e)
        {
            Edit.IsEnabled = service.CurentDepartament != null;
            Delete.IsEnabled = service.CurentDepartament != null;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var editor = new DepartmentEditingWindow();
                editor.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var editor = new DepartmentEditingWindow(true);
                editor.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены что хотите удалить подразделение: " + service.CurentDepartament.Name + "?", "Удаление",
                      MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    service.DeleteDepartment(service.CurentDepartament);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
