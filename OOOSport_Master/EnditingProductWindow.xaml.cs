using OOOSport_Master.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для EnditingProductWindow.xaml
    /// </summary>
    public partial class EnditingProductWindow : Window
    {
        public List<Entini.Product> enditinProduct;
        int index;

        public EnditingProductWindow()
        {
            InitializeComponent();
            this.enditinProduct = ProductWindow.productsData.ToList();
            this.index = ProductWindow.index;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var category = Helper.DB.Category.Select(x=> x.CategoryName).ToList();
            ComboBoxCategory.Items.Clear();
            ComboBoxCategory.ItemsSource = category;
            ComboBoxCategory.SelectedIndex = enditinProduct[index].ProductCategoryId - 1;


            var provider = Helper.DB.Provider.Select(x => x.ProviderName).ToList();
            ComboBoxProvider.Items.Clear();
            ComboBoxProvider.ItemsSource = provider;
            ComboBoxProvider.SelectedIndex = enditinProduct[index].ProductProviderId -1;


            var manufactor = Helper.DB.Manufacturer.Select(x => x.ManufacturerName).ToList();
            ComboBoxManufactory.Items.Clear();
            ComboBoxManufactory.ItemsSource = manufactor;
            ComboBoxManufactory.SelectedIndex = enditinProduct[index].ProductManufactureId - 1;


            TextBoxArtikle.Text = enditinProduct[index].ProductArticle;
            TextBoxName.Text = enditinProduct[index].ProductName;
            TextBoxDecription.Text = enditinProduct[index].ProductDecription;
            TextBoxCost.Text = enditinProduct[index].ProductCost.ToString();
            TextBoxDiscount.Text = enditinProduct[index].ProductDiscount.ToString();
            ImageProduct.Source = new BitmapImage(new Uri("Image/"+ enditinProduct[index].ProductPhoto, UriKind.Relative));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ProductWindow product = new ProductWindow();
            product.Show();
            this.Close();
        }
    }
}
