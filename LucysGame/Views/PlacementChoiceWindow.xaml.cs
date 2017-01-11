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

namespace LucysGame.Views
{
    /// <summary>
    /// Interaction logic for DrawChoiceWindow.xaml
    /// </summary>
    public partial class PlacementChoiceWindow : Window
    {
        public LucysGame.Domain.CardPlacement Placement;
        public PlacementChoiceWindow()
        {
            InitializeComponent();
        }

        private void button_V1_Click(object sender, RoutedEventArgs e)
        {
            Placement = Domain.CardPlacement.V1;
            this.Close();
        }

        private void button_H1_Click(object sender, RoutedEventArgs e)
        {
            Placement = Domain.CardPlacement.H1;
            this.Close();
        }

        private void button_V2_Click(object sender, RoutedEventArgs e)
        {
            Placement = Domain.CardPlacement.V2;
            this.Close();
        }

        private void button_H2_Click(object sender, RoutedEventArgs e)
        {
            Placement = Domain.CardPlacement.H2;
            this.Close();
        }

        private void button_Discard_Click(object sender, RoutedEventArgs e)
        {
            Placement = Domain.CardPlacement.Discard;
            this.Close();
        }
    }
}
