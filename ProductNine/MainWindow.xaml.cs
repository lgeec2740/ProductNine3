using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ProductNine.DB;
using ProductNine.mvvm.vm;

namespace ProductNine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///     
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVM();
            //этот код удаляем после выполнения
            /*var t = new (string, string)[]
            {
                ("А112Т4", "А112Т4.jpg"),
                ("T793T4", "T793T4.jpg"),
                ("G387Y6", "G387Y6.jpg"),
                ("F573T5", "F573T5.jpg"),
                ("D735T5", "D735T5.jpg"),
                ("B736H6", "B736H6.jpg"),
                ("H384H3", "H384H3.jpg"),
                ("K437E6", "K437E6.jpg"),
                ("E732R7", "E732R7.jpg"),
                ("R836H6", "R836H6.jpg")
            };

            User21Context context = new User21Context();

            var products = context.Products.ToList();
            foreach (var tt in t)
            {
                var p = products.First(s => s.ProductArticleNumber == tt.Item1);
                if (p != null)
                    p.ProductPhoto = File.ReadAllBytes(tt.Item2);
            }

            context.SaveChanges();*/
        }
    }
}
