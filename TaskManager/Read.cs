using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
//using Newtonsoft.Json;

namespace TaskManager
{

    public class User
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public List<Desk> desk { get; set; }
        public User(int id, string login, string password, string email)
        {
            this.id = id;
            this.login = login;
            this.password = password;
            this.email = email;
            //desk = null;
        }
    }
    public class Desk
    {
        public int parrent_id { get; set; }
        public int access { get; set; }
        public int[] whitelist { get; set; }
        public string name { get; set; }
        public List<Column> column { get; set; }
        public Desk(int parrent_id, int access, int[] whitelist, string name)
        {
            this.access = access;
            this.whitelist = whitelist;
            this.name = name;
            this.parrent_id = parrent_id;
        }
    }
    public class Column
    {
        public string name { get; set; }
        public List<Cards> cards { get; set; }
        public Column(string name, List<Cards> cards)
        {
            this.name = name;
            this.cards = cards;
        }
    }
    public class Cards
    {
        public Button btn;
        public string name { get; set; }
        public string description { get; set; }
        public string colour { get; set; }
        public int[] path {  get; set; } 
        public Cards(string name, string description, string colour, int[] path)
        {
            this.path=path;
            this.name = name;
            this.description = description;
            this.colour = colour;
            btn = new Button()
            {
                Background = Moving_cards.Converter(colour),
                Margin = new Thickness(10),
                Width = 225,
                Height = 40,
                Content = name
            };
        }
    }
    public class Read
    {
        public static List<User> Reading()
        {
            string pathToJson = @"C:\Users\asus\source\repos\TaskManager_All\bede.json";

            string json = File.ReadAllText(pathToJson);

            List<User> user = JsonSerializer.Deserialize<List<User>>(json);
            return user;
        }
        public static void Write(List<User> users)
        {
            using (StreamWriter fstream = new StreamWriter(@"C:\Users\asus\source\repos\TaskManager_All\bede.json"))
            {
                string json = JsonSerializer.Serialize<List<User>>(users);
                fstream.Write(json);
            }
        }
        public static int GetId()
        {
            List<User> users = Reading();
            return users.Count + 1;
        }

        public static void Rewriting_Cards_path(List<User> users, int id_user, int desk_num, int index_colm)
        {
            //Исправить, так как надо проходиться по всем столбцам
            for (int j = 0; j < users[id_user].desk[desk_num].column.Count; j++)
            {
                for (int i = 0; i < users[id_user].desk[desk_num].column[j].cards.Count; i++)
                {
                    users[id_user].desk[desk_num].column[j].cards[i].path = new int[] { id_user, desk_num, j, i };
                }
            }
            Write(users);
        }
    }

}
