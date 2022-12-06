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
    /// Логика взаимодействия для EmployeesView.xaml
    /// </summary>
    public partial class EmployeesView : Grid
    {
        private WaterCarrierService service => App.GetService<WaterCarrierService>();
        public EmployeesView()
        {
            InitializeComponent();

            DataGrid.ItemsSource = service.Employees;
            service.EmployeesUpdated += Service_EmployeesUpdated;
            service.DepartmentsUpdated += Service_DepartmentsUpdated;
        }

        private void Refresh()
        {
            try
            {
                DataGrid.ItemsSource = null;
                DataGrid.ItemsSource = service.Employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Service_DepartmentsUpdated(object? sender, EventArgs e)
        {
            Refresh();
        }

        private void Service_EmployeesUpdated(object? sender, EventArgs e)
        {
            Refresh();
        }


        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                if (e.Column.Header.ToString() == "Id")
                {
                    e.Column.Visibility = Visibility.Collapsed;
                }
                if (e.Column.Header.ToString() == "Name")
                {
                    e.Column.Header = "Имя";
                }
                if (e.Column.Header.ToString() == "Patronymic")
                {
                    e.Column.Header = "Отчество";
                }
                if (e.Column.Header.ToString() == "Surname")
                {
                    e.Column.Header = "Фамилия";
                }
                if (e.Column.Header.ToString() == "Gender")
                {
                    e.Column = new DataGridTextColumn();
                    e.Column.Header = "Пол";
                    var column = (DataGridTextColumn)e.Column;
                    column.Binding = new Binding("Gender");
                    var enumDescriptionConverter = new EnumDescriptionConverter();
                    ((Binding)column.Binding).Converter = enumDescriptionConverter;
                }
                if (e.Column.Header.ToString() == "Department")
                {
                    e.Column.Header = "Подразделение";
                    var column = (DataGridTextColumn)e.Column;
                    var departmentConverter = new DepartmentConverter();
                    ((Binding)column.Binding).Converter = departmentConverter;
                }
                if (e.Column.Header.ToString() == "DepartmentId")
                {
                    e.Column.Visibility = Visibility.Collapsed;
                }
                if (e.Column.Header.ToString() == "DisplayName")
                {
                    e.Column.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems.Count > 0)
                {
                    if(e.AddedItems[0] is Employee)
                        service.ChangeCurentEmployee(e.AddedItems[0] as Employee);
                    else
                        service.ChangeCurentEmployee(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
