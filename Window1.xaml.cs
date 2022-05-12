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

namespace ibPR4v2
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void mediaEnded(object sender, RoutedEventArgs e)
        {
            player.Play();
        }

        private void passClick(object sender, RoutedEventArgs e)
        {
            edirPass edit = new edirPass();
            mainFrame.NavigationService.Navigate(edit);
        }
    }
}
