﻿using System;
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
    /// Логика взаимодействия для TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        public TaskWindow(StackPanel cards, int index, Cards card, User user, Desk desk)
        {
            InitializeComponent();
            var d = new Task();
            Background = new SolidColorBrush(Colors.LightGray);
            Content = d.CardSettings(this, cards, card, user, desk, index);
            Show();
        }
    }
}
