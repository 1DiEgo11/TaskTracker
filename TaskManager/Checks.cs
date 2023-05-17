using System.Collections.Generic;
using System.Text.RegularExpressions;
//using read;
namespace TaskManager
{
    public class checks
    {
        public static bool Check_Login(string username)
        {
            Regex login_regex = new Regex("^[a-zA-Zа-яА-Я][a-zA-Zа-яА-Я0-9]{2,9}$");

            if (login_regex.Match(username).Success) // если совпадение удачно
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Check_newPassword(string username)
        {
            Regex login_regex = new Regex("^[a-zA-Zа-яА-Я][a-zA-Zа-яА-Я0-9]{4,9}$");

            if (login_regex.Match(username).Success) // если совпадение удачно
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckingForEngaged(string login)
        {
            List<User> user = Read.Reading();
            bool check = true;
            for (int i = 0; i < user.Count; i++)
            {
                if (user[i].login == login)
                {
                    check = false;
                    return check;
                }
            }
            return check;
        }
        public static string Log(string login)
        {
            List<User> user = Read.Reading();
            for (int i = 0; i < user.Count; i++)
            {
                if (user[i].login == login)
                {
                    return user[i].password;
                }
            }
            return "Not found";
        }

        public static bool Login(string username, string password)
        {
            if (Log(username) != "Not found" && Log(username) == password)
            {
                return true;
            }
            return false;
        }
    }
}