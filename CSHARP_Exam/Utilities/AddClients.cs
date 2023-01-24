using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARP_Exam.Utilities
{
    public class AddClients
    {
        public bool Run { get; set; }
        public AddClients(bool run)
        {
            Run = run;
        }
        public async void Generate(List<int> waitingList)
        {
            Random rnd = new Random();
            int i = 0;
            while (Run)
            {
                waitingList.Add(rnd.Next(1, 4));
                if (i % 2 == 0) waitingList.Add(rnd.Next(5, 8));
                if (i % 3 == 0) waitingList.Add(rnd.Next(9, 12));
                if (i % 4 == 0) waitingList.Add(rnd.Next(13, 16));
                await Task.Delay(10000);
                i++;
                if (i == 17) i = 0;
            }
        }
    }
}
