using CSHARP_Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARP_Exam.Services
{
    public class SearchForTable
    {
        public static async Task<List<int>> FindTableAsync(int persons, SQLite sqlite)
        {
            int tableNo = 0;
            List<int> answer = new List<int>();
            List<Table> tables = await sqlite.GetAllTables(sqlite.Conn);
            foreach (var table in tables)
            {
                if (table.Occupied) continue;
                else
                {
                    if (persons > table.Capacity) continue;
                    else
                    {
                        tableNo = table.TableNo;
                        answer.Add(tableNo);
                        table.Occupied = true;
                        await sqlite.UpdateTableData(sqlite.Conn, table, persons);
                        break;
                    }
                }
            }
            if (tableNo > 0) { } //If Administrator found a table, OK
            else                    //If not, we find two available tables to split the group
            {
                int currentTable = 0;
                int currentCapacity = 0;
                int previousTable = 0;
                int previousCapacity = 0;
                for (int i = 1; i < tables.Count() - 1; i++)
                {
                    if (tables[i].Occupied) continue;
                    else
                    {
                        previousTable = currentTable;
                        previousCapacity = currentCapacity;
                        currentTable = tables[i].TableNo;
                        currentCapacity = tables[i].Capacity;
                        int combinedCapacity = currentCapacity + previousCapacity;
                        if (persons > combinedCapacity) continue;
                        else
                        {
                            if (persons < combinedCapacity)
                            {
                                answer.Add(previousTable);
                                answer.Add(currentTable);
                                foreach (var table in tables.SkipLast(1))
                                {
                                    if (table.TableNo == previousTable)
                                    {
                                        table.Occupied = true;
                                        await sqlite.UpdateTableData(sqlite.Conn, table, table.Capacity);
                                    };
                                    if (table.TableNo == currentTable)
                                    {
                                        table.Occupied = true;
                                        await sqlite.UpdateTableData(sqlite.Conn, table, persons - previousCapacity);
                                    };
                                    if (table.TableNo > currentTable) { break; };
                                }
                                break;
                            }
                        }
                    }
                }
            }
            return answer;
        }
    }
}
