using OOOSport_Master.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Word = Microsoft.Office.Interop.Word;

namespace OOOSport_Master
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public List<ProductClass> ordersView = new List<ProductClass>();
        string pathExe = System.AppDomain.CurrentDomain.BaseDirectory; //Путь
        Word.Application wordApp;
        Word.Document wordDoc;
        Word.Paragraph wordPar;
        Word.Range wordRange;
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
        int uniqueCode,nomer, orderId;
       
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Helper.User != null)
            {
                LabelNameUser.Content = Helper.User.UserLastName + "Роль: " + Helper.User.Role.RoleName;
            }

            var point = Helper.DB.Point.Select(x => x.PointAddress).ToList();
            ComboBoxPoint.Items.Clear();
            ComboBoxPoint.ItemsSource = point;

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

      

        void DataBaseOrder()
        {
            uniqueCode = nomer;
            DateTime date = DateTime.Now;
            DateTime dateDelivery = DateTime.Now.AddDays(3);

            int countInStock;
            foreach (var item in ordersView)
            {
                Entini.Product product = Helper.DB.Product.Find(item.Artikle);
                countInStock = product.ProductCount;
                if (countInStock < 3)
                {
                    dateDelivery = DateTime.Now.AddDays(6);
                    break;
                }
            }

            int idPickupPoint = Convert.ToInt32(ComboBoxPoint.SelectedIndex) + 1;
            Entini.Order order;

            do
            {
                order = Helper.DB.Order.Where(x => x.UniqueCode == nomer).FirstOrDefault();
            }
            while (order != null);

            string userFullName = Helper.User.UserLastName +" " + Helper.User.UserName + " " + Helper.User.UserMiddleName;

            Entini.Order newOrder = new Entini.Order();
            newOrder.OrderId = Helper.DB.Order.ToList().ToList().Last().OrderId + 1;
            newOrder.OrderDate = date;
            newOrder.OrderDeliveryDate = dateDelivery;
            newOrder.PointId = idPickupPoint;
            newOrder.UserFullName = userFullName;
            newOrder.UniqueCode = uniqueCode;
            newOrder.StatusId = 1;

            try
            {
                Helper.DB.Order.Add(newOrder);
                Helper.DB.SaveChanges();

                orderId = Helper.DB.Order.ToList().ToList().Last().OrderId;
                foreach (var item in ordersView)
                {
                    Entini.OrderProduct orderProduct = new Entini.OrderProduct();
                    orderProduct.OrderId = orderId;
                    orderProduct.ProductArticle = item.Artikle;
                    orderProduct.ProductCount = item.ProductCount;
                    try
                    {
                        Helper.DB.OrderProduct.Add(orderProduct);
                        Helper.DB.SaveChanges();
                    }
                    catch
                    {
                        MessageBox.Show("Не удалось оформить заказ");
                        this.Close();
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось оформить заказ");
                this.Close();
                return;
            }

        }

        private void ButtonCreateChek_Click(object sender, RoutedEventArgs e)
        {
            DataBaseOrder();

            try
            {
                wordApp = new Word.Application();
                wordApp.Visible = false;
            }
            catch
            {
                MessageBox.Show("Товарный чек в Word создать не удалось");
                return;
            }

            wordDoc = wordApp.Documents.Add();
            wordDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait;

            wordDoc.Content.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            Word.Paragraph paragraphPicter = wordDoc.Paragraphs.Add();
            Word.Range rangePicter = paragraphPicter.Range;
            wordDoc.Content.Font.Size = 14;
            Word.InlineShape wordPicter = wordDoc.InlineShapes.AddPicture(pathExe + "\\" + "logotip.png", Range: wordApp.Selection.Range);
            wordPicter.Width = 70;
            wordPicter.Height = 70;
            rangePicter.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            rangePicter.InsertParagraphAfter();

            Word.Paragraph paragraphTitle = wordDoc.Paragraphs.Add();
            Word.Range rangeTitle = paragraphTitle.Range;
            rangeTitle.Text = " Номер заказа: №" + nomer;
            rangeTitle.Font.Bold = 1;
            rangeTitle.Font.Size = 20;

            rangeTitle.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            rangeTitle.InsertParagraphAfter();

            Word.Paragraph summa = wordDoc.Paragraphs.Add();
            Word.Range rangesumma = summa.Range;
            rangesumma.Text += "Дата заказа: " + DateTime.Now;
            rangesumma.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            rangesumma.InsertParagraphAfter();


            wordPar = (Word.Paragraph)wordDoc.Paragraphs[1];
            wordPar = wordDoc.Paragraphs.Add();
            wordRange = wordPar.Range;


            Word.Table wordTable;
            wordTable = wordDoc.Tables.Add(wordRange,ListViewOrder.Items.Count + 1, 2);
            wordTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            wordTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;


            Word.Range cellRange;
            cellRange = wordTable.Cell(1, 1).Range;
            cellRange.Text = "Товар";
            cellRange = wordTable.Cell(1, 2).Range;
            cellRange.Text = "Количество";
            int i = 0;
            foreach (var item in ordersView)
            {
                Entini.Product product = Helper.DB.Product.Find(item.Artikle);
                cellRange = wordTable.Cell(i + 2, 1).Range;
                cellRange.Text = product.ProductName;

                cellRange = wordTable.Cell(i + 2, 2).Range;
                cellRange.Text = ordersView[i].ProductCount.ToString();

                i++;

            }


            Word.Paragraph paragraphtext = wordDoc.Paragraphs.Add();
            Word.Range rangetext = paragraphtext.Range;
            rangetext.Text = "Сумма заказа: " + totalSumma + Environment.NewLine;
            rangetext.Text += "Сумма скидки: " + totalSale + Environment.NewLine;
            rangetext.Text += "Пункт выдачи: " + ComboBoxPoint.Text + Environment.NewLine;
            rangetext.Text += "Дата получение: " + DateTime.Now.AddDays(5); ;
            rangetext.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            rangetext.InsertParagraphAfter();

            ///
            wordDoc.Saved = true;

            string pathDoc = pathExe + "Word\\" + nomer;
            wordDoc.SaveAs(pathDoc + ".docx");


            MessageBox.Show("Талон оформлен");

            wordDoc.SaveAs(pathDoc + ".pdf", Word.WdExportFormat.wdExportFormatPDF);
            wordDoc.Close(true, null, null);
            wordDoc = null;

            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(wordApp);
            GC.Collect();
        }

        private void ButtonCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            Random ran = new Random();
            nomer = ran.Next(100, 999);
            string text = "Номер заказа: " + nomer + Environment.NewLine
                + "Дата создание: " + DateTime.Now + Environment.NewLine
                + "Дата доставки: " + DateTime.Now.AddDays(6);

            MessageBox.Show(text);
            ButtonCreateOrder.IsEnabled = false;
            ButtonCreateChek.IsEnabled = true;
        }
    }
}
