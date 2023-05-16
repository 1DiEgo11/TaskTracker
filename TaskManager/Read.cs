using read;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.IO;
using System.Text.Json;


namespace read
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
        public int access { get; set; }
        public int[] whitelist { get; set; }
        public string name { get; set; }
        public List<Column> column { get; set; }
        public Desk(int access, int[] whitelist, string name)
        {
            this.access = access;
            this.whitelist = whitelist;
            this.name = name;
            //column = null;
        }
    }
    public class Column
    {
        public string name { get; set; }
        public List<Card> cards { get; set; }
        public Column(string name)
        {
            this.name = name;
            //cards = null;
        }
    }

    public class Card
    {
        public string name { get; set; }
        public string description { get; set; }
        public string colour { get; set; }
        public Card(string name, string description)
        {
            this.name = name;
            this.description = description;
            colour = "#000fff";
        }
    }
    public class Read
    {
        public static List<User> Reading()
        {
            string pathToJson = @"C:\Users\Tessia\Desktop\bede.json";

            string json = File.ReadAllText(pathToJson);
            //Console.WriteLine(json);

            List<User> user = JsonSerializer.Deserialize<List<User>>(json);
            //Console.WriteLine(user[0].login);
            return user;
        }
        public static void Write(List<User> users)
        {
            using (StreamWriter fstream = new StreamWriter(@"C:\Users\Tessia\Desktop\bede.json"))
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
    }

}
