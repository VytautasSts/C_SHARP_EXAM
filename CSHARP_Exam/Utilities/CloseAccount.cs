using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSHARP_Exam.Models;
using CSHARP_Exam.Services;

namespace CSHARP_Exam.Utilities
{
    public class CloseAccount
    {
        public static async Task PayCheckAndLeave(SQLite sqlite, Check check,List<Check> openChecks)
        {
            check.ThisTable.Occupied = false;
            int peopleAtTable = 0;
            await sqlite.UpdateTableData(sqlite.Conn, check.ThisTable, peopleAtTable);
            openChecks.Remove(check);
        }
    }
}
