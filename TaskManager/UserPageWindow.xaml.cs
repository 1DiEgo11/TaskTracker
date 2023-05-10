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

namespace TaskManager
{
    /// <summary>
    /// Логика взаимодействия для UserPageWindow.xaml
    /// </summary>
    public partial class UserPageWindow : Window
    {
        public UserPageWindow()
        {
            InitializeComponent();

            var d = new Board();
            Background = new SolidColorBrush(Colors.LightGray);
            Content = d.Draw_Stack(this);
            Show();

            PreviewKeyDown += Key_down;
        }

        private void Key_down(object sender, KeyEventArgs e)
        {
           var m = new Moving_cards();
           if(e.Key == Key.P) 
           {
                m.Move(Content as ScrollViewer, this);
           }
        }
    }
}
