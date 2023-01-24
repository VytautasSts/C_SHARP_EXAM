using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARP_Exam.Models
{
    public class Table
    {
        public int TableNo { get; }
        public int Capacity { get; }
        public int Load { get; set; }
        public bool Occupied { get; set; }

        public Table(int number, int capacity, int load, bool occupied)
        {
            TableNo = number;
            Capacity = capacity;
            Load = load;
            Occupied = occupied;
        }
    }
}
