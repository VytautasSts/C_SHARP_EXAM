using CSHARP_Exam.Models;
using CSHARP_Exam.Repositories;
using CSHARP_Exam.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace CSHARP_Exam.Services
{
    public class eAdministrator
    {
        public bool OnDuty { get; set; }
        public string Status { get; set; }
        public eAdministrator(bool working)
        {
            OnDuty = working;
            Status = "Getting ready.";
        }
        public async Task AdmitClients(SQLite sqlite, List<int> queue,OpenChecks openChecks)
        {
            while (OnDuty)
            {
                List<int> removeFromQueue = new List<int>();
                int sizeofqueue = queue.Count();
                if (sizeofqueue == 0)
                {
                    Status = "Waiting for customers";
                    Task.Delay(1000).Wait();
                }
                else
                {
                    for (int i = 0; i < sizeofqueue; i++)
                    {
                        List<int> getnumbers = await SearchForTable.FindTableAsync(queue[i], sqlite); //Get the table number or numbers for optimal seating
                        if (getnumbers.Count == 0) continue; // No suitable table found
                        else
                        {
                            Status = "Searching for table";
                            Task.Delay(1000).Wait();
                            foreach (var table_number in getnumbers)
                            {
                                Table table = await sqlite.GetTable(sqlite.Conn, table_number);
                                Status = "Opening account " + table_number.ToString();
                                await OpenAccount.CreateCheckAndReceipt(sqlite, table, openChecks);
                                removeFromQueue.Add(i);
                            }
                        }
                    }
                    if (removeFromQueue.Count() == 2) queue.RemoveAt(removeFromQueue[1]);
                    if (removeFromQueue.Count() == 1) queue.RemoveAt(removeFromQueue[0]);
                    Status = "Returning to front desk";
                }
            }
        }
    }
}
