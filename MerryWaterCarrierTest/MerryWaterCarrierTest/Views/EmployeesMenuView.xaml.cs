using MerryWaterCarrierTest.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MerryWaterCarrierTest.Views
{
    /// <summary>
    /// Логика взаимодействия для EmployeesMenuView.xaml
    /// </summary>
    public partial class EmployeesMenuView : Grid
    {
        private WaterCarrierService service => App.GetService<WaterCarrierService>();
        public EmployeesMenuView()
        {
            InitializeComponent();
            Edit.IsEnabled = false;
            Delete.IsEnabled = false;

            service.CurentEmployeeChanged += Service_CurentEmployeeChanged;
        }

        private void Service_CurentEmployeeChanged(object? sender, EventArgs e)
        {
            Edit.IsEnabled = service.CurentEmployee != null;
            Delete.IsEnabled = service.CurentEmployee != null;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var editor = new EmployeeEditingWindow();
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
                var editor = new EmployeeEditingWindow(true);
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
                if (MessageBox.Show("Вы уверены что хотите удалить сотрудника: " + service.CurentEmployee.DisplayName + "?", "Удаление",
                      MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    service.DeleteEmployee(service.CurentEmployee);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
