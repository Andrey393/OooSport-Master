using OOOSport_Master.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OOOSport_Master
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public List<ProductClass> ordersView = new List<ProductClass>();
        public OrderWindow()
        {
            InitializeComponent();
            Helper.DB = new Entini.OOOSportMasterEntities1();
            this.ordersView = ProductWindow.orders;
        }

        double totalSale = 0;
        double totalSumma = 0;
        int allCountProduct = 0;
        double cost = 0;
        double sale = 0;
        int count = 0;
        public int index = 0;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowOrder();
        }
        
        void ShowOrder()
        {
            totalSale = 0;
            totalSumma = 0;
            allCountProduct = 0;
            cost = 0;
            sale = 0;
            count = 0;

            ListViewOrder.ItemsSource = null;
            ListViewOrder.ItemsSource = ordersView;
            
            foreach (var item in ordersView)
            {

                count += item.ProductCount;
                allCountProduct += item.ProductCount;
                cost = (double)(item.ProductCost * item.ProductCount);
                sale = (double) item.ProductDiscount / 100 * cost;
                totalSale += sale;
                totalSumma += cost;
            }
            TextBlockDescription.Text =
                "Количество позиций в заказе: " + ordersView.Count + Environment.NewLine +
                "Количество товаров в заказе: " + count + Environment.NewLine +
                "Общая сумма за весь товар: " + totalSumma + Environment.NewLine +
                "Общая сумма скидки:" + totalSale + Environment.NewLine +
                "Общая сумма за весь товар со скидкой:" + (totalSumma - totalSale);

        }       

        private void Window_Closed(object sender, EventArgs e)
        {
            ProductWindow productWindow = new ProductWindow();
            productWindow.Show();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            var item = button.DataContext; 
            ordersView.Remove((ProductClass)item);
            ShowOrder();
        }

        private void ListViewOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             index = ListViewOrder.SelectedIndex;
        }

        private void TextBoxCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 				
            {
                TextBox textBox = sender as TextBox;
                
                
                int res = Convert.ToInt32(textBox.Text);
                if (index == -1)
                {
                    MessageBox.Show("Сначало надо выделить объект");
                    return;
                }
                ordersView[index].ProductCount = res;
                if (res == 0)
                {
                    ordersView.RemoveAt(index);
                }
                ShowOrder();
            }

        }
    }
}
