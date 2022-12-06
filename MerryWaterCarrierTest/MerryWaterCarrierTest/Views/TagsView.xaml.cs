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
    /// Логика взаимодействия для TagsView.xaml
    /// </summary>
    public partial class TagsView : Grid
    {
        private WaterCarrierService service => App.GetService<WaterCarrierService>();
        public TagsView()
        {

            InitializeComponent();
            DataGrid.ItemsSource = service.Tags;
            service.TagsUpdated += Service_TagsUpdated;
            service.OrdersUpdated += Service_OrdersUpdated;
        }

        private void Refresh()
        {
            try
            {
                DataGrid.ItemsSource = null;
                DataGrid.ItemsSource = service.Tags;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Service_OrdersUpdated(object? sender, EventArgs e)
        {
            Refresh();
        }

        private void Service_TagsUpdated(object? sender, EventArgs e)
        {
            Refresh();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems.Count > 0)
                {
                    if (e.AddedItems[0] is Tag)
                        service.ChangeCurentTag(e.AddedItems[0] as Tag);
                    else
                        service.ChangeCurentTag(null);
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
                if (e.Column.Header.ToString() == "Name")
                {
                    e.Column.Header = "Товар";
                }
                if (e.Column.Header.ToString() == "Orders")
                {
                    e.Column.Header = "Заказы";
                    var column = (DataGridTextColumn)e.Column;
                    var ordersConverter = new OrdersConverter();
                    ((Binding)column.Binding).Converter = ordersConverter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
