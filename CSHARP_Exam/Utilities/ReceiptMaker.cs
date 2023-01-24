using CSHARP_Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARP_Exam.Utilities
{
    public class ReceiptMaker
    {
        public static async Task CreateNewReceipt(Guid name,Table table,DateTime timestamp)
        {
            string path = $"C:\\Users\\vytas\\source\\repos\\CodeAcademy\\CSHARP_Exam\\FiscalReceipts\\{name.ToString()}.txt";
            string entry = $"Receipt number: {name};\nTable number: {table.TableNo};\nSeats taken: {table.Load}/{table.Capacity};\nTime: {timestamp};\nOrder details:\n";
            await File.WriteAllTextAsync(path, entry);
        }
        public static async Task UpdateReceipt(Guid name, string item, double price)
        {
            string path = $"C:\\Users\\vytas\\source\\repos\\CodeAcademy\\CSHARP_Exam\\FiscalReceipts\\{name.ToString()}.txt";
            string entry = $"{item} - {price}\n";
            await File.AppendAllTextAsync(path, entry);
        }
    }
}
