using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSHARP_Exam.Models;
using CSHARP_Exam.Repositories;
using CSHARP_Exam.Services;

namespace CSHARP_Exam.Utilities
{
    public class OpenAccount
    {
        public static async Task CreateCheckAndReceipt(SQLite sqlite, Table table, OpenChecks openChecks)
        {
            Check check = new Check(table, Guid.NewGuid());
            await openChecks.Add_check(check, sqlite);
            DateTime time = check.get_time();
            await ReceiptMaker.CreateNewReceipt(check.ReceiptNo, table, time);
        }
    }
}
