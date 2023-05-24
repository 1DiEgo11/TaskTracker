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
        private Cards select_card;

        public void Move(ScrollViewer content, Window _window, Desk _desk)
        {
            desk = _desk;
            window = _window;
            index_colm = Get_first_index_column();
            if(index_colm == -1)
            {
                return;
            }
            index_card = 1;

            main_stack = ((StackPanel)content.Content).Children[2] as StackPanel;
            stack_cards = (StackPanel)((Border)main_stack.Children[index_colm]).Child;

            select_card = desk.column[index_colm].cards[index_card - 1];
            btn = select_card.btn;
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
            if(index_card > 1)
            {
                btn.Background = prev_color;
                index_card--;
                btn = desk.column[index_colm].cards[index_card - 1].btn;
                select_card = desk.column[index_colm].cards[index_card - 1];
                prev_color = btn.Background;
                btn.Background = Converter(select_color);
            }
        }
        private void Sel_Down() 
        {
            if(index_card < desk.column[index_colm].cards.Count)
            {
                btn.Background = prev_color;
                index_card++;
                btn = desk.column[index_colm].cards[index_card - 1].btn;
                select_card = desk.column[index_colm].cards[index_card - 1];
                prev_color = btn.Background;
                btn.Background = Converter(select_color);
            }
        }
        private void Sel_Left() 
        {
            if(index_colm > 0 && desk.column.Count > 0 && desk.column[index_colm - 1].cards.Count > 0)
            {
                btn.Background = prev_color;
                stack_cards = (StackPanel)((Border)main_stack.Children[index_colm - 1]).Child;
                index_colm--; 
                index_card = 1;
                btn = desk.column[index_colm].cards[index_card - 1].btn;
                select_card = desk.column[index_colm].cards[index_card - 1];
                prev_color = btn.Background;
                btn.Background = Converter(select_color);
            }
        }
        private void Sel_Right() 
        {
            if (desk.column.Count > 0 && index_colm < desk.column.Count - 1 && desk.column[index_colm + 1].cards.Count > 0)
            {
                btn.Background = prev_color;
                stack_cards = (StackPanel)((Border)main_stack.Children[index_colm + 1]).Child;
                index_colm++;
                index_card = 1;
                btn = desk.column[index_colm].cards[index_card - 1].btn;
                select_card = desk.column[index_colm].cards[index_card - 1];
                prev_color = btn.Background;
                btn.Background = Converter(select_color);
            }
        }
        private void Move_Right()
        {
            if (index_colm < desk.column.Count - 1 && (string)btn.Content != "+ Карточка")
            {
                stack_cards.Children.RemoveAt(index_card);
                
                users[desk.parrent_id - 1].desk[select_card.path[1]].column[index_colm].cards.RemoveAt(index_card - 1);
                desk.column[index_colm].cards.RemoveAt(index_card - 1);

                index_colm++;
                index_card = 1;

                stack_cards = (StackPanel)((Border)main_stack.Children[index_colm]).Child;
                stack_cards.Children.Insert(index_card, btn);

                users[desk.parrent_id - 1].desk[select_card.path[1]].column[index_colm].cards.Insert(index_card - 1, select_card);
                desk.column[index_colm].cards.Insert(index_card - 1, select_card);
                Read.Write(users);
                Read.Rewriting_Cards_path(users, desk.parrent_id - 1, select_card.path[1], index_colm);
                users = Read.Reading();
            }
        }
        private void Move_Left()
        {
            if (index_colm > 0 && (string)btn.Content != "+ Карточка")
            {
                stack_cards.Children.RemoveAt(index_card);

                users[desk.parrent_id - 1].desk[select_card.path[1]].column[index_colm].cards.RemoveAt(index_card - 1);
                desk.column[index_colm].cards.RemoveAt(index_card - 1);

                index_colm--;
                index_card = 1;

                stack_cards = (StackPanel)((Border)main_stack.Children[index_colm]).Child;
                stack_cards.Children.Insert(index_card, btn);

                users[desk.parrent_id - 1].desk[select_card.path[1]].column[index_colm].cards.Insert(index_card - 1, select_card);
                desk.column[index_colm].cards.Insert(index_card - 1, select_card);
                Read.Write(users);
                Read.Rewriting_Cards_path(users, desk.parrent_id - 1, select_card.path[1], index_colm);
                users = Read.Reading();
            }
        }

        public static SolidColorBrush Converter(string s)
        {
            return (SolidColorBrush)new BrushConverter().ConvertFrom(s);
        }

        private int Get_first_index_column()
        {
            int index_colm1 = -1;
            for(int i = 0; i < desk.column.Count; i++)
            {
                if (desk.column[i].cards != null)
                {
                    if (desk.column[i].cards.Count > 0)
                        return i;
                }
            }
            return index_colm1;
        }

        private int Get_id_desk()
        {
            int id = 0;
            for (int i = 0; i < users[desk.parrent_id - 1].desk.Count; i++)
            {
                if (desk == users[desk.parrent_id - 1].desk[i])
                {
                    id = i;
                }
            }
            return id;
        }
    }
}
