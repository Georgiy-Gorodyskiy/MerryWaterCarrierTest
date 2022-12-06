using MerryWaterCarrierTest.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MerryWaterCarrierTest.Views
{
    /// <summary>
    /// Логика взаимодействия для OrdersMenuView.xaml
    /// </summary>
    public partial class OrdersMenuView : Grid
    {
        private WaterCarrierService service => App.GetService<WaterCarrierService>();
        public OrdersMenuView()
        {
            InitializeComponent();
            Edit.IsEnabled = false;
            Delete.IsEnabled = false;

            service.CurentOrderChanged += Service_CurentOrderChanged; ;
        }

        private void Service_CurentOrderChanged(object? sender, EventArgs e)
        {
            Edit.IsEnabled = service.CurentOrder != null;
            Delete.IsEnabled = service.CurentOrder != null;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var editor = new OrderEditingWindow();
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
                var editor = new OrderEditingWindow(true);
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
                if (MessageBox.Show("Вы уверены что хотите удалить Заказ: " + service.CurentOrder.Name + " №" + service.CurentOrder.Number + "?", "Удаление",
                      MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    service.DeleteOrder(service.CurentOrder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
