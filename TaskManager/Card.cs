using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TaskManager
{
    internal class Card
    {
        private int index;
        private Button btn;
        private string text;
        public int Index
        {
            get { return index; }
        }
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        public Card(Button btn, int index)
        {
            this.index = index;
            this.btn = btn;
        }
    }
}
