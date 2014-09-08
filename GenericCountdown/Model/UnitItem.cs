using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCountdown.Model
{
    public class UnitItem
    {
        public string UnitName
        {
            get;
            private set;
        }
        public string UnitValue
        {
            get;
            private set;
        }

        public UnitItem(string name, string value)
        {
            UnitName = name;
            UnitValue = value;
        }
    }
}
