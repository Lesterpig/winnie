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

namespace Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void race_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image im = (Image)sender;
            int index = Int16.Parse(im.Tag.ToString());

            MainWindowViewModel vm = (MainWindowViewModel)this.DataContext;
            vm.SelectRace(index);
        }
    }
}
