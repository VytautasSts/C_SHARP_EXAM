using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CSHARP_Exam.Models;
using System.Xml.Linq;
using CSHARP_Exam.Utilities;
using System.ComponentModel;

namespace CourseTask.Services
{
    public class EmailSender
    {
        public string Email;

        public Check ThisCheck;
        public EmailSender(string email, Check check)
        {
            Email= email;
            ThisCheck = check;
        }
        public async Task SendEmail()
        {
            string path = $"C:\\Users\\vytas\\source\\repos\\CodeAcademy\\CSHARP_Exam\\FiscalReceipts\\{ThisCheck.ReceiptNo.ToString()}.txt";
            List<string> list = new List<string>();
            string [] lines = File.ReadAllLines(path);
            string text = "";
            foreach (string line in lines)
            {
                text += $"{line}\n";
            }
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("vytas.sts@gmail.com", "dibvcilpwbuklegn"),
                EnableSsl = true,
            };
            smtpClient.Send("vytas.sts@gmail.com", Email, "Receipt", text);
            await Task.Delay(500);
        }
    }
}
