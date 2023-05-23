using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media.Effects;
using System.Windows.Input;

namespace TaskManager
{
    internal class BoardColumn
    {
        private StackPanel myStackPanel;
        private ScrollViewer myScrollViewer;
        private List<User> users;
        private List<Column> col;
        private string nameB;
        private Window window;
        private User user;
        private int des;
        private int num_col;
        private Desk desk;
        
        public ScrollViewer Draw_Stack(List<User> _users, string name_of_board, Window _window, User _user, Desk desk)
        {
            this.desk = desk;
            user = _user;
            window = _window;
            window.PreviewKeyDown += Key_down;
            users = _users;
            col = desk.column;
            nameB = name_of_board;
            for (int i = 0; i < users[desk.parrent_id - 1].desk.Count; i++) 
            {
                if (col == users[desk.parrent_id - 1].desk[i].column) 
                { 
                    des = i; 
                } 
            }
            myScrollViewer = new ScrollViewer
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                Content = VertStack()
            };

            return myScrollViewer;
        }
        private StackPanel VertStack()
        {
            var nameBoard = new TextBlock
            {
                Margin = new Thickness(4, 5, 30, 0),
                Text = "Столбцы из " + nameB,
                FontSize = 20
            };

            StackPanel panel = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            panel.Children.Add(nameBoard);
            panel.Children.Add(Back_to_the_boards());
            panel.Children.Add(All_Coloum());
            return panel;
        }

        private StackPanel All_Coloum()
        {
            myStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            myStackPanel = Stack();

            return myStackPanel;
        }

        private Button Back_to_the_boards()
        {
            var b = new Button()
            {
                Content = "Назад к доскам",
                Margin = new Thickness(10, 30, 15, 0),
                Width = 140,
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Left,
            };
            b.Click += Back_Click;
            return b;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var a = new UserPageWindow(user);
            a.Show();
            window.Close();
        }

        private StackPanel Stack()
        {
            for (int i = 0; i < col.Count; i++)
            {
                myStackPanel.Children.Add(Column(col[i].cards, i));
                num_col = i;
            }

            myStackPanel.Children.Add(Plus_Column());
            
            return myStackPanel;
        }

        private void Task_Click(StackPanel cards, int index, Cards card, User user)
        {
            TaskWindow taskWindow = new TaskWindow(cards, index, card, user, desk);
            taskWindow.Show();
        }

        private Border Column(List<Cards> cards_all, int num_column)
        {
            
            Border bord = new Border
            {
                Margin = new Thickness(10),
                Background = new SolidColorBrush(Colors.White),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Padding = new Thickness(3),
                CornerRadius = new CornerRadius(5),
                Effect = new DropShadowEffect { BlurRadius = 30, Color = Colors.Black, ShadowDepth = 0 }
            };

            StackPanel myStack = new StackPanel();
            
            if (cards_all != null)
            {
                foreach (var card in cards_all)
                {
                    if ( !myStack.Children.Contains(card.btn))
                    {
                        card.btn.Click += (s, e) => Task_Click(myStack, num_column, card, user);
                        myStack.Children.Add(card.btn);
                    }
                }
            }

            var plus = new Button
            {
                Margin = new Thickness(20),
                Width = 225,
                Height = 40,
                Content = "+ Карточка"
            };
            plus.Click += (s, e) => PlusCard(myStack, num_column);

            myStack.Children.Add(plus);
            bord.Child = myStack;

            return bord;
        }
        private void PlusCard(StackPanel stack, int num_col)
        {
            int[] path = new int[4] { desk.parrent_id - 1, des, num_col, 0};

            if (col[num_col].cards.Count > 0) 
            {
                path[3] = col[num_col].cards.Count; 
            }

            var newCard = new Cards("Новая карточка", null, "#0000FF", path);

            users[desk.parrent_id - 1].desk[des].column[num_col].cards.Add(newCard);
            Read.Write(users);
           
            newCard.btn.Click += (s, e) => Task_Click(stack, stack.Children.Count - 1, newCard, user);
            stack.Children.Insert(stack.Children.Count - 1, newCard.btn);

            TaskWindow taskWindow = new TaskWindow(stack, stack.Children.Count - 2, newCard, user, desk);
            taskWindow.Show();
            
        }

        private Border Plus_Column()
        {
            Border Man = new Border
            {
                Margin = new Thickness(10),
                Background = new SolidColorBrush(Colors.White),
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Right,
                Padding = new Thickness(3),
                CornerRadius = new CornerRadius(5),
                Effect = new DropShadowEffect { BlurRadius = 30, Color = Colors.Black, ShadowDepth = 0 }
            };
            StackPanel Bruh = new StackPanel();

            Button AddJopumn = new Button
            {
                Background = new SolidColorBrush(Colors.Blue),
                Margin = new Thickness(10),
                Width = 50,
                Height = 40,
                Content = "+"
            };
            AddJopumn.Click += new RoutedEventHandler(PlusColumn_Click);

            Bruh.Children.Add(AddJopumn);
            Man.Child = Bruh;
            return Man;

        }
        private void PlusColumn_Click(object sender, RoutedEventArgs e)
        {
            if (myStackPanel.Children.Count - 1 < 10)
            {
                Create.CreateColumn(user.id, des);
                users = Read.Reading();
                desk = users[desk.parrent_id - 1].desk[des];
                col = desk.column;
                num_col++;

                myStackPanel.Children.Insert(myStackPanel.Children.Count - 1, Column(new List<Cards> {}, num_col));
                
                if(myStackPanel.Children.Count - 1 == 10)
                    myStackPanel.Children.RemoveAt(myStackPanel.Children.Count - 1);
            }
        }

        private void Key_down(object sender, KeyEventArgs e)
        {
            var m = new Moving_cards();
            if (e.Key == Key.P)
            {
                m.Move(myScrollViewer, window, desk);
            }
        }
    }
}
