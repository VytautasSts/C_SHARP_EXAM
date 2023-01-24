using System.Data;
using System.Text;
using System.Data.SQLite;
using CSHARP_Exam.Models;
using System.Runtime.CompilerServices;

namespace CSHARP_Exam.Services
{
    public class SQLite
    {
        public SQLiteConnection Conn { get; set; }
        public SQLite()
        {
            Conn = CreateConnection();
        }
        public SQLiteConnection CreateConnection()
        {
            Conn = new SQLiteConnection("Data Source = C:\\Users\\vytas\\source\\repos\\CodeAcademy\\CSHARP_Exam\\database.sqlite; version = 3; New = true; Compress = true;");
            try { Conn.Open(); }
            catch { };
            return Conn;
        }
        public async Task<List<Table>> GetAllTables(SQLiteConnection conn)
        {
            SQLiteDataReader reader;
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM Tables";
            reader = command.ExecuteReader();
            List<Table> tableData = new List<Table>();
            while (reader.Read())
            {
                int nr = reader.GetInt32(0);
                int cap = reader.GetInt32(1);
                int load = reader.GetInt32(2);
                bool occupied = Convert.ToBoolean(reader.GetString(3));
                tableData.Add(new Table(nr,cap,load,occupied));
            }
            await Task.Delay(500);
            return tableData;
        }
        public async Task<Table> GetTable(SQLiteConnection conn, int tableNo)
        {
            SQLiteDataReader reader;
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = $"SELECT * FROM Tables WHERE Number ='{tableNo}';";
            reader = command.ExecuteReader();
            int nr = 0;
            int cap = 0;
            int load = 0;
            bool occupied = false;
            while (reader.Read())
            {
                nr = tableNo;
                cap = reader.GetInt32(1);
                load = reader.GetInt32(2);
                occupied = Convert.ToBoolean(reader.GetString(3));
            }
            Table thisTable = new Table(nr, cap, load, occupied);
            await Task.Delay(1000);
            return thisTable;
        }

        public List<OrderItem> GetOptions(SQLiteConnection conn, string menu, string category)
        {
            SQLiteDataReader reader;
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = $"SELECT * FROM '{menu}' WHERE Category ='{category}';";
            reader = command.ExecuteReader();
            List<OrderItem> options = new();
            while (reader.Read())
            {
                options.Add(new OrderItem(reader.GetString(0),reader.GetDouble(1)));
            }
            return options;
        }

        public async Task UpdateTableData(SQLiteConnection conn, Table t, int people)
        {
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = $"UPDATE Tables SET " +
                $"Capacity='{t.Capacity}', " +
                $"Load='{people}', " +
                $"Occupied='{t.Occupied}' WHERE Number='{t.TableNo}';";
            //Console.WriteLine($"UPDATED!Table number:{t.TableNo},Occupied: {t.Occupied},Load={people},Capacity={t.Capacity}");
            command.ExecuteNonQuery();
            await Task.Delay(50);
        }
        public async Task InsertCheck(SQLiteConnection conn, Check check)
        {
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = $"INSERT INTO Checks (ID, TableNum, Timestamp, Amount) " +
                $"VALUES ('{check.ReceiptNo}','{check.ThisTable.TableNo}','{check.Timestamp}','{check.Amount}');";
            //Console.WriteLine($"VALUES ('{check.ReceiptNo}','{check.ThisTable.TableNo}','{check.Timestamp}','{check.Amount}');");
            command.ExecuteNonQuery();
            await Task.Delay(50);
        }
        public async Task UpdateCheck(SQLiteConnection conn, Check check)
        {
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = $"UPDATE Checks SET " +
                $"TableNum='{check.ThisTable.TableNo}', " +
                $"Timestamp='{check.Timestamp}', " +
                $"Amount='{check.Amount}' WHERE ID='{check.ReceiptNo}';";
            //Console.WriteLine($"VALUES ('{check.ReceiptNo}','{check.ThisTable.TableNo}','{check.Timestamp}','{check.Amount}');");
            command.ExecuteNonQuery();
            await Task.Delay(50);
        }
    }
}
