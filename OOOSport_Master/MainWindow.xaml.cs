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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOOSport_Master
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Helper.DB = new Entini.OOOSportMasterEntities1();
        }

        int popitk = 0;

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            string Login = TextBoxLogin.Text;
            string Password = TextBoxPassword.Text;

            if (popitk > 0)
            {
                if (TextBoxCapcha.Text != CapchaElement.CaptchaText)
                {
                    MessageBox.Show("Неправильная капча");
                    CapchaElement.CreateCaptcha(EasyCaptcha.Wpf.Captcha.LetterOption.Alphanumeric, 5);
                    return;

                }

            }
            Helper.User = Helper.DB.User.Where(x => x.UserLogin == Login && x.UserPassword == Password).ToList().FirstOrDefault();
            if (Helper.User != null)
            {
                ProductWindow product = new ProductWindow();
                product.Show();
                this.Hide();
            }
            else
            {
                popitk++;
                MessageBox.Show("Неправильный логин или пароль");
                CapchaElement.CreateCaptcha(EasyCaptcha.Wpf.Captcha.LetterOption.Alphanumeric, 5);
                CapchaElement.Visibility = Visibility.Visible;
                TextBoxCapcha.Visibility = Visibility.Visible;
            }


        }
    }
}
