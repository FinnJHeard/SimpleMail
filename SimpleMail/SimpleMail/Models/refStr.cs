using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMail.Models
{
    public class refStr
    {
        public string str { get; set; }
        public bool flag { get; set; }

        public refStr(string str)
        {
            this.str = str;
            flag = true;
        }

        public override string ToString()
        {
            return str;
        }
    }
}
