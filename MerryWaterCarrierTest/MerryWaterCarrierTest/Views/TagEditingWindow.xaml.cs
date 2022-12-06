using MerryWaterCarrierTest.Models;
using MerryWaterCarrierTest.Services;
using System;
using System.Collections.Generic;
using System.Windows;

namespace MerryWaterCarrierTest.Views
{
    /// <summary>
    /// Логика взаимодействия для TagEditingWindow.xaml
    /// </summary>
    public partial class TagEditingWindow : Window
    {
        private Tag? curentTag;
        private WaterCarrierService service => App.GetService<WaterCarrierService>();
        public TagEditingWindow(bool isEdit = false)
        {
            try
            {
                InitializeComponent();
                curentTag = isEdit ? service.CurentTag : null;
                if (curentTag != null)
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
            NameTxt.Text = curentTag?.Name;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (curentTag == null)
                {
                    curentTag = new Tag();
                    curentTag.Orders = new List<Order>();
                }
                curentTag.Name = NameTxt.Text;
                service.EditTag(curentTag);
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
