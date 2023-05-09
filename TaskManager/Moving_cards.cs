using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TaskManager
{
    internal class Moving_cards
    {
        public static void Move(StackPanel content)
        {
            Border s = (Border)content.Children[0];
            StackPanel a = s.Child as StackPanel;
            Button b = a.Children[0] as Button;
            b.Background = new SolidColorBrush(Colors.Red);

            if ((Keyboard.GetKeyStates(Key.Down) & KeyStates.Down) > 0)
            {
                Move_Down();
            }
            

        }
        private static void Move_Up() { }
        private static void Move_Down() 
        {
            MessageBox.Show("Pidoras");
        }
        private static void Move_Left() { }
        private static void Move_Right() { }

    }
}
