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
    /// Логика взаимодействия для OrdersView.xaml
    /// </summary>
    public partial class OrdersView : Grid
    {
        private WaterCarrierService service => App.GetService<WaterCarrierService>();
        public OrdersView()
        {

            InitializeComponent();
            DataGrid.ItemsSource = service.Orders;
            service.OrdersUpdated += Service_OrdersUpdated;
            service.TagsUpdated += Service_TagsUpdated;
            service.EmployeesUpdated += Service_EmployeesUpdated;
        }

        private void Refresh()
        {
            try
            {
                DataGrid.ItemsSource = null;
                DataGrid.ItemsSource = service.Orders;
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

        private void Service_TagsUpdated(object? sender, EventArgs e)
        {
            Refresh();
        }
        private void Service_OrdersUpdated(object? sender, EventArgs e)
        {
            Refresh();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems.Count > 0)
                {
                    if (e.AddedItems[0] is Order)
                        service.ChangeCurentOrder(e.AddedItems[0] as Order);
                    else
                        service.ChangeCurentOrder(null);
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
                if (e.Column.Header.ToString() == "Id")
                {
                    e.Column.Visibility = Visibility.Collapsed;
                }
                if (e.Column.Header.ToString() == "Number")
                {
                    e.Column.Header = "Номер";
                }
                if (e.Column.Header.ToString() == "Name")
                {
                    e.Column.Header = "Товар";
                }
                if (e.Column.Header.ToString() == "Employee")
                {
                    e.Column.Header = "Сотрудник";
                    var column = (DataGridTextColumn)e.Column;
                    var employeeConverter = new EmployeeConverter();
                    ((Binding)column.Binding).Converter = employeeConverter;
                }
                if (e.Column.Header.ToString() == "Tags")
                {
                    e.Column.Header = "Теги";
                    var column = (DataGridTextColumn)e.Column;
                    var tagsConverter = new TagsConverter();
                    ((Binding)column.Binding).Converter = tagsConverter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
