using OOOSport_Master.Classes;
using OOOSport_Master.Entini;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    /// Логика взаимодействия для EnditingProductWindow.xaml
    /// </summary>
    public partial class EnditingProductWindow : Window
    {
        public List<Entini.Product> enditinProduct;
        Entini.Product product;
        int index;

        public EnditingProductWindow()
        {
            InitializeComponent();
            this.enditinProduct = ProductWindow.productsData.ToList();
            this.index = ProductWindow.indexList;
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
            TextBoxCount.Text = enditinProduct[index].ProductCount.ToString();
            ImageProduct.Source = new BitmapImage(new Uri("Image/"+ enditinProduct[index].ProductPhoto, UriKind.Relative));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ProductWindow productWindow = new ProductWindow();
            productWindow.Show();
            this.Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Data();
                //if (photo != null)
                //{
                //    string s = path + "\\Товар\\" + productArticle + ".jpg";
                //    if (File.Exists(s))
                //    {
                //        File.Delete(s);
                //    }
                //    pictureBoxProduct.BackgroundImage.Save(s);
                //}
                Helper.DB.SaveChanges();
                MessageBox.Show("Информация в БД успешно обновлена");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при обновление данных в БД");
                return;
            }

        }
        void Data()
        {
            string artickle;

            artickle = TextBoxArtikle.Text;
            if (string.IsNullOrEmpty(artickle))
            {
                Entini.Product productFind = Helper.DB.Product.Find(artickle);
                if (productFind != null)
                {
                    MessageBox.Show("Такой артикл уже есть");
                    return;
                }
                product = new Entini.Product();
            }
            else
            {
                product = Helper.DB.Product.Find(artickle);

            }

            product.ProductArticle = artickle;
            product.ProductName = TextBoxName.Text;
            product.ProductDecription = TextBoxDecription.Text;
           // product.ProductPhoto = photo;
            product.ProductDiscount = Convert.ToInt32(TextBoxDiscount.Text);
            product.ProductCost = Convert.ToDouble(TextBoxCost.Text);
            product.ProductManufactureId = ComboBoxManufactory.SelectedIndex + 1;
            product.ProductProviderId = 1;
            product.ProductCategoryId = ComboBoxCategory.SelectedIndex + 1;
            product.ProductUnitId = 1;
            product.ProductCount = Convert.ToInt32(TextBoxCount.Text);

        }
    }
}
