using MerryWaterCarrierTest.DisplayConverters;
using MerryWaterCarrierTest.Models;
using MerryWaterCarrierTest.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MerryWaterCarrierTest.Views
{
    /// <summary>
    /// Логика взаимодействия для DepartmentsView.xaml
    /// </summary>
    public partial class DepartmentsView : Grid
    {
        private WaterCarrierService service => App.GetService<WaterCarrierService>();
        public DepartmentsView()
        {
            InitializeComponent();
            DataGrid.ItemsSource = service.Departments;
            service.DepartmentsUpdated += Service_DepartmentsUpdated;
            service.EmployeesUpdated += Service_EmployeesUpdated;
        }

        private void Refresh()
        {
            try
            {
                DataGrid.ItemsSource = null;
                DataGrid.ItemsSource = service.Departments;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Service_EmployeesUpdated(object? sender, EventArgs e)
        {
            Refresh();
        }

        private void Service_DepartmentsUpdated(object? sender, EventArgs e)
        {
            Refresh();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems.Count > 0)
                {
                    if (e.AddedItems[0] is Department)
                        service.ChangeCurentDepartment(e.AddedItems[0] as Department);
                    else
                        service.ChangeCurentDepartment(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                if (e.Column.Header.ToString() == "Name")
                {
                    e.Column.Header = "Наименование";
                }
                if (e.Column.Header.ToString() == "Leader")
                {
                    e.Column.Header = "Руководитель";
                    var column = (DataGridTextColumn)e.Column;
                    var leaderConverter = new EmployeeConverter();
                    ((Binding)column.Binding).Converter = leaderConverter;
                }
                if (e.Column.Header.ToString() == "Id")
                {
                    e.Column.Visibility = Visibility.Collapsed;
                }
                if (e.Column.Header.ToString() == "LeaderId")
                {
                    e.Column.Visibility = Visibility.Collapsed;
                }
                if (e.Column.Header.ToString() == "Employees")
                {
                    e.Column.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
