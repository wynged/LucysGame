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
using LucysGame.Domain;

namespace LucysGame.Views
{
    /// <summary>
    /// Interaction logic for DrawChoiceWindow.xaml
    /// </summary>
    public partial class DrawChoiceWindow : Window
    {
        public CardChoice DrawCard;
        public DrawChoiceWindow()
        {
            InitializeComponent();
        }

        private void button_ChooseMain_Click(object sender, RoutedEventArgs e)
        {
            this.DrawCard = CardChoice.MainDeck;
            this.Close();
        }

        private void button_ChooseDiscard_Click(object sender, RoutedEventArgs e)
        {
            this.DrawCard = CardChoice.Discard;
            this.Close();
        }
    }
}
