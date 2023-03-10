using OOOSport_Master.Classes;
using OOOSport_Master.Entini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OOOSport_Master
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public ProductWindow()
        {
            InitializeComponent();
            Helper.DB = new Entini.OOOSportMasterEntities1();
        }
        public static List<Entini.Product> productsData;
        public static List<Classes.ProductClass> orders; // Добавление заказы
        List<Classes.ProductClass> producList = new List<ProductClass>();
        public static int index;

        int filterCategory;
        string searchProduct;
        string sort;
        
        void ShowProduct()
        {
            productsData = Helper.DB.Product.ToList();
            producList.Clear();

            if (filterCategory != 0)
            {
                productsData = productsData.Where(x => x.ProductCategoryId == filterCategory).ToList();
            }
            if (!String.IsNullOrEmpty(TextBoxSearch.Text))
            {
                productsData = productsData.Where(x => x.ProductName.Contains(searchProduct)).ToList();
            }
            if (sort == "ASC")
            {
                productsData = productsData.OrderBy(x => x.ProductCost).ToList();
            }
            else
            {
                productsData = productsData.OrderByDescending(x => x.ProductCost).ToList();
            }

            int i = 0;
            foreach (var item in productsData)
            {

                producList.Add(new Classes.ProductClass
                {
                    Artikle = productsData[i].ProductArticle,
                    ProductPhoto =productsData[i].ProductPhoto,
                    Image= productsData[i].ProductPhoto,
                    ProductName = productsData[i].ProductName,
                    ProductDecription = productsData[i].ProductDecription,
                    ProductCategory = productsData[i].Category.CategoryName,
                    ProductCost = productsData[i].ProductCost,
                    ProductDiscount = productsData[i].ProductDiscount,
                    ProductCostDiscount = productsData[i].ProductCost - (productsData[i].ProductCost * productsData[i].ProductDiscount)/100,
                    ProductCount = 1
                    
                });
                i++;
            }
            ListViewProduct.ItemsSource = null;
            ListViewProduct.ItemsSource = producList;

        }

      

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(Helper.User != null)
            {
                LabelUser.Content = Helper.User.UserLastName + "Роль: " + Helper.User.Role.RoleName;
            }
            if(Helper.User.RoleID == 2 || Helper.User.RoleID == 1)
            {
                ButtonAddProduct.Visibility = Visibility.Visible;
            }

            orders = new List<Classes.ProductClass>();
            LabelUser.Content = Helper.User.UserName + " Роль:" + Helper.User.Role.RoleName;

            var category = Helper.DB.Category.Select(x => x.CategoryName).ToList();
            category.Insert(0, "Все категори");
            ComboBoxCategory.Items.Clear();
            ComboBoxCategory.ItemsSource = category;
            ShowProduct();
        }

        private void ComboBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxSort.SelectedIndex == 0) sort = "ASC";
            else sort = "DESC";
            ShowProduct();

        }

        private void ComboBoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filterCategory = ComboBoxCategory.SelectedIndex;
            ShowProduct();

        }

        private void ComboBoxDiscoint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //filterDiscount = ComboBoxDiscoint.SelectedIndex;
            //ShowProduct();

        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchProduct = TextBoxSearch.Text;
            ShowProduct();

        }

        private void ListViewProduct_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Classes.ProductClass item = (ProductClass)(sender as ListView).SelectedItem;
            int index = orders.FindIndex(x => x.Artikle == item.Artikle);

            if (index < 0)
            {
                orders.Add(new Classes.ProductClass
                {
                    Artikle = item.Artikle,
                    ProductPhoto = item.ProductPhoto,
                    Image = item.Image,
                    ProductName = item.ProductName,
                    ProductDecription = item.ProductDecription,
                    ProductCategory = item.ProductCategory,
                    ProductCost = item.ProductCost,
                    ProductDiscount = item.ProductDiscount,
                    ProductCostDiscount = item.ProductCost - (item.ProductCost * item.ProductDiscount) / 100,
                    ProductCount = 1

                });
            }
            else
            {
                orders[index].ProductCount++;
            }
            if (orders !=null)
            {
                ButtonOrder.IsEnabled = true;
            }
        }

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow();
            orderWindow.Show();
            this.Hide();
        }

        private void ListViewProduct_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            index = ListViewProduct.SelectedIndex;
            EnditingProductWindow enditing= new EnditingProductWindow();
            enditing.Show();
            this.Hide();
        }
    }
}
