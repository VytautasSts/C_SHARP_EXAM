using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSHARP_Exam.Models;
using CSHARP_Exam.Services;

namespace CSHARP_Exam.Repositories
{
    public class OpenChecks
    {
        public List<Check> open_checks= new List <Check>();
        public async Task Add_check(Check check, SQLite sqlite)
        {
            open_checks.Add(check);
            await sqlite.InsertCheck(sqlite.Conn,check);
        }
        public void Remove(Check check)
        {
            open_checks.Remove(check);
        }
        public Check GetCheckByTable(Table table)
        {
            int x = 0;
            for (int i = 0; i < open_checks.Count; i++)
            {
                if (open_checks[i].ThisTable.TableNo == table.TableNo)
                {
                    x = i; break;
                }
            }
            return open_checks[x];
        }
    }
    
}
