using CSHARP_Exam.Models;
using CSHARP_Exam.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARP_Exam.Utilities
{
    public class TakeOrder
    {
      public static async Task NewOrder(SQLite sqlite, string menu, string category, Check check, Random rnd)
        {
            List<OrderItem> options = sqlite.GetOptions(sqlite.Conn, menu, category);
            List<OrderItem> items = new List<OrderItem>();
            double sum = 0;
            for (int i=0;i<check.ThisTable.Load;i++)
            {
                int selection = rnd.Next(1,options.Count());
                items.Add(new(options[selection].Name, options[selection].Price));
            };
            foreach(var item in items)
            {
                sum = sum+item.Price;
                await ReceiptMaker.UpdateReceipt(check.ReceiptNo,item.Name, item.Price);
            }
            Console.WriteLine("Updating receipt");
            check.add_amount(sum);
            await sqlite.UpdateCheck(sqlite.Conn,check);
            Console.WriteLine("Updating check");
        }
    }
}
