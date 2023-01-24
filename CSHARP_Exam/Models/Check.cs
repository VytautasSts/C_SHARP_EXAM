using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARP_Exam.Models
{
    public class Check
    {
        public Guid ReceiptNo { get; }
        public Table ThisTable { get; }
        public DateTime Timestamp { get; set; }
        public double Amount { get; set; }

        public Check(Table table,Guid receiptid)
        {
            ReceiptNo = receiptid;
            ThisTable = table;
            Timestamp = DateTime.Now;
            Amount = 0.0;
        }
        public Check add_amount(double addition)
        {
            Amount = Math.Round(Amount + addition,2);
            return this;
        }
        public DateTime get_time()
        {
            return Timestamp;
        }
    }
}
