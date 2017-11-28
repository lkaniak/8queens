using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8queens
{
    public class Pair<First, Second>
    {
        public Pair()
        {

        }
        public Pair(First first, Second second)
        {
            this.first = first;
            this.second = second;
        }

        public First first { get; set; }
        public Second second { get; set; }
    }
}
