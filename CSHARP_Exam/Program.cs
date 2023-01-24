using System.Data.SQLite;
using CSHARP_Exam.Services;
using CSHARP_Exam.Models;
using CSHARP_Exam.Repositories;
using CSHARP_Exam.Utilities;
using System.Diagnostics;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System;
using CourseTask.Services;

namespace CSHARP_Exam;

class Program
{
    static async Task Main(string[] args)
    {
        SQLite sqlite = new(); //Establish SQLite connection
        Console.WriteLine("sqlite");
        OpenChecks openChecks = new();
        Console.WriteLine("openChecks");
        List<int> queue = new List<int>(); //Create a que at the reception
        Console.WriteLine("queue");
        eWaitress waitress = new eWaitress(true);
        Console.WriteLine("waitress");
        eAdministrator administrator = new eAdministrator(true);
        Console.WriteLine("admin");
        AddClients addclients = new AddClients(true);
        Console.WriteLine("Client generator");
        addclients.Generate(queue);
        Console.WriteLine("queue start");
        administrator.AdmitClients(sqlite, queue, openChecks);
        Console.WriteLine("admin start");
        waitress.ServeClients(sqlite, openChecks);
        Console.WriteLine("waitress start");
        int delayTime = 1000;
        while (true)
        {
            int heads = CountHeads(queue);
            List<int> tablesTaken = await TablesTaken(sqlite);
            List<int> tablesFree = await TablesFree(sqlite);
            Console.Clear();
            Console.WriteLine(string.Format("{0,20}|{1,35}", "Administrator is:",administrator.Status));
            Console.WriteLine(string.Format("{0,20}|{1,35}", "Waitress is:", waitress.Status));
            Console.WriteLine(string.Format("{0,20}|{1,35}", "Goups in queue:", queue.Count()));
            Console.WriteLine(string.Format("{0,20}|{1,35}", "People in queue:", heads));
            Console.WriteLine(string.Format("{0,20}|{1,35}", "Tables taken:", string.Join(",",tablesTaken)));
            Console.WriteLine(string.Format("{0,20}|{1,35}", "Tables free:", string.Join(",", tablesFree)));
            Console.WriteLine(string.Format("{0,20}|{1,35}", "Clients coming:", addclients.Run));
            if (heads > 30) addclients.Run = false;
            if (heads < 30) addclients.Run = true;
            if(waitress.Status.Contains("Sending check by email "))
            {
                Console.WriteLine("Provide email:");
                await Task.Delay(5000);
                Console.WriteLine("Press Enter again");
                while (Console.ReadKey().Key != ConsoleKey.Enter) {};
            }
            await Task.Delay(delayTime);
        }
    }
    private static int CountHeads(List<int> queue)
    {
        int count = 0;
        foreach (var item in queue)
        {
            count += item;
        }
        return count;
    }
    private static async Task<List<int>> TablesTaken(SQLite sqlite)
    {
        List<Table> tables = await sqlite.GetAllTables(sqlite.Conn);
        List<int> count = new();
        foreach (var table in tables)
        {
            if(table.Occupied)count.Add(table.TableNo);
        }
        return count;
    }
    private static async Task<List<int>> TablesFree(SQLite sqlite)
    {
        List<Table> tables = await sqlite.GetAllTables(sqlite.Conn);
        List<int> count = new();
        foreach (var table in tables)
        {
            if (!table.Occupied) count.Add(table.TableNo);
        }
        return count;
    }
}
