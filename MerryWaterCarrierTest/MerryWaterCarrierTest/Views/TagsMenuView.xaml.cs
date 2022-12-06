using MerryWaterCarrierTest.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MerryWaterCarrierTest.Views
{
    /// <summary>
    /// Логика взаимодействия для TagsMenuView.xaml
    /// </summary>
    public partial class TagsMenuView : Grid
    {
        private WaterCarrierService service => App.GetService<WaterCarrierService>();
        public TagsMenuView()
        {
            InitializeComponent();
            Edit.IsEnabled = false;
            Delete.IsEnabled = false;

            service.CurentTagChanged += Service_CurentTagChanged;
        }

        private void Service_CurentTagChanged(object? sender, EventArgs e)
        {
            Edit.IsEnabled = service.CurentTag != null;
            Delete.IsEnabled = service.CurentTag != null;
        }


        private void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var editor = new TagEditingWindow();
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
                var editor = new TagEditingWindow(true);
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
                if (MessageBox.Show("Вы уверены что хотите удалить Тег: " + service.CurentTag.Name + "?", "Удаление",
                      MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    service.DeleteTag(service.CurentTag);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
