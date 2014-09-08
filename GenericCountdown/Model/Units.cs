using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCountdown.Model
{
    public class Units
    {
        public string Name { get; set; }
        public Units()
        { }

        public Units(string name)
        {
            Name = name;
        }
    }
}
