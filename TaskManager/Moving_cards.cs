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
        private const string select_color = "#00FFFF";
        private int index_colm, index_card;
        private Button btn;
        private Brush prev_color;
        private StackPanel main_stack, stack_cards;
        private Window window;
        private Desk desk;
        private List<User> users = Read.Reading();
        private Card select_card;

        public void Move(ScrollViewer content, Window _window, Desk _desk)
        {

            desk = _desk;
            window = _window;
            main_stack = ((StackPanel)content.Content).Children[2] as StackPanel;
            stack_cards = (StackPanel)((Border)main_stack.Children[0]).Child;
            index_colm = 0;
            btn = stack_cards.Children[0] as Button;
            index_card = 0;
            prev_color = btn.Background;
            btn.Background = Converter(select_color);
            window.PreviewKeyDown += Select_card;
            window.PreviewKeyDown += Move_card_Where;
        }
        
        private void Select_card(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                Sel_Down();
            }
            else if (e.Key == Key.Up)
            {
                Sel_Up();
            }
            else if (e.Key == Key.Left)
            {
                Sel_Left();
            }
            else if (e.Key == Key.Right)
            {
                Sel_Right();
            }
            else if (e.Key == Key.O)
            {
                Back(window);
            }
        }
        private void Move_card_Where(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                Move_Left();
            }
            else if (e.Key == Key.D)
            {
                Move_Right();
            }
            else if (e.Key == Key.O)
            {
                Back(window);
            }
        }
        private void Back(Window window)
        {
            btn.Background = prev_color;
            Keyboard.RemovePreviewKeyDownHandler(window, Select_card);
            Keyboard.RemovePreviewKeyDownHandler(window, Move_card_Where);
        }
        private void Sel_Up() 
        {
            if(index_card > 0)
            {
                btn.Background = prev_color;
                btn = stack_cards.Children[index_card - 1] as Button;
                index_card--;
                prev_color = btn.Background;
                btn.Background = Converter(select_color);
            }
        }
        private void Sel_Down() 
        {
            if(index_card < stack_cards.Children.Count - 2)
            {
                btn.Background = prev_color;
                btn = stack_cards.Children[index_card + 1] as Button;
                index_card++;
                prev_color = btn.Background;
                btn.Background = Converter(select_color);
            }
        }
        private void Sel_Left() 
        {
            if(index_colm > 0 && main_stack.Children.Count > 0)
            {
                btn.Background = prev_color;
                stack_cards = (StackPanel)((Border)main_stack.Children[index_colm - 1]).Child;
                index_colm--; 
                btn = stack_cards.Children[0] as Button;
                index_card = 0;
                prev_color = btn.Background;
                btn.Background = Converter(select_color);
            }
        }
        private void Sel_Right() 
        {
            if (main_stack.Children.Count > 0 && index_colm < main_stack.Children.Count - 2)
            {
                btn.Background = prev_color;
                stack_cards = (StackPanel)((Border)main_stack.Children[index_colm + 1]).Child;
                index_colm++; 
                btn = stack_cards.Children[0] as Button;
                index_card = 0;
                prev_color = btn.Background;
                btn.Background = Converter(select_color);
            }
        }
        private void Move_Right()
        {
            if (index_colm < main_stack.Children.Count - 2 && (string)btn.Content != "+ Карточка")
            {
                stack_cards.Children.RemoveAt(index_card);

                index_colm++;
                stack_cards = (StackPanel)((Border)main_stack.Children[index_colm]).Child;
                stack_cards.Children.Insert(index_card, btn);
                
            }
        }
        private void Move_Left()
        {
            if (index_colm > 0 && (string)btn.Content != "+ Карточка")
            {
                stack_cards.Children.RemoveAt(index_card);
                index_colm--;
                stack_cards = (StackPanel)((Border)main_stack.Children[index_colm]).Child;
                stack_cards.Children.Insert(index_card, btn);
            }
        }

        public static SolidColorBrush Converter(string s)
        {
            return (SolidColorBrush)new BrushConverter().ConvertFrom(s);
        }
    }
}
