using CourseTask.Services;
using CSHARP_Exam.Models;
using CSHARP_Exam.Repositories;
using CSHARP_Exam.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace CSHARP_Exam.Services
{
    public delegate void Termination(SQLite sqlite, Check check, OpenChecks openChecks);
    public class eWaitress
    {
        Random rnd = new Random();

        List<Guid> ids = new List<Guid>();
        public bool OnDuty { get; set; }
        public string Status { get; set; }
        public eWaitress(bool working)
        {
            OnDuty = working;
            Status = "Getting ready";
        }
        public async Task ServeClients(SQLite sqlite, OpenChecks openChecks)
        {
            List<Table> tables = await sqlite.GetAllTables(sqlite.Conn);
            Task.Delay(1000).Wait();
            while (OnDuty)
            {
                if (openChecks.open_checks.Count < 1)
                {
                    Status = "Cleaning tables";
                    Task.Delay(1000).Wait();
                }
                else
                {
                    foreach (var check in openChecks.open_checks)
                    {
                        int menuSelect = 0;
                        string menu = "";
                        string category = "";
                        char q = 'n';
                        Guid current = check.ReceiptNo;
                        Status = "Going to table " + check.ThisTable.TableNo.ToString();
                        ids.Add(current);
                        foreach (var id in ids)
                        {
                            if (id == current)
                            {
                                menuSelect++;
                            }
                        }
                        Task.Delay(1000).Wait();
                        switch (menuSelect)
                        {
                            case 1: { menu = "DrinkMenu"; category = "Drinks"; } break;
                            case 2: { menu = "FoodMenu"; category = "Starters"; } break;
                            case 3: { menu = "FoodMenu"; category = "MainCourse"; } break;
                            case 4: { menu = "FoodMenu"; category = "Dessert"; } break;
                            case 5: { menu = "DrinkMenu"; category = "Drinks"; } break;
                            case 6:
                                {
                                    Console.WriteLine("Say goodbye to table " + check.ThisTable.TableNo.ToString());
                                    menu = "";
                                    category = "";
                                    Status = "Finalizing order " + check.ThisTable.TableNo.ToString();
                                    Termination thisAccount = new Termination(CloseCheck);
                                    thisAccount(sqlite, check, openChecks);
                                    //await CloseAccount.PayCheckAndLeave(sqlite, check, openChecks.open_checks);
                                    q = 'y';
                                    var emailrequired = rnd.Next(1, 5);
                                    if (emailrequired == 1)
                                    {
                                        Status = "Sending check by email " + check.ThisTable.TableNo.ToString();
                                        var email = Console.ReadLine();
                                        EmailSender emailsender = new(email, check);
                                        await emailsender.SendEmail();
                                    }
                                    while (ids.Remove(current)) ;
                                    openChecks.open_checks.Remove(check);//removes check from start of queue
                                }
                                break;
                            default: break;
                        }
                        if (q == 'y') break;
                        else
                        {
                            Status = "Collecting order " + check.ThisTable.TableNo.ToString();
                            Task.Delay(1000).Wait();
                            await TakeOrder.NewOrder(sqlite, menu, category, check, rnd);
                            Status = "Taking order to kitchen " + check.ThisTable.TableNo.ToString();
                            await Task.Delay(1000);
                            openChecks.open_checks.Remove(check);//removes check from start of queue
                            openChecks.open_checks.Add(check);//adds check to back of queue
                            break;
                        };
                    }
                }
            }
        }
        static void CloseCheck(SQLite sql,Check checks,OpenChecks openChecks1)
        {
            CloseAccount.PayCheckAndLeave(sql, checks, openChecks1.open_checks);
        }
    }
}
